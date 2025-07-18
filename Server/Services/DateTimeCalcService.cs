using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Oqtane.Models;
using Oqtane.Security;
using Oqtane.Shared;
using GIBS.Module.DateTimeCalc.Repository;

namespace GIBS.Module.DateTimeCalc.Services
{
    public class ServerDateTimeCalcService : IDateTimeCalcService
    {
        private readonly IDateTimeCalcRepository _DateTimeCalcRepository;
        private readonly IUserPermissions _userPermissions;
        private readonly ILogManager _logger;
        private readonly IHttpContextAccessor _accessor;
        private readonly Alias _alias;

        public ServerDateTimeCalcService(IDateTimeCalcRepository DateTimeCalcRepository, IUserPermissions userPermissions, ITenantManager tenantManager, ILogManager logger, IHttpContextAccessor accessor)
        {
            _DateTimeCalcRepository = DateTimeCalcRepository;
            _userPermissions = userPermissions;
            _logger = logger;
            _accessor = accessor;
            _alias = tenantManager.GetAlias();
        }

        public Task<List<Models.DateTimeCalc>> GetDateTimeCalcsAsync(int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_DateTimeCalcRepository.GetDateTimeCalcs(ModuleId).ToList());
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized DateTimeCalc Get Attempt {ModuleId}", ModuleId);
                return null;
            }
        }

        public Task<Models.DateTimeCalc> GetDateTimeCalcAsync(int DateTimeCalcId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_DateTimeCalcRepository.GetDateTimeCalc(DateTimeCalcId));
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized DateTimeCalc Get Attempt {DateTimeCalcId} {ModuleId}", DateTimeCalcId, ModuleId);
                return null;
            }
        }

        public Task<Models.DateTimeCalc> AddDateTimeCalcAsync(Models.DateTimeCalc DateTimeCalc)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, DateTimeCalc.ModuleId, PermissionNames.Edit))
            {
                DateTimeCalc = _DateTimeCalcRepository.AddDateTimeCalc(DateTimeCalc);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "DateTimeCalc Added {DateTimeCalc}", DateTimeCalc);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized DateTimeCalc Add Attempt {DateTimeCalc}", DateTimeCalc);
                DateTimeCalc = null;
            }
            return Task.FromResult(DateTimeCalc);
        }

        public Task<Models.DateTimeCalc> UpdateDateTimeCalcAsync(Models.DateTimeCalc DateTimeCalc)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, DateTimeCalc.ModuleId, PermissionNames.Edit))
            {
                DateTimeCalc = _DateTimeCalcRepository.UpdateDateTimeCalc(DateTimeCalc);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "DateTimeCalc Updated {DateTimeCalc}", DateTimeCalc);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized DateTimeCalc Update Attempt {DateTimeCalc}", DateTimeCalc);
                DateTimeCalc = null;
            }
            return Task.FromResult(DateTimeCalc);
        }

        public Task DeleteDateTimeCalcAsync(int DateTimeCalcId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.Edit))
            {
                _DateTimeCalcRepository.DeleteDateTimeCalc(DateTimeCalcId);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "DateTimeCalc Deleted {DateTimeCalcId}", DateTimeCalcId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized DateTimeCalc Delete Attempt {DateTimeCalcId} {ModuleId}", DateTimeCalcId, ModuleId);
            }
            return Task.CompletedTask;
        }
    }
}
