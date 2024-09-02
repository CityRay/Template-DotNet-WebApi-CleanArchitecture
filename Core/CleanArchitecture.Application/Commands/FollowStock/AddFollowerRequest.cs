namespace CleanArchitecture.Application.Commands.FollowStock
{
    public class AddFollowerRequest
    {
        public string UserId { get; set; }
        public Guid StockId { get; set; }
        public string Remark { get; set; }
    }
}