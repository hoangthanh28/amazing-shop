using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AmazingShop.Product.Test.Extension;
using AmazingShop.Product.Test.Model;
using Humanizer;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
using Xunit.Abstractions;

namespace AmazingShop.Product.Test
{
    public class IntegrationTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly ITestOutputHelper _output;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly WebApplicationFactory<Startup> _factory;
        public IntegrationTest(WebApplicationFactory<Startup> factory, ITestOutputHelper output)
        {
            _output = output;
            _factory = factory.WithWebHostBuilder(builder =>
            {
                //builder.UseEnvironment("IntegrationTest");
                builder.ConfigureAppConfiguration((hostingContext, configBuilder) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    configBuilder.AddJsonFile("appsettings.json", optional: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
                    configBuilder.AddEnvironmentVariables();
                });
                builder.ConfigureServices(serviceCollection =>
                {
                    // configure service here

                });
            });
            _configuration = _factory.Services.GetService(typeof(IConfiguration)) as IConfiguration;
            _logger = _factory.Services.GetService(typeof(ILogger<IntegrationTest>)) as ILogger<IntegrationTest>;
        }

        [Fact]
        public async Task PostmanCollectionTester()
        {
            var httpClient = _factory.CreateClient();
            var postmanCollection = _configuration["Postman:Collection"];
            var environmentPath = _configuration["Postman:Environment"];
            using (var client = new HttpClient())
            {
                _output.WriteLine($"Download the postman collection: {postmanCollection}");
                _output.WriteLine($"Download the postman environment: {environmentPath}");
                var response = await client.GetStringAsync(postmanCollection);
                var environmentResponse = await client.GetStringAsync(environmentPath);
                var postmanEnvironment = JsonConvert.DeserializeObject<JObject>(environmentResponse)["environment"];
                var environment = postmanEnvironment.ToObject<PostmanEnvironment>();
                var collection = JsonConvert.DeserializeObject<JObject>(response)["collection"]["item"] as JArray;
                await AccquiredAccessTokenAndRandomValueAsync(client, environment);
                await TestThisSubCollectionAsync(httpClient, environment, collection);
            }
        }

