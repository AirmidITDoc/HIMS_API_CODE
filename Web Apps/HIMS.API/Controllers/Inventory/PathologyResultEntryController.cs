using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Inventory
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PathologyResultEntryController : BaseController
    {
        private readonly IPathologyResultEntryService _IPathologyResultEntryService;
        public PathologyResultEntryController(IPathologyResultEntryService repository)
        {
            _IPathologyResultEntryService = repository;
        }
        [HttpPost("InsertResultEntry")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(PathologyResultEntryModel obj)
        {
            TPathologyReportDetail model = obj.MapTo<TPathologyReportDetail>();
            if (obj.PathReportDetId == 0)
            {
                //model.PathDate = Convert.ToDateTime(obj.PathDate);
                //model.AddedBy = CurrentUserId;
              
                await _IPathologyResultEntryService.InsertAsyncResultEntry(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Pathology Result Entry added successfully.");
        }


      
        [HttpPost("InsertTemplateResult")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(PathTemplateResultModel obj)
        {
            TPathologyReportTemplateDetail model = obj.MapTo<TPathologyReportTemplateDetail>();
            if (obj.PathReportTemplateDetId == 0)
            {
                //model.PathDate = Convert.ToDateTime(obj.PathDate);
                //model.AddedBy = CurrentUserId;

                await _IPathologyResultEntryService.InsertAsyncTemplateResult(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Pathology Template Result  added successfully.");
        }


        [HttpPost("Cancel")]
        //[Permission(PageCode = "VisitDetail", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Cancel(PathologyResultEntryModel obj)
        {
            TPathologyReportDetail model = new();
            if (obj.PathReportDetId != 0)
            {
                //model.IndentId = obj.IndentId;
                //model.Isclosed = true;
                //model.IsCancelledDate = DateTime.Now;
                await _IPathologyResultEntryService.CancelAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Pathology Result Entry Canceled successfully.");
        }
    }
}
