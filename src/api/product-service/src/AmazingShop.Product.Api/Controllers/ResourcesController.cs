using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediatR;
using System.IO;
using AmazingShop.Product.Application.Product.Command;
using AmazingShop.Product.Application.Resource.Command;

namespace AmazingShop.Product.Controller
{
    [ApiController]
    [Route("prd/[controller]")]
    [Authorize(AuthenticationSchemes = "oidc")]
    public class ResourcesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ResourcesController> _logger;
        public ResourcesController(IMediator mediator, ILogger<ResourcesController> logger)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllResources()
        {
            var command = new GetAllResources();
            var result = await _mediator.Send(command);
            return Ok(result);
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
}
