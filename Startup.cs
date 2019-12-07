using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Models;
using Microsoft.AspNetCore.Identity;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace SocialNetwork
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
 
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //var connectionString = "server=localhost;Uid=root;Pwd=24052000;Database=social_network;TreatTinyAsBoolean=true;";
            //System.Console.WriteLine(connectionString);
            
            services.AddDbContext<UsersContext>(options =>
                options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<User, IdentityRole>(settings => {
				settings.Password.RequireDigit           = false;
				settings.Password.RequireLowercase       = false;
				settings.Password.RequireUppercase       = false;
				settings.Password.RequireNonAlphanumeric = false;
			}).AddEntityFrameworkStores<UsersContext>()
            .AddDefaultTokenProviders();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseStaticFiles();
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseMvc(routes =>
            {   
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}"); 

                routes.MapRoute("Registration", "Registration/Registration", new { controller = "Registration", action = "Registration" });
                routes.MapRoute("Account", "Registration/Registration", new { controller = "Account", action = "Profile" });
            });
        }
    }
}
