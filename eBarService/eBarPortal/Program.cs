using eBarPortal.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace eBarPortal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var serviceProvider = services.GetRequiredService<IServiceProvider>();
                    var configuration = services.GetRequiredService<IConfiguration>();

                    var RoleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
                    var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                    AppSettings.ReadAppSettings(configuration);
                }
                catch (Exception exception)
                {
                    //var logger = services.GetRequiredService<ILogger<Program>>();
                    //logger.LogError(exception, "An error occurred while creating roles");
                }
            }
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
