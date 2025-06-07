using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.OPPatient;
using HIMS.API.Models.OutPatient;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
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
        [HttpGet("{id?}")]
        [Permission(PageCode = "PhoneAppointment", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            var data = await _repository.GetById(x => x.PhoneAppId == id);
            return data.ToSingleResponse<TPhoneAppointment, PhoneAppointment2Model>("Registration");
        }

        [HttpPost("InsertSP")]
        [Permission(PageCode = "PhoneAppointment", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(PhoneAppointment2Model obj)
        {
            TPhoneAppointment model = obj.MapTo<TPhoneAppointment>();
            if (obj.PhoneAppId == 0)
            {
                model.AppDate = DateTime.Now.Date;
                model.AppTime = DateTime.Now;

                model.PhAppDate = Convert.ToDateTime(obj.PhAppDate);
                model.PhAppTime = Convert.ToDateTime(obj.PhAppTime);

                model.UpdatedBy = CurrentUserId;
                await _IPhoneAppointment2Service.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model);
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
                model.IsCancelledDate = DateTime.Now;
                await _IPhoneAppointment2Service.CancelAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Canceled successfully.");
        }


        [HttpGet("auto-complete")]
        [Permission(PageCode = "PhoneAppointment", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetAutoComplete(string Keyword)
        {
            var data = await _IPhoneAppointment2Service.SearchPhoneApp(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PhoneApp Data.", data.Select(x => new { Text = x.FirstName + " " + x.LastName + " | " + x.RegNo + " | " + x.Mobile, Value = x.Id }));
        }
        
    }
}

