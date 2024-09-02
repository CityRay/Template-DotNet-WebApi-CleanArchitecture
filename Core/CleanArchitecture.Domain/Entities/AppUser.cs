using CleanArchitecture.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }

        // 頭像
        public string Avatar { get; set; }

        // 生日
        public string Birthday { get; set; }

        // 身份
        public UserType UserType { get; set; }

        // 通過管理者審核
        public bool IsApproved { get; set; }

        public ICollection<Follower> Followers { get; set; } = new List<Follower>();

        // 股票的交易紀錄
        public ICollection<StockTransaction> Transactions { get; set; } = new List<StockTransaction>();
    }
}