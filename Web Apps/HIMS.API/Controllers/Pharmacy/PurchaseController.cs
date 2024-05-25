using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.API.Models.Pharmacy;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Services.Pharmacy;
using HIMS.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Pharmacy
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PurchaseController : BaseController
    {
        private readonly IPurchaseService _IPurchaseService;
        public PurchaseController(IPurchaseService repository)
        {
            _IPurchaseService = repository;
        }
        [HttpPost("Insert")]
        //[Permission(PageCode = "Purchase", Permission = PagePermission.Add)]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public async Task<ApiResponse> Insert(PurchaseModel obj)
        {
            TPurchaseHeader model = obj.MapTo<TPurchaseHeader>();
            if (obj.PurchaseId == 0)
            {
                model.PurchaseDate = DateTime.Now.Date;
                model.PurchaseTime = DateTime.Now;
                model.AddedBy = CurrentUserId;
                model.UpdatedBy = 0;
                await _IPurchaseService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Purchase added successfully.");
        }

        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "Purchase", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(PurchaseModel obj)
        {
            TPurchaseHeader model = obj.MapTo<TPurchaseHeader>();
            if (obj.PurchaseId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.PurchaseDate = DateTime.Now.Date;
                model.PurchaseTime = DateTime.Now;
                model.UpdatedBy = CurrentUserId;
                await _IPurchaseService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Purchase updated successfully.");
        }

        [HttpPost("Verify")]
        //[Permission(PageCode = "Purchase", Permission = PagePermission.Edit)]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public async Task<ApiResponse> Verify(PurchaseVerifyModel obj)
        {
            TPurchaseHeader model = obj.MapTo<TPurchaseHeader>();
            if (obj.PurchaseId != 0)
            {
                model.IsVerified = true;
                model.VerifiedDateTime = DateTime.Now.Date;
                await _IPurchaseService.VerifyAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Purchase verify successfully.");
        }
    }
}
