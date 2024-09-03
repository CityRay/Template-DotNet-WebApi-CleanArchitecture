using System.Security.Claims;
using CleanArchitecture.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebAPI.Controllers.Base
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private IMediator? _mediator;
        private ILogger<BaseApiController>? _logger;

        protected IMediator Mediator => _mediator ??=
            HttpContext.RequestServices.GetService<IMediator>() ?? throw new InvalidOperationException("IMediator service not found.");

        protected ILogger<BaseApiController> Logger => _logger ??=
            HttpContext.RequestServices.GetService<ILogger<BaseApiController>>() ?? throw new InvalidOperationException("ILogger<BaseApiController> service not found.");

        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result == null)
            {
                return NotFound();
            }
            if (result.IsSuccess && result.Data != null)
            {
                return Ok(result.Data);
            }
            if (result.IsSuccess && result.Data == null)
            {
                return NotFound();
            }
            return BadRequest(result.Error);
        }

        protected string? GetUserId()
        {
            var id = ClaimTypes.NameIdentifier;
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            var userId = User.FindFirstValue(id);
            return string.IsNullOrWhiteSpace(userId) ? null : userId;
        }
    }
}
