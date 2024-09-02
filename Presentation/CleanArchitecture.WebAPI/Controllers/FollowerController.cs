using CleanArchitecture.Application.Commands.FollowStock;
using CleanArchitecture.Application.Queries.FollowStock;
using CleanArchitecture.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebAPI.Controllers
{
    public class FollowerController : BaseApiController
    {
        // 取得追蹤的股票
        [HttpGet("id")]
        public async Task<IActionResult> GetFollowedStocks(string id)
        {
            var result = await Mediator.Send(new GetFollowedStocks.Query { UserId = id });
            return HandleResult(result);
        }

        // 新增追蹤
        [HttpPost]
        public async Task<IActionResult> AddFollow(AddFollowerRequest data)
        {
            var result = await Mediator.Send(new AddFollower.Command
            {
                Follower = data
            });

            return HandleResult(result);
        }
    }
}
