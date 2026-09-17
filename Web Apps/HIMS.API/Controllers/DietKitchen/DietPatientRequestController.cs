using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.DietKitchen;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data.DTO.DietKitchen;
using HIMS.Data.DTO.OTManagement;
using HIMS.Data.Models;
using HIMS.Services.DietKitchen;
using HIMS.Services.Transaction;
using Microsoft.AspNetCore.Mvc;
using static HIMS.API.Models.DietKitchen.DietPatientRequestDetailModelValidator;

namespace HIMS.API.Controllers.DietKitchen
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]

    public class DietPatientRequestController : BaseController
    {
        private readonly IDietPatientRequestService _IDietPatientRequestService;
        public DietPatientRequestController(IDietPatientRequestService repository)
        {
            _IDietPatientRequestService = repository;

        }
        [HttpPost("DietPatientRequestHeaderList")]
        [Permission]
        public async Task<IActionResult> DietPatientRequestHeaderList(GridRequestModel objGrid)
        {
            IPagedList<DietPatientRequestHeaderListDto> ReservationAttendingDetailList = await _IDietPatientRequestService.GetListAsync(objGrid);
            return Ok(ReservationAttendingDetailList.ToGridResponse(objGrid, "DietPatientRequestHeader List"));
        }
        [HttpPost("DietPatientRequestDetailsList")]
        [Permission]
        public async Task<IActionResult> DietPatientRequestDetailsList(GridRequestModel objGrid)
        {
            IPagedList<DietPatientRequestDetailsListDto> ReservationAttendingDetailList = await _IDietPatientRequestService.GetListDetailsAsync(objGrid);
            return Ok(ReservationAttendingDetailList.ToGridResponse(objGrid, "DietPatientRequestDetails List"));
        }
        [HttpPost("Insert")]
        [Permission]
        public async Task<ApiResponse> Insert(DietPatientRequestModel obj)
        {
            TDietPatientRequestHeader model = obj.MapTo<TDietPatientRequestHeader>();
            if (obj.DietReqId == 0)
            {
                foreach (var q in model.TDietPatReqDetails)
                {
                    q.OrderDate = AppTime.Now;

                }
                foreach (var q in model.TDietPatReqDetails)
                {

                }
                model.CreatedDate = AppTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IDietPatientRequestService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.DietReqId);
        }

      

        [HttpPut("Edit/{id:int}")]
        [Permission]
        public async Task<ApiResponse> Edit(DietPatientRequestModel obj)
        {
            if (obj.DietReqId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");

            TDietPatientRequestHeader model = obj.MapTo<TDietPatientRequestHeader>();

            foreach (var q in model.TDietPatReqDetails)
            {
                q.DietReqDetId = 0;     
                q.OrderDate = AppTime.Now;
            }

            model.ModifiedDate = AppTime.Now;
            model.ModifiedBy = CurrentUserId;

            await _IDietPatientRequestService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });


            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model.DietReqId);
        }

        [HttpPost("DietPatientReqHeaderCancel")]
        [Permission]
        public async Task<ApiResponse> Cancel(DietPatientRequestCancel obj)
        {
            TDietPatientRequestHeader model = obj.MapTo<TDietPatientRequestHeader>();
            if (obj.DietReqId != 0)
            {
                model.DietReqId = obj.DietReqId;
                await _IDietPatientRequestService.Cancel(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Canceled successfully.");
        }
        [HttpPost("DietPatReqDetailCanel")]
        [Permission]
        public async Task<ApiResponse> Cancels(DietPatientRequestDetailsCancel obj)
        {
            TDietPatReqDetail model = obj.MapTo<TDietPatReqDetail>();
            if (obj.DietReqDetId != 0)
            {
                model.DietReqDetId = obj.DietReqDetId;
                await _IDietPatientRequestService.CancelD(model, CurrentUserId, CurrentUserName);
            }
            else
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Canceled successfully.");
        }

        [HttpPost("DietPatReqDetailAccept")]
        [Permission]
        public async Task<ApiResponse> Accepts(DietPatientRequestDetailsAccept obj)
        {
            TDietPatReqDetail model = obj.MapTo<TDietPatReqDetail>();
            if (obj.DietReqDetId != 0)
            {
                model.DietReqDetId = obj.DietReqDetId;
                await _IDietPatientRequestService.AcceptD(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Accepted successfully.");
        }
    }

}



