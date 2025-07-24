using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Masters;
using HIMS.API.Models.OTManagement;
using HIMS.API.Models.OutPatient;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;
using HIMS.Services;
using HIMS.Services.IPPatient;
using HIMS.Services.OTManagment;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.OPPatient
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]

    public class EmergencyController : BaseController
    {
        private readonly IEmergencyService _EmergencyService;
        private readonly IGenericService<TEmergencyAdm> _repository;
        private readonly IGenericService<TEmergencyMedicalHistory> _repository1;
        public EmergencyController(IEmergencyService repository, IGenericService<TEmergencyAdm> repository1, IGenericService<TEmergencyMedicalHistory> repository2)
        {
            _EmergencyService = repository;
            _repository = repository1;
            _repository1 = repository2;


        }

        [HttpPost("Emergencylist")]
        //[Permission(PageCode = "Emergency", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<EmergencyListDto> Emergencylist = await _EmergencyService.GetListAsyn(objGrid);
            return Ok(Emergencylist.ToGridResponse(objGrid, "Emergencylist "));
        }

        [HttpGet("{id?}")]
        //[Permission(PageCode = "Emergency", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            var data = await _repository.GetById(x => x.EmgId == id);
            return data.ToSingleResponse<TEmergencyAdm, GetEmergencyModel>("TEmergencyAdm");
        }


        [HttpPost("InsertSP")]
        //[Permission(PageCode = "Emergency", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(EmergencyModel obj)
        {
            TEmergencyAdm model = obj.MapTo<TEmergencyAdm>();
            if (obj.EmgId == 0)
            {
                //model.EmgDate = Convert.ToDateTime(obj.EmgDate);
                //model.EmgTime = Convert.ToDateTime(obj.EmgTime);
                //model.EmgTime = DateTime.Now;

                model.CreatedBy = CurrentUserId;
                await _EmergencyService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }


        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "Emergency", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(EmergencyupdateModel obj)
        {
            TEmergencyAdm model = obj.MapTo<TEmergencyAdm>();
            if (obj.EmgId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.EmgDate = Convert.ToDateTime(obj.EmgDate);
                model.ModifiedBy = CurrentUserId;

                await _EmergencyService.UpdateSP(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }
        [HttpPost("Cancel")]
        //[Permission(PageCode = "Emergency", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Cancel(EmergencyCancel obj)
        {
            TEmergencyAdm model = new();
            if (obj.EmgId != 0)
            {
                model.EmgId = obj.EmgId;
                await _EmergencyService.CancelSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Canceled successfully.");
        }
        //Add API
        [HttpPost("EmergencyMedical")]
        //[Permission(PageCode = "Emergency", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(EmergencyMedicalHistoryModel obj)
        {
            TEmergencyMedicalHistory model = obj.MapTo<TEmergencyMedicalHistory>();
            //model.IsActive = true;
            if (obj.EmgHistoryId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedOn = DateTime.Now;
                await _repository1.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.");
        }
        //Edit API
        [HttpPut("EmergencyMedical/{id:int}")]
        //[Permission(PageCode = "Emergency", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(EmergencyMedicalHistoryModel obj)
        {
            TEmergencyMedicalHistory model = obj.MapTo<TEmergencyMedicalHistory>();
            //model.IsActive = true;
            if (obj.EmgHistoryId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedOn = DateTime.Now;
                await _repository1.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedOn" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.");
        }




    }
}
