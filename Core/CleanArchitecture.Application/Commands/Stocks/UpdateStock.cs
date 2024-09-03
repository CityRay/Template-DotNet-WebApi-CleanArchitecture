using CleanArchitecture.Application.Common;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistence.Context;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Commands.Stocks
{
    public class UpdateStock
    {
        public class Command : IRequest<Result<Unit>>
        {
            public required UpdateStockRequest Stock { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly PostgresqlDataContext _context;
            private readonly ILogger<Handler> _logger;

            public Handler(PostgresqlDataContext context, ILogger<Handler> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.Stock == null)
                    {
                        return Result<Unit>.Failure("Stock not found");
                    }

                    var stock = await _context.Stocks.FindAsync(request.Stock.Id);
                    if (stock == null)
                    {
                        return Result<Unit>.Failure("Stock not found");
                    }

                    stock.Price = request.Stock.Price;
                    stock.DisposalStock = request.Stock.DisposalStock;
                    stock.AlertStock = request.Stock.AlertStock;
                    stock.UpdatedTime = DateTime.UtcNow;

                    await _context.SaveChangesAsync();

                    return Result<Unit>.Success(Unit.Value);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error updating stock : {request.Stock.Id}");
                    return Result<Unit>.Failure(ex.Message);
                }
            }
        }
    }
}
