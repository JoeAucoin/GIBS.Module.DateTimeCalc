using Microsoft.AspNetCore.Builder; 
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Oqtane.Infrastructure;
using GIBS.Module.DateTimeCalc.Repository;
using GIBS.Module.DateTimeCalc.Services;

namespace GIBS.Module.DateTimeCalc.Startup
{
    public class ServerStartup : IServerStartup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // not implemented
        }

        public void ConfigureMvc(IMvcBuilder mvcBuilder)
        {
            // not implemented
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IDateTimeCalcService, ServerDateTimeCalcService>();
            services.AddDbContextFactory<DateTimeCalcContext>(opt => { }, ServiceLifetime.Transient);
        }
    }
}
