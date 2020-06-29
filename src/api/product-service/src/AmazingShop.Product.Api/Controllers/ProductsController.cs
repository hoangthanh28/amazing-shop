using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediatR;
using System.IO;
using AmazingShop.Product.Application.Product.Command;
using System.Linq;

namespace AmazingShop.Product.Controller
{
    [ApiController]
    [Route("prd/[controller]")]
    [Authorize(AuthenticationSchemes = "oidc")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductsController> _logger;
        private static readonly IEnumerable<ProductModel> _products = new List<ProductModel>()
        {
            new ProductModel(){Id = 1, Name =  "Pizza"},
            new ProductModel(){Id = 2, Name =  "Cheese"},
            new ProductModel(){Id = 3, Name = "Hotpot"},
        };
        public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_products);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _products.Where(x => x.Id == id).FirstOrDefault();
            if (product != null)
            {
                return Ok(product);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost("images")]
        [AllowAnonymous]
        public async Task<IActionResult> UploadProductImageAsync(IFormFile file)
        {
            var fileName = file.FileName;
            var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            var command = new UploadProductImage(fileName, stream.ToArray(), file.ContentType);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
