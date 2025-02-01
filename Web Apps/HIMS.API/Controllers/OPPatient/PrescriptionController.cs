using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.OPPatient;
using HIMS.API.Models.OutPatient;
using HIMS.API.Models.Pharmacy;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;
using HIMS.Services.OPPatient;
using HIMS.Services.OutPatient;
using HIMS.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security;

namespace HIMS.API.Controllers.OPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PrescriptionController : BaseController
    {
        private readonly IPrescription _IPrescription;
        public PrescriptionController(IPrescription repository)
        {
            _IPrescription = repository;
        }
        [HttpPost("PatietWiseMatetialList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<PatietWiseMatetialListDto> PatietWiseMatetialList = await _IPrescription.PatietWiseMatetialList(objGrid);
            return Ok(PatietWiseMatetialList.ToGridResponse(objGrid, "PatietWiseMatetial App List"));
        }

        [HttpPost("PrescriptionInsertEDMX")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(PrescriptionModel obj)
        {
            TPrescription model = obj.MapTo<TPrescription>();
            if (obj.PrecriptionId == 0)
            {
                model.Date = Convert.ToDateTime(obj.Date);
                model.Ptime = Convert.ToDateTime(obj.Ptime);

                model.CreatedBy = CurrentUserId;
                await _IPrescription.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Prescription added successfully.");
        }
        [HttpPost("PrescriptionInsert")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertSP(PrescriptionModel obj)
        {
            TPrescription model = obj.MapTo<TPrescription>();
            if (obj.PrecriptionId == 0)
            {
                model.Date = Convert.ToDateTime(obj.Date);
                model.Ptime = Convert.ToDateTime(obj.Ptime);

                model.CreatedBy = CurrentUserId;
                await _IPrescription.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Prescription added successfully.");
        }


        [HttpPost("PrescriptionInsertMultiRecord")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(PrescriptionModel obj)
        {
            TPrescription model = obj.MapTo<TPrescription>();
            if (obj.PrecriptionId == 0)
            {
                model.Date = Convert.ToDateTime(obj.Date);
                model.Ptime = Convert.ToDateTime(obj.Ptime);

                model.CreatedBy = CurrentUserId;
                await _IPrescription.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Prescription added successfully.");
        }
        [HttpPost("PrescriptionUpdate")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Update(PrescriptionModel obj)
        {
            TPrescription model = obj.MapTo<TPrescription>();
            if (obj.PrecriptionId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.Date = Convert.ToDateTime(obj.Date);
                model.Ptime = Convert.ToDateTime(obj.Ptime);
                model.CreatedBy = CurrentUserId;
                await _IPrescription.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Prescription updated successfully.");
        }
    }
}