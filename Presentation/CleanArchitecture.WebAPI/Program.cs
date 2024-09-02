using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistence;
using CleanArchitecture.Persistence.Context;
using CleanArchitecture.Persistence.Extensions;
using CleanArchitecture.WebAPI.Extensions;
using CleanArchitecture.WebAPI.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CleanArchitecture.WebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddPostgresqlContext(builder.Configuration);
            builder.Services.AddAppServices(builder.Configuration);
            builder.Services.AddIdentityServices(builder.Configuration);

            builder.Host.UseSerilog();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseCors("CorsPolicy");

            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseSerilogRequestLogging(options => {
                // �p�G�n�ۭq�T�����d���榡�A�i�H�ק�o�̡A���ק��ä��|�v�T���c�ưO�����ݩ�
                options.MessageTemplate = "Handled {RequestPath}";

                // �w�]��X���������Ŭ� Information�A�i�H�b���ק�O������
                // options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Debug;

                // �i�H�q httpContext ���o HttpContext �U�Ҧ��i�H���o����T�I
                options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                    diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
                    diagnosticContext.Set("UserID", httpContext.User.Identity?.Name);
                };
            });

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<PostgresqlDataContext>();
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                await context.Database.MigrateAsync();
                await Seed.SeedData(context, userManager);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred during migration");
            }

            app.Run();
        }
    }
}
