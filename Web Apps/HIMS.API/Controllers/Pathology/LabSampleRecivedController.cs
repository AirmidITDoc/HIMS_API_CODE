using HIMS.Api.Controllers;
using HIMS.Data.Models;
using HIMS.Data;
using HIMS.Services.Pharmacy;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.OutPatient;
using HIMS.Core;
using HIMS.Services.Pathlogy;
using HIMS.API.Models.Pathology;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Pathology;
using Asp.Versioning;

namespace HIMS.API.Controllers.Pathology
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class LabSampleRecivedController : BaseController
    {
        private readonly ILabSampleRecivedService _ILabSampleRecivedService;
        public LabSampleRecivedController(ILabSampleRecivedService repository)
        {
            _ILabSampleRecivedService = repository;
        }
        [HttpPost("LabSampleRecivedList")]
        [Permission]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<LabSampleRecivedListDto> LabSampleRecivedList = await _ILabSampleRecivedService.LabGetListAsync(objGrid);
            return Ok(LabSampleRecivedList.ToGridResponse(objGrid, "LabSampleRecived List"));
        }


        //[HttpPut("LabSampleRecivedUpdate")]
        ////[Permission(PageCode = "Administration", Permission = PagePermission.Edit)]
        //public ApiResponse Update(PathologyLabReportHeader obj)
        //{
        //    List<TPathologyReportHeader> model = obj.PathologyLabReport.MapTo<List<TPathologyReportHeader>>();
        //    if (model.Count > 0)
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    else
        //    {
        //        _ILabSampleRecivedService.Update(model, CurrentUserId, CurrentUserName);
        //    }
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        //}
        [HttpPut("LabSampleRecivedUpdate")]
        public ApiResponse Update(PathologyLabReportHeader obj)
        {
            if (obj?.PathologyLabReport == null || !obj.PathologyLabReport.Any())
            {
                return ApiResponseHelper.GenerateResponse(  ApiStatusCode.Status500InternalServerError,  "Invalid params");
            }

            List<TPathologyReportHeader> model = obj.PathologyLabReport .MapTo<List<TPathologyReportHeader>>();

            _ILabSampleRecivedService.Update( model,  CurrentUserId,  CurrentUserName);

            return ApiResponseHelper.GenerateResponse(  ApiStatusCode.Status200OK, "Record updated successfully.");
        }




    }
}
