using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediatR;
using System.IO;
using AmazingShop.Product.Application.Product.Command;

namespace AmazingShop.Product.Controller
{
    [ApiController]
    [Route("prd/[controller]")]
    [Authorize(AuthenticationSchemes = "oidc")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductAsync()
        {
            var command = new GetAllProduct();
            var products = await _mediator.Send(command);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            var command = new GetProductById(id);
            var product = await _mediator.Send(command);
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
        public async Task<IActionResult> UploadProductImageAsync(IFormFile file)
        {
            var fileName = file.FileName;
            var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            var command = new UploadProductImage(fileName, stream.ToArray(), file.ContentType);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductAsync(int id, UpdateProduct command)
        {
            command.Id = id;
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
