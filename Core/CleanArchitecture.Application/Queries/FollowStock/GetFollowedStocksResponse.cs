using CleanArchitecture.Application.Queries.Stocks;

namespace CleanArchitecture.Application.Queries.FollowStock
{
    public class GetFollowedStocksResponse
    {
        public Guid ID { get; set; }
        public string UserId { get; set; }
        public Guid StockId { get; set; }
        public GetStocksResponse Stock { get; set; }
        public string Remark { get; set; }
    }
}