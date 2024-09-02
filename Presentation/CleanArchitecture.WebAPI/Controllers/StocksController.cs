using CleanArchitecture.Application.Commands.Stocks;
using CleanArchitecture.Application.Queries.Stocks;
using CleanArchitecture.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebAPI.Controllers
{
    public class StocksController : BaseApiController
    {
        // 取得所有
        [HttpGet]
        public async Task<IActionResult> GetStocks()
        {
            //await Task.Delay(1000);
            //return HandleResult(Result<object>.Success(null));

            var result = await Mediator.Send(new GetStocks.Query());
            return HandleResult(result);
        }

        // 新增股票
        [HttpPost]
        public async Task<IActionResult> AddStock(AddStockRequest stock)
        {
            var result = await Mediator.Send(new AddStock.Command
            {
                Stock = stock
            });

            return HandleResult(result);
        }

        // 更新股票
        [HttpPut]
        public async Task<IActionResult> UpdateStock(UpdateStockRequest stock)
        {
            var result = await Mediator.Send(new UpdateStock.Command
            {
                Stock = stock
            });

            return HandleResult(result);
        }
    }
}
