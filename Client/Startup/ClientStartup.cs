using Microsoft.Extensions.DependencyInjection;
using Oqtane.Services;
using GIBS.Module.DateTimeCalc.Services;

namespace GIBS.Module.DateTimeCalc.Startup
{
    public class ClientStartup : IClientStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDateTimeCalcService, DateTimeCalcService>();
        }
    }
}
