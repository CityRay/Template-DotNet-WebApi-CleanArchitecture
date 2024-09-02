using CleanArchitecture.Application.Common;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistence.Context;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Commands.FollowStock
{
    public class AddFollower
    {
        public class Command : IRequest<Result<Unit>>
        {
            public AddFollowerRequest Follower { get; set; }
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
                    if (request.Follower == null)
                    {
                        return Result<Unit>.Failure("Follower not found");
                    }

                    var follower = new Follower
                    {
                        UserId = request.Follower.UserId,
                        StockId = request.Follower.StockId,
                        Remark = request.Follower.Remark
                    };

                    await _context.Followers.AddAsync(follower);
                    await _context.SaveChangesAsync();

                    return Result<Unit>.Success(Unit.Value);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error adding follower : {request.Follower.UserId}");
                    return Result<Unit>.Failure(ex.Message);
                }
            }
        }
    }
}
