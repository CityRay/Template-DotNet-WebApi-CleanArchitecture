using CleanArchitecture.Application.Common;
using CleanArchitecture.Domain.DTO;
using CleanArchitecture.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Queries.Stocks
{
    public class GetStocks
    {
        public class Query : IRequest<Result<List<GetStocksResponse>>> { }

        public class Handler(PostgresqlDataContext context, ILogger<Handler> logger) : IRequestHandler<Query, Result<List<GetStocksResponse>>>
        {
            public async Task<Result<List<GetStocksResponse>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await context.Stocks
                        .Select(s => new GetStocksResponse
                        {
                            Id = s.Id,
                            Symbol = s.Symbol,
                            Name = s.Name,
                            Price = s.Price,
                            Industry = s.Industry ?? string.Empty,
                            LastDividendYield = s.LastDividendYield,
                            DisposalStock = s.DisposalStock,
                            AlertStock = s.AlertStock,
                            UpdatedTime = s.UpdatedTime
                        })
                        .ToListAsync(cancellationToken);

                    return Result<List<GetStocksResponse>>.Success(result);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.Message);
                    return Result<List<GetStocksResponse>>.Failure("Error retrieving stocks");
                }
            }
        }

    }
}
