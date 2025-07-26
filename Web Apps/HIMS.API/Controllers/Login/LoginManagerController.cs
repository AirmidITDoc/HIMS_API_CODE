using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Masters;
using HIMS.API.Models.OutPatient;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using HIMS.Services.OutPatient;
using Microsoft.AspNetCore.Mvc;
using static HIMS.API.Models.Inventory.PathTestDetailModelModelValidator;

namespace HIMS.API.Controllers.Login
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
        [HttpPost("LoginList")]
        [Permission(PageCode = "Login", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<LoginManagerListDto> LoginManagerList = await _ILoginService.GetListAsync(objGrid);
            return Ok(LoginManagerList.ToGridResponse(objGrid, "User List"));
        }
        [HttpPost("loginAccessDetailsList")]
        [Permission(PageCode = "Login", Permission = PagePermission.View)]
        public async Task<IActionResult> Listl(GridRequestModel objGrid)
        {
            IPagedList<LoginConfigUserWiseListDto> LoginConfigUserWiseList = await _ILoginService.GetListAsyncL(objGrid);
            return Ok(LoginConfigUserWiseList.ToGridResponse(objGrid, "loginAccessDetailsList "));
        }

        [HttpPost("LoginStoreUserWiseList")]
        [Permission(PageCode = "Login", Permission = PagePermission.View)]
        public async Task<IActionResult> ListC(GridRequestModel objGrid)
        {
            IPagedList<LoginStoreUserWiseListDto> LoginStoreUserWiseList = await _ILoginService.GetListAsyncLC(objGrid);
            return Ok(LoginStoreUserWiseList.ToGridResponse(objGrid, "LoginConfigUserWiseList "));
        }

        [HttpPost("LoginAccessConfigList")]
        [Permission(PageCode = "Login", Permission = PagePermission.View)]
        public async Task<IActionResult> ListA(GridRequestModel objGrid)
        {
            IPagedList<LoginAccessConfigListDto> LoginAccessConfigList = await _ILoginService.GetListAsyncLA(objGrid);
            return Ok(LoginAccessConfigList.ToGridResponse(objGrid, "LoginConfigUserWiseList "));
        }
        [HttpPost("Insert")]
        [Permission(PageCode = "Login", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(LoginManagerModel obj)
        {
            LoginManager model = obj.MapTo<LoginManager>();
            if (obj.UserId == 0)
            {
                foreach (var q in model.TLoginAccessDetails)
                {
                    q.CreatedBy = CurrentUserId;
                    q.CreatedDate = DateTime.Now;

                }
                foreach (var v in model.TLoginUnitDetails)
                {
                    v.CreatedBy = CurrentUserId;
                    v.CreatedDate = DateTime.Now;

                }
                foreach (var p in model.TLoginStoreDetails)
                {
                    p.CreatedBy = CurrentUserId;
                    p.CreatedDate = DateTime.Now;
                }
                model.AddedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                model.IsActive = true;
                await _ILoginService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "User added successfully.");
        }

       
        [HttpPut("Edit/{id:int}")]
        [Permission(PageCode = "Login", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(LoginManagerModel obj)
        {
            if (obj.UserId <= 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }

            LoginManager model = obj.MapTo<LoginManager>();
            model.IsActive = true;

            foreach (var q in model.TLoginAccessDetails)
            {
                if (q.LoginId == 0)
                {
                    q.CreatedBy = CurrentUserId;
                    q.CreatedDate = DateTime.Now;
                }
                q.ModifiedBy = CurrentUserId;
                q.ModifiedDate = DateTime.Now;
                q.LoginId = 0;
            }

            foreach (var v in model.TLoginUnitDetails)
            {
                if (v.LoginId == 0)
                {
                    v.CreatedBy = CurrentUserId;
                    v.CreatedDate = DateTime.Now;
                }
                v.ModifiedBy = CurrentUserId;
                v.ModifiedDate = DateTime.Now;
                v.LoginId = 0;
            }

            foreach (var p in model.TLoginStoreDetails)
            {
                if (p.LoginId == 0)
                {
                    p.CreatedBy = CurrentUserId;
                    p.CreatedDate = DateTime.Now;
                }
                p.ModifiedBy = CurrentUserId;
                p.ModifiedDate = DateTime.Now;
                p.LoginId = 0;
            }

            model.ModifiedBy = CurrentUserId;
            await _ILoginService.UpdateAsync(model, CurrentUserId, CurrentUserName);

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "User updated successfully.");
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
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "User Canceled successfully.");
        }
       
        [HttpPost("updatepassword")]
        [Permission(PageCode = "Login", Permission = PagePermission.Add)]
        public async Task<ApiResponse> updatepassAsync(ChangePassword obj)
        {
            if (obj.UserId == 0 || string.IsNullOrWhiteSpace(obj.UserName) || string.IsNullOrWhiteSpace(obj.Password))
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }

            LoginManager model = new()
            {
                UserId = obj.UserId,
                UserName = obj.UserName,       
                Password = obj.Password,         
                ModifiedBy = CurrentUserId,
                ModifiedDate = DateTime.Now
            };

            await _ILoginService.updatepassAsync(model, CurrentUserId, CurrentUserName);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Password updated successfully.");
        }
        //[HttpPut("updatepassword/{id:int}")]
        ////[Permission(PageCode = "Login", Permission = PagePermission.Edit)]
        //public async Task<ApiResponse> updatepassAsync(ChangePassword obj)
        //{
        //    LoginManager model = new();
        //    if (obj.UserId != 0)
        //    {
        //        model.UserId = obj.UserId;
        //        model.ModifiedBy = CurrentUserId;
        //        model.ModifiedDate = DateTime.Now;
        //        await _ILoginService.updatepassAsync(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "password Updated successfully.");
        //}


    }
}
