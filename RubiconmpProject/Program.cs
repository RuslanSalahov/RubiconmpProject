using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RubiconmpProject.Infrastructure;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        ApplyMigrations(host);
        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                config.AddEnvironmentVariables();
            })
            .ConfigureServices((hostContext, services) =>
            {
                // Configure DbContext and Connection String
                services.AddDbContext<RepositoryContext>(options =>
                {
                    options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection"));
                });
            });

    private static void ApplyMigrations(IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<RepositoryContext>();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while applying migrations:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
