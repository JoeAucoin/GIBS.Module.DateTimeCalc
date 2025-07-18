using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;

namespace GIBS.Module.DateTimeCalc.Repository
{
    public class DateTimeCalcRepository : IDateTimeCalcRepository, ITransientService
    {
        private readonly IDbContextFactory<DateTimeCalcContext> _factory;

        public DateTimeCalcRepository(IDbContextFactory<DateTimeCalcContext> factory)
        {
            _factory = factory;
        }

        public IEnumerable<Models.DateTimeCalc> GetDateTimeCalcs(int ModuleId)
        {
            using var db = _factory.CreateDbContext();
            return db.DateTimeCalc
                .Where(item => item.ModuleId == ModuleId)
                .OrderBy(item => item.EndDate == null ? 1 : 0)
                .ThenBy(item => item.EndDate)
                .ThenByDescending(item => item.StartDate)
                .ToList();
        }

        public Models.DateTimeCalc GetDateTimeCalc(int DateTimeCalcId)
        {
            return GetDateTimeCalc(DateTimeCalcId, true);
        }

        public Models.DateTimeCalc GetDateTimeCalc(int DateTimeCalcId, bool tracking)
        {
            using var db = _factory.CreateDbContext();
            if (tracking)
            {
                return db.DateTimeCalc.Find(DateTimeCalcId);
            }
            else
            {
                return db.DateTimeCalc.AsNoTracking().FirstOrDefault(item => item.DateTimeCalcId == DateTimeCalcId);
            }
        }

        public Models.DateTimeCalc AddDateTimeCalc(Models.DateTimeCalc DateTimeCalc)
        {
            using var db = _factory.CreateDbContext();
            db.DateTimeCalc.Add(DateTimeCalc);
            db.SaveChanges();
            return DateTimeCalc;
        }

        public Models.DateTimeCalc UpdateDateTimeCalc(Models.DateTimeCalc DateTimeCalc)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(DateTimeCalc).State = EntityState.Modified;
            db.SaveChanges();
            return DateTimeCalc;
        }

        public void DeleteDateTimeCalc(int DateTimeCalcId)
        {
            using var db = _factory.CreateDbContext();
            Models.DateTimeCalc DateTimeCalc = db.DateTimeCalc.Find(DateTimeCalcId);
            db.DateTimeCalc.Remove(DateTimeCalc);
            db.SaveChanges();
        }
    }
}