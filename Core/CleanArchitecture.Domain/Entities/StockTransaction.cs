using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Entities
{
    public class StockTransaction : BaseEntity
    {
        // 買入或賣出
        public bool IsPurchase { get; set; } = true;

        // 買入數量
        [Range(0, double.MaxValue, ErrorMessage = "Quantity must be a non-negative value.")]
        public decimal Quantity { get; set; }

        // 買入價格
        [Range(0, double.MaxValue, ErrorMessage = "PurchasePrice must be a non-negative value.")]
        public decimal PurchasePrice { get; set; } = 0;

        // 賣出價格
        public decimal? SellPrice { get; set; }

        // 買入日期
        public DateTimeOffset? PurchaseTime { get; set; }

        // 賣出日期
        public DateTimeOffset? SellTime { get; set; }
    }
}