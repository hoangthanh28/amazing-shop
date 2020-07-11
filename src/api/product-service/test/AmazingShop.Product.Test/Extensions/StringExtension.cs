using AmazingShop.Product.Test.Model;

namespace AmazingShop.Product.Test.Extension
{
    public static class StringExtension
    {
        public static string ReplaceWithEnvironment(this string input, PostmanEnvironment environment)
        {
            var source = input;
            foreach (var value in environment.Values)
            {
                source = source.Replace($"{{{{{value.Key}}}}}", value.Value);
            }
            return source;
        }
    }
}