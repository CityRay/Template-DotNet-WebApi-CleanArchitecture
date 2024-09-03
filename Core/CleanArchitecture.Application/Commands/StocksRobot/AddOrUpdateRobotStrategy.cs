using CleanArchitecture.Application.Common;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Commands.StocksRobot
{
    public class AddOrUpdateRobotStrategy
    {
        public class Command : IRequest<Result<Unit>>
        {
            public required IList<AddOrUpdateRobotStrategyRequest> Request { get; set; }
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
                    if (request.Request == null || request.Request.Count == 0)
                    {
                        return Result<Unit>.Failure("Not found");
                    }

                    // 透過 Request 取得所有的 symbols 與 strategyTypes
                    var symbols = request.Request.Select(r => r.Symbol).Distinct().ToList();
                    var strategyTypes = request.Request.Select(r => r.StrategyType).Distinct().ToList();

                    // 一次性查詢所有需要的 StockRobotTransaction 資料
                    var existingStrategies = await _context.StockRobotTransactions
                        .Where(srt => symbols.Contains(srt.Symbol) && strategyTypes.Contains(srt.StrategyType))
                        .ToListAsync(cancellationToken);

                    // 放置批次新增或更新的策略
                    var newStrategies = new List<StockRobotTransaction>();
                    var updatedStrategies = new List<StockRobotTransaction>();

                    foreach (var item in request.Request)
                    {
                        // StockRobotTransactions 找出符合 item.Symbol 並符合 item.StrategyType 的資料
                        var robotStrategy = existingStrategies.FirstOrDefault(x => x.Symbol == item.Symbol && x.StrategyType == item.StrategyType);

                        if (robotStrategy != null)
                        {
                            // 時間轉換為UTC
                            item.ConvertDatesToUtc();

                            // 更新現有的策略
                            robotStrategy.TriggerPrice = item.TriggerPrice;
                            robotStrategy.TriggerDate = item.TriggerDate;
                            robotStrategy.HoldDays = item.HoldDays;
                            robotStrategy.ExitDate = item.ExitDate;
                            robotStrategy.RiskLevel = item.RiskLevel;
                            robotStrategy.UpdatedTime = DateTime.UtcNow;

                            updatedStrategies.Add(robotStrategy);
                        }
                        else
                        {
                            // 新增新的策略
                            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Symbol == item.Symbol, cancellationToken);
                            if (stock == null)
                            {
                                _logger.LogWarning($"Stock with symbol {item.Symbol} not found.");
                                continue;
                            }

                            // 時間轉換為UTC
                            item.ConvertDatesToUtc();

                            var newRobotStrategy = new StockRobotTransaction
                            {
                                Symbol = item.Symbol,
                                StockId = stock.Id,
                                TriggerPrice = item.TriggerPrice,
                                TriggerDate = item.TriggerDate,
                                HoldDays = item.HoldDays,
                                ExitDate = item.ExitDate,
                                StrategyType = item.StrategyType,
                                RiskLevel = item.RiskLevel
                            };

                            newStrategies.Add(newRobotStrategy);
                        }
                    }

                    // 批次更新和新增
                    if (updatedStrategies.Any())
                    {
                        _context.StockRobotTransactions.UpdateRange(updatedStrategies);
                    }

                    if (newStrategies.Any())
                    {
                        await _context.StockRobotTransactions.AddRangeAsync(newStrategies, cancellationToken);
                    }

                    await _context.SaveChangesAsync(cancellationToken);
                    return Result<Unit>.Success(Unit.Value);
                }
                catch (InvalidOperationException ex)
                {
                    _logger.LogError(ex, "日期轉換失敗");
                    return Result<Unit>.Failure(ex.Message);
                }
                catch (DbUpdateException dbEx)
                {
                    _logger.LogError(dbEx, "Database update error in AddOrUpdateRobotStrategy");
                    return Result<Unit>.Failure("Update error");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error AddOrUpdateRobotStrategy stock");
                    return Result<Unit>.Failure(ex.Message);
                }
            }
        }
    }
}
