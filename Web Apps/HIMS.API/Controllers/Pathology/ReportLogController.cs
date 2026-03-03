using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Pathology;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Pathology
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ReportLogController : BaseController
    {
        private readonly IGenericService<TReportLog> _repository;
        public ReportLogController(IGenericService<TReportLog> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "ReportLog", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<TReportLog> ReportLogList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(ReportLogList.ToGridResponse(objGrid, "Report Log List"));
        }

        [HttpPost]
        //[Permission(PageCode = "ReportLog", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(ReportLogModel obj)
        {
            TReportLog model = obj.MapTo<TReportLog>();
            if (obj.LogId == 0)
            {
                model.LogDate = AppTime.Now.Date;
                model.LogTime = AppTime.Now;
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }
    }

}
