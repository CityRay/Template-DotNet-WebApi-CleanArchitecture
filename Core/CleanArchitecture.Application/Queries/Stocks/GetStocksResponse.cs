namespace CleanArchitecture.Application.Queries.Stocks
{
    public class GetStocksResponse
    {
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
        public bool DisposalStock { get; set; }

        // 警示股
        public bool AlertStock { get; set; }

        public DateTimeOffset? UpdatedTime { get; set; }
    }
}