using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Models.Pathology;
using HIMS.Services.OTManagment;
using HIMS.Services.Pathlogy;
using HIMS.Services.IPPatient;
using HIMS.API.Models.OPPatient;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.DTO.Pathology;

namespace HIMS.API.Controllers.Pathology
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class LabAppointmentController : BaseController
    {
        private readonly ILabAppointmentService _ILabAppointmentService;

        private readonly IGenericService<TLabAppointment> _repository;
        public LabAppointmentController(ILabAppointmentService repository, IGenericService<TLabAppointment> repository1)
        {
            _ILabAppointmentService = repository;
            _repository = repository1;

        }
        //List API Get By Id
        [HttpGet("{id?}")]
        [Permission]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.LabAppId == id);
            return data.ToSingleResponse<TLabAppointment, LabAppointmentModel>("TLabAppointment");
        }

        [HttpPost("LabAppointmentList")]
        [Permission]
        //[Permission(PageCode = "PhoneAppointment", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<LabAppointmentListDto> LabAppointmentList = await _ILabAppointmentService.GetListAsync(objGrid);
            return Ok(LabAppointmentList.ToGridResponse(objGrid, "LabAppointment List"));
        }
        [HttpPost("LabAppointmentDetailList")]
        [Permission]
        //[Permission(PageCode = "PhoneAppointment", Permission = PagePermission.View)]
        public async Task<IActionResult> DetailList(GridRequestModel objGrid)
        {
            IPagedList<LabAppDetListDto> LabAppointmentDetailList = await _ILabAppointmentService.LabGetListAsync(objGrid);
            return Ok(LabAppointmentDetailList.ToGridResponse(objGrid, "LabAppointmentDetail List"));
        }

        [HttpPost("Insert")]
        [Permission]
        public async Task<ApiResponse> Insert(LabAppointmentModel obj)
        {
            TLabAppointment model = obj.MapTo<TLabAppointment>();
            model.IsActive = true;

            if (obj.LabAppId == 0)
            {
                foreach (var q in model.TLabAppServiceDetails)
                {
                    q.CreatedBy = CurrentUserId;
                    q.CreatedDate = AppTime.Now;

                }
                model.CreatedDate = AppTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _ILabAppointmentService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.LabAppId);
        }

      
        [HttpPut("Edit/{id:int}")]
        [Permission]
        public async Task<ApiResponse> Edit(LabAppointmentModel obj)
        {
            TLabAppointment model = obj.MapTo<TLabAppointment>();
            model.IsActive = true;
            if (obj.LabAppId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                foreach (var q in model.TLabAppServiceDetails)
                {
                    if (q.AppointmentDetId == 0)
                    {
                        q.CreatedBy = CurrentUserId;
                        q.CreatedDate = AppTime.Now;
                    }
                    q.ModifiedBy = CurrentUserId;
                    q.ModifiedDate = AppTime.Now;
                    q.AppointmentDetId = 0;
                }

              
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _ILabAppointmentService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });

            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model.LabAppId);
        }


        [HttpPut("RescheduleLabAppointment")]
        [Permission]
        public async Task<ApiResponse> Edit(LabAppointmentUpdate obj)
        {
            if (obj.LabAppId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                TLabAppointment model = await _repository.GetById(x => x.LabAppId == obj.LabAppId);
                model.StartTime = obj.StartTime?.ToLocalDateTime("5:30");
                model.EndTime = obj.EndTime?.ToLocalDateTime("5:30");
                await _repository.Update(model, CurrentUserId, CurrentUserName, null);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }

        [HttpGet("get-Labappoinments")]
        [Permission]
        //[Permission(PageCode = "PhoneAppointment", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetLabAppoinments(int DocId, DateTime FromDate, DateTime ToDate, int CategoryId)
        {
            var data = await _ILabAppointmentService.GetLabAppoinments(DocId, FromDate, ToDate, CategoryId);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Labappoinments Data.", data.Select(x => new
            {
                Start = x.StartTime,
                Id = x.LabAppId,
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

        //[HttpGet("auto-complete")]
        ////[Permission(PageCode = "PhoneAppointment", Permission = PagePermission.View)]
        //public async Task<ApiResponse> GetAutoComplete(string Keyword)
        //{
        //    if (string.IsNullOrWhiteSpace(Keyword) || Keyword == "%")
        //    {
        //        Keyword = string.Empty;
        //    }

        //    var data = await _ILabAppointmentService.SearchLabApp(Keyword);
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PhoneApp Data.", data.Select(x => new { Text = x.FirstName + " " + x.LastName + " | " + x.RegNo + " | " + x.Mobile, Value = x.Id, RegId = x.RegNo, LabAppId = x.AppId, DoctorId = x.DoctorId, DepartmentId = x.DepartmentId }));
        //}
        [HttpDelete("Cancel")]
        [Permission]
        //[Permission(PageCode = "PhoneAppointment", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Cancel(int Id)
        {
            TLabAppointment model = await _repository.GetById(x => x.LabAppId == Id);
            if ((model?.LabAppId ?? 0) > 0)
            {
                model.LabAppId = Id;
                model.IsCancelled = true;
                model.IsCancelledBy = CurrentUserId;
                model.IsCancelledDate = AppTime.Now;
                await _ILabAppointmentService.CancelAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Canceled successfully.");
        }


    }
}
