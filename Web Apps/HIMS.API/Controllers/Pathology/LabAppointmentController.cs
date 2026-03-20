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
        [HttpPost("LabAppointmentList")]
        //[Permission(PageCode = "PhoneAppointment", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<LabAppointmentListDto> LabAppointmentList = await _ILabAppointmentService.GetListAsync(objGrid);
            return Ok(LabAppointmentList.ToGridResponse(objGrid, "LabAppointment List"));
        }

        [HttpPost("Insert")]
        //[Permission]
        public async Task<ApiResponse> Insert(LabAppointmentModel obj)
        {
            TLabAppointment model = obj.MapTo<TLabAppointment>();
            model.IsActive = true;

            if (obj.LabAppId == 0)
            {
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
        //[Permission(PageCode = "OTReservation", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(LabAppointmentModel obj)
        {
            TLabAppointment model = obj.MapTo<TLabAppointment>();
            model.IsActive = true;

            if (obj.LabAppId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
              
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _ILabAppointmentService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });

            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model.LabAppId);
        }
        //Delete API
        [HttpDelete]
        //[Permission]
        public async Task<ApiResponse> Delete(int Id)
        {
            TLabAppointment model = await _repository.GetById(x => x.LabAppId == Id);
            if ((model?.LabAppId ?? 0) > 0)
            {
                model.IsActive = model.IsActive == true ? false : true;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  deleted successfully.");
            }
            else
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }

        [HttpPut("RescheduleLabAppointment")]
        //[Permission]
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
        //[Permission(PageCode = "PhoneAppointment", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetLabAppoinments(int DocId, DateTime FromDate, DateTime ToDate, int? CategoryId)
        {
            var data = await _ILabAppointmentService.GetLabAppoinments(DocId, FromDate, ToDate, CategoryId);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PhoneApp Data.", data.Select(x => new
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
        
    }
}
