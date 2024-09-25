using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.OutPatient;
using HIMS.Data.Models;
using HIMS.Services.IPPatient;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.IPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]

    public class AdmissionController : BaseController
    {

        private readonly IAdmissionService _IAdmissionService;
        public AdmissionController(IAdmissionService repository)
        {
            _IAdmissionService = repository;
        }


        [HttpPost("AdmissionInsertSP")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> AdmissionInsertSP(NewAdmission obj)
        {
            Registration model = obj.AdmissionReg.MapTo<Registration>();
            Admission objAdmission = obj.ADMISSION.MapTo<Admission>();
            if (obj.AdmissionReg.RegId == 0)
            {
                model.RegTime = Convert.ToDateTime(obj.AdmissionReg.RegTime);
                model.AddedBy = CurrentUserId;

                //if (obj.Visit.VisitId == 0)
                //{
                //    objVisitDetail.VisitTime = Convert.ToDateTime(obj.Visit.VisitTime);
                //    objVisitDetail.AddedBy = CurrentUserId;
                //    objVisitDetail.UpdatedBy = CurrentUserId;
                //}
                await _IAdmissionService.InsertAsyncSP(model, objAdmission, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Admission added successfully.");
        }


        [HttpPost("AdmissionRegisteredInsertSP")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> AdmissionRegisteredInsertSP(NewAdmission obj)
        {

            Admission objAdmission = obj.ADMISSION.MapTo<Admission>();

            await _IAdmissionService.InsertRegAsyncSP(objAdmission, CurrentUserId, CurrentUserName);

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Registered Admission added successfully.");
        }


        [HttpPost("AdmissionUpdateSP")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> AdmissionUpdateSP(NewAdmission obj)
        {

            Admission objAdmission = obj.ADMISSION.MapTo<Admission>();

            await _IAdmissionService.UpdateAdmissionAsyncSP(objAdmission, CurrentUserId, CurrentUserName);

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, " Admission Updated successfully.");
        }
    }
}
