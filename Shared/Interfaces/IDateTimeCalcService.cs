using System.Collections.Generic;
using System.Threading.Tasks;

namespace GIBS.Module.DateTimeCalc.Services
{
    public interface IDateTimeCalcService 
    {
        Task<List<Models.DateTimeCalc>> GetDateTimeCalcsAsync(int ModuleId);

        Task<Models.DateTimeCalc> GetDateTimeCalcAsync(int DateTimeCalcId, int ModuleId);

        Task<Models.DateTimeCalc> AddDateTimeCalcAsync(Models.DateTimeCalc DateTimeCalc);

        Task<Models.DateTimeCalc> UpdateDateTimeCalcAsync(Models.DateTimeCalc DateTimeCalc);

        Task DeleteDateTimeCalcAsync(int DateTimeCalcId, int ModuleId);
    }
}
