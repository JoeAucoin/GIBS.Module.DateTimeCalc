using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using GIBS.Module.DateTimeCalc.Services;
using Oqtane.Controllers;
using System.Net;
using System.Threading.Tasks;

namespace GIBS.Module.DateTimeCalc.Controllers
{
    [Route(ControllerRoutes.ApiRoute)]
    public class DateTimeCalcController : ModuleControllerBase
    {
        private readonly IDateTimeCalcService _DateTimeCalcService;

        public DateTimeCalcController(IDateTimeCalcService DateTimeCalcService, ILogManager logger, IHttpContextAccessor accessor) : base(logger, accessor)
        {
            _DateTimeCalcService = DateTimeCalcService;
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<IEnumerable<Models.DateTimeCalc>> Get(string moduleid)
        {
            int ModuleId;
            if (int.TryParse(moduleid, out ModuleId) && IsAuthorizedEntityId(EntityNames.Module, ModuleId))
            {
                return await _DateTimeCalcService.GetDateTimeCalcsAsync(ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized DateTimeCalc Get Attempt {ModuleId}", moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}/{moduleid}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<Models.DateTimeCalc> Get(int id, int moduleid)
        {
            Models.DateTimeCalc DateTimeCalc = await _DateTimeCalcService.GetDateTimeCalcAsync(id, moduleid);
            if (DateTimeCalc != null && IsAuthorizedEntityId(EntityNames.Module, DateTimeCalc.ModuleId))
            {
                return DateTimeCalc;
            }
            else
            { 
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized DateTimeCalc Get Attempt {DateTimeCalcId} {ModuleId}", id, moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<Models.DateTimeCalc> Post([FromBody] Models.DateTimeCalc DateTimeCalc)
        {
            if (ModelState.IsValid && IsAuthorizedEntityId(EntityNames.Module, DateTimeCalc.ModuleId))
            {
                DateTimeCalc = await _DateTimeCalcService.AddDateTimeCalcAsync(DateTimeCalc);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized DateTimeCalc Post Attempt {DateTimeCalc}", DateTimeCalc);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                DateTimeCalc = null;
            }
            return DateTimeCalc;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<Models.DateTimeCalc> Put(int id, [FromBody] Models.DateTimeCalc DateTimeCalc)
        {
            if (ModelState.IsValid && DateTimeCalc.DateTimeCalcId == id && IsAuthorizedEntityId(EntityNames.Module, DateTimeCalc.ModuleId))
            {
                DateTimeCalc = await _DateTimeCalcService.UpdateDateTimeCalcAsync(DateTimeCalc);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized DateTimeCalc Put Attempt {DateTimeCalc}", DateTimeCalc);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                DateTimeCalc = null;
            }
            return DateTimeCalc;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}/{moduleid}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task Delete(int id, int moduleid)
        {
            Models.DateTimeCalc DateTimeCalc = await _DateTimeCalcService.GetDateTimeCalcAsync(id, moduleid);
            if (DateTimeCalc != null && IsAuthorizedEntityId(EntityNames.Module, DateTimeCalc.ModuleId))
            {
                await _DateTimeCalcService.DeleteDateTimeCalcAsync(id, DateTimeCalc.ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized DateTimeCalc Delete Attempt {DateTimeCalcId} {ModuleId}", id, moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }
    }
}
