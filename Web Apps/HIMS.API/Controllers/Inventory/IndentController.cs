using Asp.Versioning;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Pharmacy;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Services.Pharmacy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security;
using HIMS.API.Models.Inventory;
using HIMS.Api.Controllers;
using HIMS.Services.Inventory;
using HIMS.API.Models.OPPatient;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;

namespace HIMS.API.Controllers.Inventory
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class IndentController : BaseController
    {

        public readonly IIndentService _IIndentService;
        public IndentController(IIndentService repository)
        {
            _IIndentService = repository;
        }


        [HttpPost("IndentList")]
        [Permission(PageCode = "Indent", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<IndentListDto> AppList = await _IIndentService.GetListAsync(objGrid);
            return Ok(AppList.ToGridResponse(objGrid, "Indent List"));
        }

        [HttpPost("IndentDetailsList")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.View)]
        public async Task<IActionResult> IndentDetailsList(GridRequestModel objGrid)
        {
            IPagedList<IndentItemListDto> IndentDetailsList = await _IIndentService.GetIndentItemListAsync(objGrid);
            return Ok(IndentDetailsList.ToGridResponse(objGrid, "Indent Item Detail List"));
        }
        [HttpPost("Insert")]
        [Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(IndentModel obj)
        {
            TIndentHeader model = obj.MapTo<TIndentHeader>();
            if (obj.IndentId == 0)
            {
                model.IndentDate = Convert.ToDateTime(obj.IndentDate);
                model.IndentTime = Convert.ToDateTime(obj.IndentTime);
                model.Addedby = CurrentUserId;
                await _IIndentService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Indent added successfully.");
        }

        [HttpPut("Edit/{id:int}")]
        [Permission(PageCode = "Indent", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(IndentModel obj)
        {
            TIndentHeader model = obj.MapTo<TIndentHeader>();
            if (obj.IndentId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.IndentDate = Convert.ToDateTime(obj.IndentDate);
                model.IndentTime = Convert.ToDateTime(obj.IndentTime);
                await _IIndentService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Indent updated successfully.");
        }

        [HttpPost("Verify")]
        [Permission(PageCode = "Indent", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Verify(IndentVerifyModel obj)
        {
            TIndentHeader model = obj.MapTo<TIndentHeader>();
            if (obj.IndentId != 0)
            {
                model.IsInchargeVerify = true;
                model.IsInchargeVerifyDate = DateTime.Now.Date;
                await _IIndentService.VerifyAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Indent verify successfully.");
        }

        
        [HttpPost("Cancel")]
        [Permission(PageCode = "VisitDetail", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Cancel(IndentCancel obj)
        {
            TIndentHeader model = new();
            if (obj.IndentId != 0)
            {
                model.IndentId = obj.IndentId;
                model.Isclosed = true;
                //model.IsCancelledDate = DateTime.Now;
                await _IIndentService.CancelAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Indent Canceled successfully.");
        }


        [HttpPost("OldIndentList")]
        //[Permission(PageCode = "PurchaseOrder", Permission = PagePermission.View)]
        public async Task<IActionResult> GetOldIndentList(GridRequestModel objGrid)
        {
            IPagedList<IndentItemListDto> List1 = await _IIndentService.GetOldIndentAsync(objGrid);
            return Ok(List1.ToGridResponse(objGrid, "Indent List"));
        }
    }
}
