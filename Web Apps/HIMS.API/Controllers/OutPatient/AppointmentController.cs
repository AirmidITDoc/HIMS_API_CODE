using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.API.Models.OutPatient;
using HIMS.API.Models.Pharmacy;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services.OutPatient;
using HIMS.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security;

namespace HIMS.API.Controllers.OutPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class AppointmentController : BaseController
    {
        private readonly IAppointmentService _IAppointmentService;
        public AppointmentController(IAppointmentService repository)
        {
            _IAppointmentService = repository;
        }

        [HttpPost("AppointmentInsert")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(AppointmentReqDto obj)
        {
            Registration model = obj.Registration.MapTo<Registration>();
            VisitDetail objVisitDetail = obj.Visit.MapTo<VisitDetail>();
            if (obj.Registration.RegID == 0)
            {
                model.RegDate = Convert.ToDateTime(obj.Registration.RegDate);
                model.RegTime = Convert.ToDateTime(obj.Registration.RegTime);
                model.AddedBy = CurrentUserId;

                if (obj.Visit.VisitId == 0)
                {
                    objVisitDetail.VisitDate = Convert.ToDateTime(obj.Visit.VisitDate);
                    objVisitDetail.VisitTime = Convert.ToDateTime(obj.Visit.VisitTime);
                    objVisitDetail.AddedBy = CurrentUserId;
                    objVisitDetail.UpdatedBy = CurrentUserId;
                }
                await _IAppointmentService.InsertAsyncSP(model, objVisitDetail, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Appointment added successfully.");
        }

        //[HttpPost("AppointmentVisitUpdate")]
        ////[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> Update(AppointmentReqDto obj)
        //{
        //    VisitDetail model = obj.MapTo<VisitDetail>();
        //    if (obj.RegId == 0)
        //    {
        //        model.VisitDate = Convert.ToDateTime(obj.VisitDate);
        //        model.VisitTime = Convert.ToDateTime(obj.VisitTime);

        //        model.AddedBy = CurrentUserId;
        //        await _IAppointmentService.UpdateAsyncSP(model, objVisitDetail, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Appointment updated successfully.");
        //}
        [HttpPost("AppointmentCancel")]
        //[Permission(PageCode = "VisitDetail", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Cancel(CancelAppointment obj)
        {
            VisitDetail model = new VisitDetail();
            if (obj.PatVisitID != 0)
            {
                model.VisitId = obj.PatVisitID;
                model.IsCancelled = true;
                model.IsCancelledBy = CurrentUserId;
                model.IsCancelledDate = DateTime.Now;
                await _IAppointmentService.CancelAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Appointment Canceled successfully.");
        }
    }
}
