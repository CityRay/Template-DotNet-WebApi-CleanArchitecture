using CleanArchitecture.Application.Common;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistence.Context;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Commands.Stocks
{
    public class AddStock
    {
        public class Command : IRequest<Result<Unit>>
        {
            public AddStockRequest Stock { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Stock).SetValidator(new AddStockValidator());
            }
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

                    var stock = new Stock
                    {
                        Symbol = request.Stock.Symbol,
                        Name = request.Stock.Name.Trim(),
                        Price = request.Stock.Price,
                        Industry = request.Stock.Industry.Trim(),
                        LastDividendYield = request.Stock.LastDividendYield,
                        DisposalStock = request.Stock.DisposalStock,
                        AlertStock = request.Stock.AlertStock
                    };

                    await _context.Stocks.AddAsync(stock);
                    await _context.SaveChangesAsync();

                    return Result<Unit>.Success(Unit.Value);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error adding stock : {request.Stock.Name}");
                    return Result<Unit>.Failure(ex.Message);
                }
            }
        }
    }
}
