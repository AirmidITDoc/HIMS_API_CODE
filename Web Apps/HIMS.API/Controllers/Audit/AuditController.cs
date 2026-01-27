using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services.Audit;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Audit
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class AuditController : BaseController
    {
        private readonly IAuditService _auditService;
        public AuditController(IAuditService auditService)
        {
            _auditService = auditService;
        }
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "Audit", Permission = PagePermission.View)]
        [Permission]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            //var sessionId = Context.StoreId;
            //var unitId = Context.UnitId;
            IPagedList<AuditLog> DocList = await _auditService.GetAllPagedAsync(objGrid);
            return Ok(DocList.ToGridResponse(objGrid, "Audit List"));
        }
    }
}
