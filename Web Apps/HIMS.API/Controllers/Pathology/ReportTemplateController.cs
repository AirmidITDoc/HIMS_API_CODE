using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.OPPatient;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
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

        private readonly IGenericService<MReportTemplateConfig> _repository;
        public ReportTemplateController(IReportTemplateService repository, IGenericService<MReportTemplateConfig> repository1)
        {
            _IReportTemplateService = repository;

            _repository = repository1;
        }
        [HttpPost("ReportTemplateList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<ReportTemplateListDto> ReportTemplateList = await _IReportTemplateService.ReportTemplateList(objGrid);
            return Ok(ReportTemplateList.ToGridResponse(objGrid, "ReportTemplate App List"));
        }

        [HttpGet("{id?}")]
        // [Permission(PageCode = "Appointment", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {

            var data1 = await _repository.GetById(x => x.TemplateId == id);
            return data1.ToSingleResponse<MReportTemplateConfig, MReportTemplateConfig>("Template Details");
        }


      


        [HttpGet]
        [Route("get-Templates")]
        //[Permission(PageCode = "StateMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetDropdown()
        {
            var MMasterList = await _repository.GetAll();
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Template dropdown", MMasterList.Select(x => new { x.TemplateId, x.TemplateName, x.TemplateDescription }));
        }
    }
}
