using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Oqtane.Services;
using Oqtane.Shared;

namespace GIBS.Module.DateTimeCalc.Services
{
    public class DateTimeCalcService : ServiceBase, IDateTimeCalcService
    {
        public DateTimeCalcService(HttpClient http, SiteState siteState) : base(http, siteState) { }

        private string Apiurl => CreateApiUrl("DateTimeCalc");

        public async Task<List<Models.DateTimeCalc>> GetDateTimeCalcsAsync(int ModuleId)
        {
            List<Models.DateTimeCalc> DateTimeCalcs = await GetJsonAsync<List<Models.DateTimeCalc>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={ModuleId}", EntityNames.Module, ModuleId), Enumerable.Empty<Models.DateTimeCalc>().ToList());
            return DateTimeCalcs.OrderBy(item => item.Name).ToList();
        }

        public async Task<Models.DateTimeCalc> GetDateTimeCalcAsync(int DateTimeCalcId, int ModuleId)
        {
            return await GetJsonAsync<Models.DateTimeCalc>(CreateAuthorizationPolicyUrl($"{Apiurl}/{DateTimeCalcId}/{ModuleId}", EntityNames.Module, ModuleId));
        }

        public async Task<Models.DateTimeCalc> AddDateTimeCalcAsync(Models.DateTimeCalc DateTimeCalc)
        {
            return await PostJsonAsync<Models.DateTimeCalc>(CreateAuthorizationPolicyUrl($"{Apiurl}", EntityNames.Module, DateTimeCalc.ModuleId), DateTimeCalc);
        }

        public async Task<Models.DateTimeCalc> UpdateDateTimeCalcAsync(Models.DateTimeCalc DateTimeCalc)
        {
            return await PutJsonAsync<Models.DateTimeCalc>(CreateAuthorizationPolicyUrl($"{Apiurl}/{DateTimeCalc.DateTimeCalcId}", EntityNames.Module, DateTimeCalc.ModuleId), DateTimeCalc);
        }

        public async Task DeleteDateTimeCalcAsync(int DateTimeCalcId, int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{DateTimeCalcId}/{ModuleId}", EntityNames.Module, ModuleId));
        }
    }
}
