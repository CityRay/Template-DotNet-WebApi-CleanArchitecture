using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Domain.Entities
{
    public class StockRobotTransaction : OperateTimeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// 代碼
        /// </summary>
        [MaxLength(8)]
        public required string Symbol { get; set; }

        /// <summary>
        /// StockId
        /// </summary>
        public Guid StockId { get; set; }

        /// <summary>
        /// Stock
        /// </summary>
        [ForeignKey("StockId")]
        public Stock? Stock { get; set; }

        /// <summary>
        /// 觸發價格
        /// </summary>
        [Range(0, double.MaxValue)]
        public decimal TriggerPrice { get; set; }

        /// <summary>
        /// 觸發日期
        /// </summary>
        public DateTimeOffset TriggerDate { get; set; }

        /// <summary>
        /// 持有天數
        /// </summary>
        [Range(0, double.MaxValue)]
        public int HoldDays { get; set; } = 0;

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
    }
}
