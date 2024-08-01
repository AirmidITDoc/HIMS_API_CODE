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
        public async Task<ApiResponse> Insert(VisitDetailModel obj)
        {
            VisitDetail model = obj.MapTo<VisitDetail>();
            if (obj.RegId == 0)
            {
                model.VisitDate = Convert.ToDateTime(obj.VisitDate);
                model.VisitTime = Convert.ToDateTime(obj.VisitTime);
            
                model.AddedBy = CurrentUserId;
                await _IAppointmentService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Appointment added successfully.");
        }

        [HttpPost("AppointmentVisitUpdate")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Update(VisitDetailModel obj)
        {
            VisitDetail model = obj.MapTo<VisitDetail>();
            if (obj.RegId == 0)
            {
                model.VisitDate = Convert.ToDateTime(obj.VisitDate);
                model.VisitTime = Convert.ToDateTime(obj.VisitTime);

                model.AddedBy = CurrentUserId;
                await _IAppointmentService.UpdateAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Appointment updated successfully.");
        }
        [HttpPost("AppointmentCancel")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Cancel(VisitDetailModel obj)
        {
            VisitDetail model = obj.MapTo<VisitDetail>();
            if (obj.RegId == 0)
            {
                model.VisitDate = Convert.ToDateTime(obj.VisitDate);
                model.VisitTime = Convert.ToDateTime(obj.VisitTime);

                model.AddedBy = CurrentUserId;
                await _IAppointmentService.CancelAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Appointment Canceled successfully.");
        }
    }
}
