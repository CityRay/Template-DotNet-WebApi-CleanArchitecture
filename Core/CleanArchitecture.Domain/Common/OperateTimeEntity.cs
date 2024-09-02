namespace CleanArchitecture.Domain.Common
{
    public class OperateTimeEntity
    {
        public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? UpdatedTime { get; set; }
        public DateTimeOffset? DeletedTime { get; set; }
    }
}
