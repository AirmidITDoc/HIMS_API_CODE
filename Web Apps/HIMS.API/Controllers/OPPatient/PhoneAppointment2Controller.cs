using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.OPPatient;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.OPPatient;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.OPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PhoneAppointment2Controller : BaseController
    {
        private readonly IPhoneAppointment2Service _IPhoneAppointment2Service;
        private readonly IGenericService<TPhoneAppointment> _repository;

        public PhoneAppointment2Controller(IPhoneAppointment2Service repository, IGenericService<TPhoneAppointment> repository1)
        {
            _IPhoneAppointment2Service = repository;
            _repository = repository1;

        }
        [HttpPost("PhoneAppList")]
        [Permission(PageCode = "PhoneAppointment", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<PhoneAppointment2ListDto> PhoneAppList = await _IPhoneAppointment2Service.GetListAsync(objGrid);
            return Ok(PhoneAppList.ToGridResponse(objGrid, "PhoneApp List"));
        }
        [HttpPost("FutureAppointmentList")]
        [Permission(PageCode = "PhoneAppointment", Permission = PagePermission.View)]
        public async Task<IActionResult> List1(GridRequestModel objGrid)
        {
            IPagedList<FutureAppointmentListDto> FutureAppointmentList = await _IPhoneAppointment2Service.FutureAppointmentList(objGrid);
            return Ok(FutureAppointmentList.ToGridResponse(objGrid, "FutureAppointmentList"));
        }
        [HttpPost("FutureAppointmentDetailList")]
        [Permission(PageCode = "PhoneAppointment", Permission = PagePermission.View)]
        public async Task<IActionResult> List2(GridRequestModel objGrid)
        {
            IPagedList<FutureAppointmentDetailListDto> FutureAppointmentDetailList = await _IPhoneAppointment2Service.GetListAsyncF(objGrid);
            return Ok(FutureAppointmentDetailList.ToGridResponse(objGrid, "FutureAppointmentDetailList"));
        }
        [HttpGet("{id?}")]
        [Permission(PageCode = "PhoneAppointment", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            var data = await _repository.GetById(x => x.PhoneAppId == id);
            return data.ToSingleResponse<TPhoneAppointment, PhoneAppointment2Model>("Registration");
        }

        [HttpPost("InsertSP")]
        [Permission(PageCode = "PhoneAppointment", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insertsp(PhoneAppointment2Model obj)
        {
            TPhoneAppointment model = obj.MapTo<TPhoneAppointment>();
            if (obj.PhoneAppId == 0)
            {
                model.AppDate = AppTime.Now.Date;
                model.AppTime = AppTime.Now;

                model.PhAppDate = Convert.ToDateTime(obj.PhAppDate);
                model.PhAppTime = Convert.ToDateTime(obj.PhAppTime);

                model.UpdatedBy = CurrentUserId;
                await _IPhoneAppointment2Service.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model);
        }
        [HttpPost("Insert")]
        [Permission(PageCode = "PhoneAppointment", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(PhoneAppointment2Model obj)
        {
            TPhoneAppointment model = obj.MapTo<TPhoneAppointment>();
            if (obj.PhoneAppId == 0)
            {
                model.AppDate = AppTime.Now.Date;
                model.AppTime = AppTime.Now;

                model.PhAppDate = Convert.ToDateTime(obj.PhAppDate);
                model.PhAppTime = Convert.ToDateTime(obj.PhAppTime);

                model.StartTime = Convert.ToDateTime(obj.StartTime);
                model.EndTime = Convert.ToDateTime(obj.EndTime);

                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _IPhoneAppointment2Service.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model);
        }


        [HttpPut("ReschedulePhoneAppointment")]
        [Permission(PageCode = "PhoneAppointment", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(PhoneAppointmentUpdate obj)
        {
            if (obj.PhoneAppId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                TPhoneAppointment model = await _repository.GetById(x => x.PhoneAppId == obj.PhoneAppId);
                model.StartTime = obj.StartDate.ToLocalDateTime("5:30");
                model.EndTime = obj.EndDate.ToLocalDateTime("5:30");
                await _repository.Update(model, CurrentUserId, CurrentUserName, null);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }


        [HttpDelete("Cancel")]
        [Permission(PageCode = "PhoneAppointment", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Cancel(int Id)
        {
            TPhoneAppointment model = await _repository.GetById(x => x.PhoneAppId == Id);
            if ((model?.PhoneAppId ?? 0) > 0)
            {
                model.PhoneAppId = Id;
                model.IsCancelled = true;
                model.IsCancelledBy = CurrentUserId;
                model.IsCancelledDate = AppTime.Now;
                await _IPhoneAppointment2Service.CancelAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Canceled successfully.");
        }


        [HttpGet("auto-complete")]
        //[Permission(PageCode = "PhoneAppointment", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetAutoComplete(string Keyword)
        {
            if (string.IsNullOrWhiteSpace(Keyword) || Keyword == "%")
            {
                Keyword = string.Empty;
            }

            var data = await _IPhoneAppointment2Service.SearchPhoneApp(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PhoneApp Data.", data.Select(x => new { Text = x.FirstName + " " + x.LastName + " | " + x.RegNo + " | " + x.Mobile, Value = x.Id, RegId = x.RegNo, PhAppId = x.AppId, DoctorId = x.DoctorId, DepartmentId = x.DepartmentId }));
        }



        [HttpGet("get-appoinments")]
        [Permission(PageCode = "PhoneAppointment", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetAppoinments(int DocId, DateTime FromDate, DateTime ToDate)
        {
            var data = await _IPhoneAppointment2Service.GetAppoinments(DocId, FromDate, ToDate);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PhoneApp Data.", data.Select(x => new
            {
                Start = x.StartTime,
                Id = x.PhoneAppId,
                End = x.EndTime,
                Title = x.FirstName + " " + x.MiddleName + " " + x.LastName + " (" + x.MobileNo + ")",
                resizable = new
                {
                    beforeStart = true,
                    afterEnd = true,
                },
                draggable = true,
                color = new
                {
                    primary = "#ad2121",
                    secondary = "#FAE3E3",
                }
            }));
        }
    }
}

