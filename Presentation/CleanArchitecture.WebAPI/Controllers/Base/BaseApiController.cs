using CleanArchitecture.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebAPI.Controllers.Base
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        private ILogger<BaseApiController> _logger;

        protected IMediator Mediator => _mediator ??=
            HttpContext.RequestServices.GetService<IMediator>();

        protected ILogger<BaseApiController> Logger => _logger ??=
            HttpContext.RequestServices.GetService<ILogger<BaseApiController>>();

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
    }
}
