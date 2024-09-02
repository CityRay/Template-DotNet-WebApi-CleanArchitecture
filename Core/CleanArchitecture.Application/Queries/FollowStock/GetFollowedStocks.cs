using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.Queries.Stocks;
using CleanArchitecture.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Queries.FollowStock
{
    public class GetFollowedStocks
    {
        public class Query : IRequest<Result<List<GetFollowedStocksResponse>>>
        {
            public string UserId { get; set; }
        }

        public class Handler(PostgresqlDataContext context, ILogger<Handler> logger) : IRequestHandler<Query, Result<List<GetFollowedStocksResponse>>>
        {
            public async Task<Result<List<GetFollowedStocksResponse>>> Handle(Query request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrEmpty(request.UserId))
                {
                    return Result<List<GetFollowedStocksResponse>>.Success([]);
                }

                try
                {
                    var result = await context.Followers
                        .AsNoTracking()
                        .Where(f => f.UserId == request.UserId)
                        .Select(f => new GetFollowedStocksResponse
                        {
                            ID = f.Id,
                            UserId = f.UserId,
                            StockId = f.StockId,
                            Stock = new GetStocksResponse
                            {
                                Id = f.Stock.Id,
                                Symbol = f.Stock.Symbol,
                                Name = f.Stock.Name,
                                Price = f.Stock.Price,
                                Industry = f.Stock.Industry,
                                LastDividendYield = f.Stock.LastDividendYield,
                                DisposalStock = f.Stock.DisposalStock,
                                AlertStock = f.Stock.AlertStock,
                                UpdatedTime = f.Stock.UpdatedTime
                            },
                            Remark = f.Remark
                        })
                        .ToListAsync(cancellationToken);

                    return Result<List<GetFollowedStocksResponse>>.Success(result);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.Message);
                    return Result<List<GetFollowedStocksResponse>>.Failure("Error retrieving followers");
                }
            }
        }
    }
}

