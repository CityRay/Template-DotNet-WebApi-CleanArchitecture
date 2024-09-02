using CleanArchitecture.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Persistence.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddPostgresqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connection = config.GetConnectionString("PostgresqlConnection");
            services.AddDbContext<PostgresqlDataContext>(options => options.UseNpgsql(connection));
        }
    }
}
