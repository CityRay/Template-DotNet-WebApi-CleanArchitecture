using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.ModelContract.WebAPI.Request
{
    public class StockRobotRequest
    {
        /// <summary>
        /// 代碼
        /// </summary>
        public string Symbol { get; set; }

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
        public StrategyType StrategyName { get; set; }

        /// <summary>
        /// 風險程度
        /// </summary>
        public RiskLevelType RiskLevel { get; set; }
    }
}
