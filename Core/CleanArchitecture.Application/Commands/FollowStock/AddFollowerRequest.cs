namespace CleanArchitecture.Application.Commands.FollowStock
{
    public class AddFollowerRequest
    {
        public Guid StockId { get; set; }
        public string? Remark { get; set; }
    }
}