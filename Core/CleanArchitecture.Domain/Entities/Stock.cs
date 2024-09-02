using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Entities
{
    public class Stock : OperateTimeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        // 股票代號
        public string Symbol { get; set; }

        // 股票名稱
        public string Name { get; set; }

        // 股票價格
        public decimal Price { get; set; }

        // 產業
        public string Industry { get; set; }

        // 殖利率
        public decimal LastDividendYield { get; set; }

        // 處置股
        public bool DisposalStock { get; set; } = false;

        // 警示股
        public bool AlertStock { get; set; } = false;

        // 誰在關注該股票
        public ICollection<Follower> Followers { get; set; } = new List<Follower>();

        // 股票的交易紀錄
        public ICollection<StockTransaction> Transactions { get; set; } = new List<StockTransaction>();
    }
}
