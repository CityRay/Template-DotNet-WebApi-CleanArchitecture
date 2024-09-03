using CleanArchitecture.Application.Commands.Stocks;
using CleanArchitecture.Application.Commands.StocksRobot;
using CleanArchitecture.Application.Queries.Stocks;
using CleanArchitecture.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebAPI.Controllers
{
    public class StocksController : BaseApiController
    {
        /// <summary>
        /// 取得所有
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetStocks()
        {
            //await Task.Delay(1000);
            //return HandleResult(Result<object>.Success(null));

            var result = await Mediator.Send(new GetStocks.Query());
            return HandleResult(result);
        }

        /// <summary>
        /// 新增Stock
        /// </summary>
        /// <param name="stock"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddStock(AddStockRequest stock)
        {
            var result = await Mediator.Send(new AddStock.Command
            {
                Stock = stock
            });

            return HandleResult(result);
        }

        /// <summary>
        /// 更新Stock
        /// </summary>
        /// <param name="stock"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateStock(UpdateStockRequest stock)
        {
            var result = await Mediator.Send(new UpdateStock.Command
            {
                Stock = stock
            });

            return HandleResult(result);
        }

        /// <summary>
        /// 更新 Robot 策略
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateRobotStrategy(IList<AddOrUpdateRobotStrategyRequest> request)
        {
            var result = await Mediator.Send(new AddOrUpdateRobotStrategy.Command
            {
                Request = request
            });

            return HandleResult(result);
        }
    }
}