        private async Task TestThisSubCollectionAsync(HttpClient httpClient, PostmanEnvironment environment, JArray collection)
        {
            foreach (var subCollection in collection)
            {
                if (subCollection["request"] is JObject requestObject)
                {

                    var request = requestObject.ToObject<PostmanRequest>();
                    string path = null;
                    if (request.Url.Raw.StartsWith("{{identity-service}}"))
                    {
                        continue;
                    }
                    if (request.Url.Raw.EndsWith("skipTest=true"))
                    {
                        continue;
                    }
                    _output.WriteLine($"Run the request: {subCollection["name"]}");
                    AddPreRequestScript(subCollection, environment);
                    path = string.Join('/', request.Url.Path).ReplaceWithEnvironment(environment);
                    httpClient.DefaultRequestHeaders.Clear();
                    foreach (var header in request.Header.Where(x => !x.Disabled && x.Key != "Content-Type"))
                    {
                        httpClient.DefaultRequestHeaders.Add(header.Key, header.Value.ReplaceWithEnvironment(environment));
                    }
                    try
                    {
                        HttpResponseMessage result = null;
                        switch (request.Method.ToUpper())
                        {
                            case "GET":
                                result = await httpClient.GetAsync(path);
                                break;
                            case "POST":
                                result = await httpClient.PostAsync(path, GetContent(request, environment));
                                break;
                            case "PUT":
                                result = await httpClient.PutAsync(path, GetContent(request, environment));
                                break;
                            case "DELETE":
                                result = await httpClient.DeleteAsync(path);
                                break;
                        }
                        var testResponse = await result.Content.ReadAsStringAsync();

                        _output.WriteLine($"{(int)result.StatusCode}- {testResponse}");
                        testResponse = testResponse.Replace("[],", "null,");
                        var responseObject = JsonConvert.DeserializeObject<JObject>(testResponse) as JObject;
                        var dictionary = ParseDictionary(responseObject);
                        foreach (var item in dictionary)
                        {
                            _output.WriteLine($"Add to environment: {item.Key} - {item.Value}");
                            environment.Values.Add(new EnvironmentValue(item.Key, item.Value, false));
                        }
                        Assert.True(testResponse != null);
                    }
                    catch (Exception exc)
                    {
                        _logger.LogError(exc, exc.Message);
                    }

                }
                else if (subCollection["item"] is JArray array)
                {
                    await TestThisSubCollectionAsync(httpClient, environment, array);
                }
            }
        }
        private Dictionary<string, string> ParseDictionary(JObject responseObject, string root = "")
        {
            var dictionary = new Dictionary<string, string>();
            if (responseObject != null)
            {
                foreach (var item in responseObject)
                {
                    if (item.Value is JObject jObj)
                    {
                        var result = ParseDictionary(jObj, $"{root}_{item.Key}".Trim('_'));
                        foreach (var dic in result)
                        {
                            dictionary[dic.Key] = dic.Value;
                        }
                    }
                    else if (item.Value is JArray arr)
                    {
                        var key = item.Key.Replace("data", "");
                        if (!string.IsNullOrEmpty(key))
                        {
                            key = key.Singularize();
                        }
                        var result = ParseDictionary(arr, $"{root}_{key}".Trim('_'));
                        foreach (var dic in result)
                        {
                            dictionary[dic.Key] = dic.Value;
                        }
                    }
                    else
                    {
                        dictionary[$"{root}_{item.Key}".Trim('_')] = item.Value.ToString();
                    }
                }
            }
            return dictionary;
        }
        private Dictionary<string, string> ParseDictionary(JArray responseObject, string root = "")
        {
            var dictionary = new Dictionary<string, string>();
            var item = responseObject.First;
            if (item is JObject jObj)
            {
                var result = ParseDictionary(jObj, root);
                foreach (var dic in result)
                {
                    dictionary[dic.Key] = dic.Value;
                }
            }
            return dictionary;
        }
        private void AddPreRequestScript(JToken subCollection, PostmanEnvironment environment)
        {
            var events = (subCollection["event"] as JArray)?.Where(x => x["listen"].ToString() == "prerequest").Select(x => x["script"]["exec"]);
            if (events != null && events.Any())
            {
                foreach (var evt in events)
                {
                    if (evt is JArray scripts)
                    {
                        foreach (var script in scripts)
                        {
                            var sScript = script.ToString().Replace("pm.environment.set(", "").Replace(");", "").Replace("\\", "").Replace("\"", "").Trim();
                            if (!string.IsNullOrEmpty(sScript) && sScript.Contains(","))
                            {
                                _output.WriteLine($"Add prerequest sript {sScript}");
                                var sData = sScript.Split(',');
                                environment.Values.Add(new EnvironmentValue(sData[0], sData[1], true));
                            }
                        }
                    }
                }
            };
        }
        private HttpContent GetContent(PostmanRequest request, PostmanEnvironment environment)
        {

            if (request.Body.Mode == "raw")
            {
                var content = request.Body.Raw.ReplaceWithEnvironment(environment);
                _output.WriteLine(content);
                return new StringContent(content, System.Text.Encoding.UTF8, request.Header.FirstOrDefault(x => string.Equals(x.Key, "Content-Type", StringComparison.InvariantCultureIgnoreCase))?.Value ?? "application/json");
            }
            else if (request.Body.Mode == "urlencoded")
            {
                var content = request.Body.Urlencoded.Select(x => KeyValuePair.Create(x.Key, x.Value.ReplaceWithEnvironment(environment)));
                _output.WriteLine(JsonConvert.SerializeObject(content));
                return new FormUrlEncodedContent(content);
            }
            else if (request.Body.Mode == "formdata")
            {
                var method = new MultipartFormDataContent();
                var streamContent = new StreamContent(File.Open("AppData/images/bg.jpeg", FileMode.Open));
                method.Add(streamContent, "file", "bg.jpeg");
                return method;
            }
            return new StringContent("");
        }

        private string RandomNameGenerate(int length = 10)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            return finalString;
        }

        private async Task AccquiredAccessTokenAndRandomValueAsync(HttpClient client, PostmanEnvironment environment)
        {
            #region Preparing access_token
            var tokenEndpoint = environment.Values.First((x => x.Key == "identity-service")).Value;
            var clientId = environment.Values.First(x => x.Key == "sa_client_id").Value;
            var clientSecrect = environment.Values.First(x => x.Key == "sa_client_password").Value;
            _output.WriteLine($"Accquired the access token from endpoint: {tokenEndpoint} with client credentials: {clientId}/{clientSecrect}");
            var content = new FormUrlEncodedContent(
                     new List<KeyValuePair<string, string>>()
                     {
                            KeyValuePair.Create("grant_type","client_credentials"),
                            KeyValuePair.Create("client_id",clientId),
                            KeyValuePair.Create("client_secret",clientSecrect),
                     });
            var tokenRequest = await client.PostAsync($"{tokenEndpoint.TrimEnd('/')}/connect/token", content);
            var tokenResponse = await tokenRequest.Content.ReadAsStringAsync();
            _output.WriteLine(tokenResponse);
            var accessToken = JsonConvert.DeserializeObject<JObject>(tokenResponse)["access_token"].ToString();
            var accessTokenEnvironment = environment.Values.FirstOrDefault(x => x.Key == "access_token");
            if (accessTokenEnvironment != null)
            {
                environment.Values.Remove(accessTokenEnvironment);
            }
            environment.Values.Add(new EnvironmentValue("access_token", accessToken, true));
            #endregion

            #region Preparing randomName
            var randomName = RandomNameGenerate();
            var randomNameEnvironment = environment.Values.FirstOrDefault(x => x.Key == "randomName");
            if (randomNameEnvironment != null)
            {
                environment.Values.Remove(randomNameEnvironment);
            }
            environment.Values.Add(new EnvironmentValue("randomName", randomName, true));
            _output.WriteLine($"Insert the random string into variable: {randomNameEnvironment}");
            #endregion
        }
    }
}
