using System.Security.Claims;
using CleanArchitecture.Application.Commands.FollowStock;
using CleanArchitecture.Application.Queries.FollowStock;
using CleanArchitecture.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebAPI.Controllers
{
    public class FollowerController : BaseApiController
    {
        // 取得追蹤的股票
        [HttpGet]
        public async Task<IActionResult> GetFollowedStocks()
        {
            var userId = GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized();
            }

            var result = await Mediator.Send(new GetFollowedStocks.Query { UserId = userId });
            return HandleResult(result);
        }

        // 新增追蹤
        [HttpPost]
        public async Task<IActionResult> AddFollow(AddFollowerRequest data)
        {
            var userId = GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized();
            }

            var result = await Mediator.Send(new AddFollower.Command
            {
                Id = userId,
                Follower = data
            });

            return HandleResult(result);
        }
    }
}
