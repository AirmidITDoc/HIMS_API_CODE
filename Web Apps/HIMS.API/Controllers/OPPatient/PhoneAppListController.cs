using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.OPPatient;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.OPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PhoneAppListController : BaseController
    {
        private readonly IPhoneAppointment1Service _IPhoneAppListService;
        public PhoneAppListController(IPhoneAppointment1Service repository)
        {
            _IPhoneAppListService = repository;
        }
        [HttpPost("InsertSP")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertAsyncSP(PhoneAppListModel obj)
        {
            TPhoneAppointment model = obj.MapTo<TPhoneAppointment>();
            if (obj.PhoneAppId == 0)
            {
                model.PhAppDate = Convert.ToDateTime(obj.PhAppDate);
                model.PhAppTime = Convert.ToDateTime(obj.PhAppTime);
                model.AddedBy = CurrentUserId;
                await _IPhoneAppListService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PhoneApp List  added successfully.");
        }
    }
}
