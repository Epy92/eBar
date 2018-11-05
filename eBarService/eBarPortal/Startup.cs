using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.FileProviders;
using System.IO;
using eBarPortal.Models;
using ViewModels;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace eBarPortal
{
    public class Startup
    {
        public IHostingEnvironment HostingEnvironment { get; private set; }
        

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {

            var builder = new ConfigurationBuilder()
             .SetBasePath(env.ContentRootPath)
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
             .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
             .AddEnvironmentVariables();

            Configuration = builder.Build();

            AppSettings.ReadAppSettings(configuration);
            HostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("LoggedIn", policy => policy.RequireAuthenticatedUser());
            //});



            //services.AddMvc(config =>
            //{
            //    var policy = new AuthorizationPolicyBuilder()
            //                     .RequireAuthenticatedUser()
            //                     .Build();
            //    config.Filters.Add(new AuthorizeFilter(policy));
            //});

            //services.AddMvc();
            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
            //services.AddIdentity<ApplicationUser, IdentityRole>();


            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.AccessDeniedPath = "/Account/Forbidden/";
                options.LoginPath = "/Account/Login/";
                options.LogoutPath = "/Account/Logout";
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(
            //    Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", @"assets")),
            //    RequestPath = new PathString("/assets")
            //});

            //app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseSession();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
