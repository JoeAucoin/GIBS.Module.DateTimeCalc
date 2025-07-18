using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Oqtane.Modules;
using Oqtane.Models;
using Oqtane.Infrastructure;
using Oqtane.Interfaces;
using Oqtane.Enums;
using Oqtane.Repository;
using GIBS.Module.DateTimeCalc.Repository;
using System.Threading.Tasks;

namespace GIBS.Module.DateTimeCalc.Manager
{
    public class DateTimeCalcManager : MigratableModuleBase, IInstallable, IPortable, ISearchable
    {
        private readonly IDateTimeCalcRepository _DateTimeCalcRepository;
        private readonly IDBContextDependencies _DBContextDependencies;

        public DateTimeCalcManager(IDateTimeCalcRepository DateTimeCalcRepository, IDBContextDependencies DBContextDependencies)
        {
            _DateTimeCalcRepository = DateTimeCalcRepository;
            _DBContextDependencies = DBContextDependencies;
        }

        public bool Install(Tenant tenant, string version)
        {
            return Migrate(new DateTimeCalcContext(_DBContextDependencies), tenant, MigrationType.Up);
        }

        public bool Uninstall(Tenant tenant)
        {
            return Migrate(new DateTimeCalcContext(_DBContextDependencies), tenant, MigrationType.Down);
        }

        public string ExportModule(Oqtane.Models.Module module)
        {
            string content = "";
            List<Models.DateTimeCalc> DateTimeCalcs = _DateTimeCalcRepository.GetDateTimeCalcs(module.ModuleId).ToList();
            if (DateTimeCalcs != null)
            {
                content = JsonSerializer.Serialize(DateTimeCalcs);
            }
            return content;
        }

        public void ImportModule(Oqtane.Models.Module module, string content, string version)
        {
            List<Models.DateTimeCalc> DateTimeCalcs = null;
            if (!string.IsNullOrEmpty(content))
            {
                DateTimeCalcs = JsonSerializer.Deserialize<List<Models.DateTimeCalc>>(content);
            }
            if (DateTimeCalcs != null)
            {
                foreach(var DateTimeCalc in DateTimeCalcs)
                {
                    _DateTimeCalcRepository.AddDateTimeCalc(new Models.DateTimeCalc { ModuleId = module.ModuleId, Name = DateTimeCalc.Name });
                }
            }
        }

        public Task<List<SearchContent>> GetSearchContentsAsync(PageModule pageModule, DateTime lastIndexedOn)
        {
           var searchContentList = new List<SearchContent>();

           foreach (var DateTimeCalc in _DateTimeCalcRepository.GetDateTimeCalcs(pageModule.ModuleId))
           {
               if (DateTimeCalc.ModifiedOn >= lastIndexedOn)
               {
                   searchContentList.Add(new SearchContent
                   {
                       EntityName = "GIBSDateTimeCalc",
                       EntityId = DateTimeCalc.DateTimeCalcId.ToString(),
                       Title = DateTimeCalc.Name,
                       Body = DateTimeCalc.Name,
                       ContentModifiedBy = DateTimeCalc.ModifiedBy,
                       ContentModifiedOn = DateTimeCalc.ModifiedOn
                   });
               }
           }

           return Task.FromResult(searchContentList);
        }
    }
}
