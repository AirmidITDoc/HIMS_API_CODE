using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Models.IPPatient;
using HIMS.Services.IPPatient;
using HIMS.API.Models.Nursing;

namespace HIMS.API.Controllers.IPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class DischargeSummaryController : BaseController
    {
        private readonly IDischargeSummaryService _IDischargeSummaryService;
        public DischargeSummaryController(IDischargeSummaryService repository)
        {
            _IDischargeSummaryService = repository;
        }


        [HttpPost("Insert")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(DischargeSummaryModel obj)
        {
            DischargeSummary model = obj.MapTo<DischargeSummary>();
            if (obj.DischargeSummaryId == 0)
            {
                model.DischargeSummaryDate = Convert.ToDateTime(obj.DischargeSummaryDate);
                model.DischargeSummaryTime = Convert.ToDateTime(obj.DischargeSummaryTime);
                model.AddedBy = CurrentUserId;
                await _IDischargeSummaryService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DischargeSummary added successfully.");
        }
         [HttpPut("Update")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(DischargeSummaryModel obj)
        {
            DischargeSummary model = obj.MapTo<DischargeSummary>();
            if (obj.DischargeSummaryId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.DischargeSummaryDate = Convert.ToDateTime(obj.DischargeSummaryDate);
                model.DischargeSummaryTime = Convert.ToDateTime(obj.DischargeSummaryTime);
                await _IDischargeSummaryService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DischargeSummary updated successfully.");
        }

    }

}
