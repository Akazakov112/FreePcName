using FreePcNameWeb.Models;
using FreePcNameWeb.Models.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FreePcNameWeb
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<OspsContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DBConnection")));
            services.AddSingleton<IFreenameSearcher, ADOFreenameSearcher>();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "freepcname/{controller=Main}/{action=Index}");
                endpoints.MapFallbackToController("freepcname", "Index", "Main");
            });
        }
    }
}
