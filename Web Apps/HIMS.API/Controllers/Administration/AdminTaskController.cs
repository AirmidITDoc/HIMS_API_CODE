using HIMS.Data.Models;
using HIMS.Data;
using HIMS.Services.Administration;
using Microsoft.AspNetCore.Mvc;
using HIMS.Api.Controllers;
using Asp.Versioning;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.OutPatient;
using HIMS.Core;
using HIMS.API.Models.Administration;

namespace HIMS.API.Controllers.Administration
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class AdminTaskController : BaseController
    {
        private readonly IAdminTaskService _IAdminTaskService;
      
        public AdminTaskController(IAdminTaskService repository)
        {
            _IAdminTaskService = repository;
         
        }
        [HttpPut("UpdateBilldatetime{id:int}")]
        //[Permission(PageCode = "Administration", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> BilldatetimeUpdate(BilllsModel obj)
        {
            Bill model = obj.MapTo<Bill>();
            if (obj.BillNo == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {

                await _IAdminTaskService.BilldateUpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }

        //[HttpPut("UpdateAdmissiondatetime{id:int}")]
        ////[Permission(PageCode = "Administration", Permission = PagePermission.Edit)]
        //public ApiResponse Update(AdmissionModell obj)
        //{
        //    Admission model = obj.MapTo<Admission>();
        //    if (obj.AdmissionID == 0)
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    else
        //    {

        //        _IAdminTaskService.Update(model, CurrentUserId, CurrentUserName);
        //    }
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        //}


    }
}
