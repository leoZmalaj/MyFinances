using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyFinances.Contract.Home;
using MyFinances.Contract.Login;
using MyFinances.Core.Home.Repository;
using MyFinances.Core.Home.Service;
using MyFinances.Core.Login.LoginRepository;
using MyFinances.Core.Login.LoginService;
using MyFinances.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinances
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication("CookieAth")
                .AddCookie("CookieAth", config =>
                {
                    config.Cookie.Name = "LogIn";
                    config.LoginPath = "/Home/ReturnLogin";
                });
            //var con = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDataProtection();
            services.AddHttpContextAccessor();
            services.AddMemoryCache();
            //services.AddDbContext<MyFinancesContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddScoped<IHomeService, HomeServices>();
            services.AddScoped<IHomeRepository, HomeRepositories>();
            services.AddScoped<ILoginRepository, LoginRepositories>();
            services.AddScoped<ILoginService, LoginServices>();
            services.AddMvc();
            services.AddControllers();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();
            app.UseDeveloperExceptionPage();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
