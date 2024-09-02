using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistence.Context;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Persistence
{
    public class Seed
    {
        public static async Task SeedData(PostgresqlDataContext context, UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        DisplayName = "Bob",
                        UserName = "bob",
                        Email = "bob@gmail.com"
                    },
                    new AppUser
                    {
                        DisplayName = "Jane",
                        UserName = "jane",
                        Email = "jane@gmail.com"
                    },
                    new AppUser
                    {
                        DisplayName = "ray",
                        UserName = "ray",
                        Email = "ray@gmail.com"
                    }
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }
            }

            if (!context.Stocks.Any())
            {
                var stocks = new List<Stock>
                {
                    // 台灣股市
                    new Stock
                    {
                        Symbol = "2330",
                        Name = "台積電",
                        Price = 600,
                        Industry = "半導體",
                        DisposalStock = false,
                        AlertStock = false
                    },
                    new Stock
                    {
                        Symbol = "2317",
                        Name = "鴻海",
                        Price = 100,
                        Industry = "電子",
                        DisposalStock = false,
                        AlertStock = false
                    },
                    new Stock
                    {
                        Symbol = "2454",
                        Name = "聯發科",
                        Price = 400,
                        Industry = "半導體",
                        DisposalStock = false,
                        AlertStock = false
                    },
                    new Stock
                    {
                        Symbol = "2603",
                        Name = "長榮",
                        Price = 50,
                        Industry = "航運",
                        DisposalStock = false,
                        AlertStock = false
                    },
                    new Stock
                    {
                        Symbol = "2881",
                        Name = "富邦金",
                        Price = 500,
                        Industry = "金融",
                        DisposalStock = false,
                        AlertStock = false
                    },
                    new Stock
                    {
                        Symbol = "2882",
                        Name = "國泰金",
                        Price = 400,
                        Industry = "金融",
                        DisposalStock = false,
                        AlertStock = false
                    }
                };

                await context.Stocks.AddRangeAsync(stocks);
                await context.SaveChangesAsync();
            }
        }
    }
}
