namespace CleanArchitecture.Application.Commands.Stocks
{
    public class UpdateStockRequest
    {
        public Guid Id { get; set; }
        // 股票價格
        public decimal Price { get; set; }

        // 殖利率
        public decimal LastDividendYield { get; set; }

        // 處置股
        public bool DisposalStock { get; set; } = false;

        // 警示股
        public bool AlertStock { get; set; } = false;
    }
}
