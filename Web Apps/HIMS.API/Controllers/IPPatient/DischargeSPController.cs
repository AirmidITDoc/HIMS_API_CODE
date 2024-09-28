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

namespace HIMS.API.Controllers.IPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class DischargeSPController : BaseController
    {
        private readonly IDischargeServiceSP _IPDischargeService;
        public DischargeSPController(IDischargeServiceSP repository)
        {
            _IPDischargeService = repository;
        }

        [HttpPost("IPDischargeInsert")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> IPDischargeInsert(NewDischarge obj)
        {
            Discharge model = obj.DischargeModel.MapTo<Discharge>();
            Admission objAdmission = obj.DischargeAdmissionModel.MapTo<Admission>();
            if (obj.DischargeModel.DischargeId == 0)
            {
                model.DischargeTime = Convert.ToDateTime(obj.DischargeModel.DischargeTime);
                model.AddedBy = CurrentUserId;

                if (obj.DischargeModel.DischargeId == 0)
                {
                    objAdmission.DischargeTime = Convert.ToDateTime(obj.DischargeModel.DischargeTime);
                    objAdmission.AddedBy = CurrentUserId;
                    // objVisitDetail.UpdatedBy = CurrentUserId;
                }
                await _IPDischargeService.InsertAsyncSP(model, objAdmission, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Discharge added successfully.");
        }

        [HttpPost("IPDischargeUpdate")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> IPDischargeUpdate(NewDischarge obj)
        {
            Discharge model = obj.DischargeModel.MapTo<Discharge>();
            Admission objAdmission = obj.DischargeAdmissionModel.MapTo<Admission>();
            if (obj.DischargeModel.DischargeId != 0)
            {
                model.DischargeTime = Convert.ToDateTime(obj.DischargeModel.DischargeTime);
                model.AddedBy = CurrentUserId;

                if (obj.DischargeModel.DischargeId != 0)
                {
                    objAdmission.DischargeTime = Convert.ToDateTime(obj.DischargeModel.DischargeTime);
                    objAdmission.AddedBy = CurrentUserId;
                    // objVisitDetail.UpdatedBy = CurrentUserId;
                }
                await _IPDischargeService.UpdateAsyncSP(model, objAdmission, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Discharge Updated successfully.");
        }
    }
}
