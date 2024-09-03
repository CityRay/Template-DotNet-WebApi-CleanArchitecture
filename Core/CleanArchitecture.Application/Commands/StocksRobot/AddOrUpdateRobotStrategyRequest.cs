using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.Commands.StocksRobot
{
    public class AddOrUpdateRobotStrategyRequest
    {
        /// <summary>
        /// 代碼
        /// </summary>
        public required string Symbol { get; set; }

        /// <summary>
        /// 觸發價格
        /// </summary>
        public decimal TriggerPrice { get; set; }

        /// <summary>
        /// 觸發日期
        /// </summary>
        public DateTimeOffset TriggerDate { get; set; }

        /// <summary>
        /// 持有天數
        /// </summary>
        public int HoldDays { get; set; }

        /// <summary>
        /// 出場日期
        /// </summary>
        public DateTimeOffset? ExitDate { get; set; }

        /// <summary>
        /// 策略類型
        /// </summary>
        public StrategyType StrategyType { get; set; }

        /// <summary>
        /// 風險程度
        /// </summary>
        public RiskLevelType RiskLevel { get; set; }

        /// <summary>
        /// 將日期轉換為 UTC 時間
        /// </summary>
        public void ConvertDatesToUtc()
        {
            try
            {
                TriggerDate = TriggerDate.ToUniversalTime();

                if (ExitDate.HasValue)
                {
                    ExitDate = ExitDate.Value.ToUniversalTime();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"日期轉換失敗: {ex.Message}", ex);
            }
        }
    }
}
