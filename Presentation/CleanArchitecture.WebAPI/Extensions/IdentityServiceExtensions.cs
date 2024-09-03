using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistence.Context;
using CleanArchitecture.WebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CleanArchitecture.WebAPI.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<PostgresqlDataContext>();

            //services.AddAuthentication();

            var tokenKey = config["TokenKey"] ?? throw new ArgumentNullException("TokenKey is required");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = true, // 啟用發行者驗證
                        ValidIssuer = config["ValidIssuer"], // 設定有效的發行者
                        ValidateAudience = true, // 啟用Audience驗證
                        ValidAudience = config["ValidAudience"] // 設定有效的Audience
                    };
                });


            services.AddAuthentication();
            services.AddScoped<TokenService>();

            return services;
        }
    }
}
