using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Pathology;
using HIMS.Services.Inventory;
using HIMS.Services.Pathlogy;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Pathology
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ReportTemplateController : BaseController
    {
        private readonly IReportTemplateService _IReportTemplateService;
        public ReportTemplateController(IReportTemplateService repository)
        {
            _IReportTemplateService = repository;
        }
        [HttpPost("ReportTemplateList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<ReportTemplateListDto> ReportTemplateList = await _IReportTemplateService.ReportTemplateList(objGrid);
            return Ok(ReportTemplateList.ToGridResponse(objGrid, "ReportTemplate App List"));
        }
    }
}
