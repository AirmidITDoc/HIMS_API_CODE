using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Masters;
using HIMS.API.Models.OutPatient;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using HIMS.Services.OutPatient;
using Microsoft.AspNetCore.Mvc;
using static HIMS.API.Models.Inventory.PathTestDetailModelModelValidator;

namespace HIMS.API.Controllers.Inventory
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class LoginManagerController : BaseController
    {
        private readonly ILoginService _ILoginService;
        public LoginManagerController(ILoginService repository)
        {
            _ILoginService = repository;
        } 

        [HttpPost("Insert")]
        [Permission(PageCode = "Login", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(LoginManagerModel obj)
        {
            LoginManager model = obj.MapTo<LoginManager>();
            if (obj.UserId == 0)
            {
                model.AddedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                model.IsActive = true;
                await _ILoginService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "LoginManager added successfully.");
        }

        [HttpPut("Edit/{id:int}")]
        [Permission(PageCode = "Login", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(LoginManagerModel obj)
        {
            LoginManager model = obj.MapTo<LoginManager>();
            model.IsActive = true;
            if (obj.UserId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.AddedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _ILoginService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "LoginManager updated successfully.");
        }

        [HttpPost("LoginCanceled")]
        [Permission(PageCode = "Login", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Cancel(loginCancel obj)
        {
            LoginManager model = new();
            if (obj.UserId != 0)
            {
                model.UserId = obj.UserId;
                model.ModifiedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _ILoginService.CancelAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "LoginManager Canceled successfully.");
        }
        [HttpPost("updatepassword")]
        [Permission(PageCode = "Login", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> updatepassAsync(ChangePassword obj)
        {
            LoginManager model = new();
            if (obj.UserId != 0)
            {
                model.UserId = obj.UserId;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _ILoginService.updatepassAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "updatepassword successfully.");
        }

    }
}
