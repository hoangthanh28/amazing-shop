using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediatR;
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
        public async Task<IActionResult> GetAllResourcesAsync()
        {
            var command = new GetAllResources();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetResourceByIdAsync(int id)
        {
            var command = new GetResourceById(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
