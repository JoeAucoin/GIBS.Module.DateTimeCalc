using System.Collections.Generic;
using System.Threading.Tasks;

namespace GIBS.Module.DateTimeCalc.Repository
{
    public interface IDateTimeCalcRepository
    {
        IEnumerable<Models.DateTimeCalc> GetDateTimeCalcs(int ModuleId);
        Models.DateTimeCalc GetDateTimeCalc(int DateTimeCalcId);
        Models.DateTimeCalc GetDateTimeCalc(int DateTimeCalcId, bool tracking);
        Models.DateTimeCalc AddDateTimeCalc(Models.DateTimeCalc DateTimeCalc);
        Models.DateTimeCalc UpdateDateTimeCalc(Models.DateTimeCalc DateTimeCalc);
        void DeleteDateTimeCalc(int DateTimeCalcId);
    }
}
