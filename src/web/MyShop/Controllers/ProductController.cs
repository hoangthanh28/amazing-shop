using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.Model;
using MyShop.Service.Abstraction;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyShop.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IUserContext _userContext;
        public ProductController(IHttpClientFactory clientFactory, IUserContext userContext)
        {
            _clientFactory = clientFactory;
            _userContext = userContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetProductAsync(string sidx, string sord, int page, int rows, bool _search, string searchField, string searchOper, string searchString)
        {
            var client = _clientFactory.CreateClient("product-service");
            var response = await client.GetAsync("prd/products");
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            return new StreamActionResult(async res =>
            {
                await stream.CopyToAsync(res.Body);
            });
        }
    }
}