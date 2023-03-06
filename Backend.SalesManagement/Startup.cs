using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Backend.SalesManagement.Context;
using Backend.SalesManagement.Repositories;
using Backend.SalesManagement.Repositories.Interfaces;
using Backend.SalesManagement.Services;
using Backend.SalesManagement.Services.Interfaces;

namespace Backend.SalesManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            ConfigureTransientServices(services);
            ConfigureRepositories(services);
            ConfigureEntityFramework(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        private static void ConfigureTransientServices(IServiceCollection services)
        {
            services.AddTransient<ISalesService, SalesService>();
        }

        private static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddSingleton<ISalesRepository, SalesRepository>();
        }

        private static void ConfigureEntityFramework(IServiceCollection services)
        {
            var databaseName = Configuration["EntityFramework:DatabaseName"];

            services.AddDbContext<SalesDatabaseContext>(options =>
                options.UseInMemoryDatabase(databaseName));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
