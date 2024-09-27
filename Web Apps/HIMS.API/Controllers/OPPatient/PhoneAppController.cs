using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.OutPatient;
using HIMS.Data.Models;
using HIMS.Services.OutPatient;
using Microsoft.AspNetCore.Mvc;
using static HIMS.API.Models.OutPatient.phoneAppModelValidator;

namespace HIMS.API.Controllers.OPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PhoneAppController : BaseController
    {
        //private readonly IPhoneAppService _IPhoneAppService;
        //public PhoneAppController(IPhoneAppService repository)
        //{
        //    _IPhoneAppService = repository;
        //}

        //[HttpPost("InsertSP")]
        ////[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> Insert(phoneAppModel obj)
        //{
        //    TPhoneAppointment model = obj.MapTo<TPhoneAppointment>();
        //    if (obj.PhoneAppId == 0)
        //    {
        //        model.AppDate = Convert.ToDateTime(obj.AppDate);
        //        model.AppTime = Convert.ToDateTime(obj.AppTime);

        //        model.UpdatedBy = CurrentUserId;
        //        model = await _IPhoneAppService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PhoneApp added successfully.", model);
        //}
        //[HttpPost("InsertEDMX")]
        ////[Permission(PageCode = "ItemMaster", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> InsertEDMX(phoneAppModel obj)
        //{
        //    TPhoneAppointment model = obj.MapTo<TPhoneAppointment>();
        //    if (obj.PhoneAppId == 0)
        //    {
        //        model.CreatedDate = DateTime.Now;
        //        model.CreatedBy = CurrentUserId;
        //        //model.IsActive = true;
        //        model.AddedBy = CurrentUserId;
        //        model.UpdatedBy = CurrentUserId;
        //        await _IPhoneAppService.InsertAsync(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PhoneApp   added successfully.");
        //}
        //[HttpPut("Edit/{id:int}")]
        ////[Permission(PageCode = "ItemMaster", Permission = PagePermission.Edit)]
        //public async Task<ApiResponse> Edit(phoneAppModel obj)
        //{
        //    TPhoneAppointment model = obj.MapTo<TPhoneAppointment>();
        //    if (obj.PhoneAppId == 0)
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    else
        //    {
        //        model.AppTime = Convert.ToDateTime(obj.AppTime);
        //        await _IPhoneAppService.UpdateAsync(model, CurrentUserId, CurrentUserName);
        //    }
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PhoneApp updated successfully.");
        //}
        //[HttpPost("PhoneAppCanceled")]
        ////[Permission(PageCode = "TestMaster", Permission = PagePermission.Delete)]
        //public async Task<ApiResponse> Cancel(PhoneAppointmentCancel obj)
        //{
        //    TPhoneAppointment model = new();
        //    if (obj.PhoneAppId != 0)
        //    {
        //        model.PhoneAppId = obj.PhoneAppId;
        //        model.CreatedBy = CurrentUserId;
        //        model.CreatedDate = DateTime.Now;
        //        await _IPhoneAppService.CancelAsync(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PhoneApp Canceled successfully.");
        //}
    }
}

