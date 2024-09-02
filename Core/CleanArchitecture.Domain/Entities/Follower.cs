using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Entities
{
    public class Follower : BaseEntity
    {
        // 備註
        public string Remark { get; set; } = string.Empty;
    }
}