using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Models.Inventory;
using HIMS.Services.Masters;
using HIMS.Data.DTO.OPPatient;

namespace HIMS.API.Controllers.Masters.DoctorMasterm
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class DoctorController : BaseController
    {
        private readonly IDoctorMasterService _IDoctorMasterService;
        private readonly IGenericService<LvwDoctorMasterList> _repository1;

        public DoctorController(IDoctorMasterService repository, IGenericService<LvwDoctorMasterList> repository1)
        {
            _IDoctorMasterService = repository;
            _repository1 = repository1;

        }
        [HttpPost("DoctorList")]
        [Permission(PageCode = "DoctorMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<DoctorMaster> DoctorList = await _IDoctorMasterService.GetListAsync(objGrid);
            return Ok(DoctorList.ToGridResponse(objGrid, "DoctorList"));
        }
        [HttpGet("{id?}")]
        [Permission(PageCode = "DoctorMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _IDoctorMasterService.GetById(id);
            return data.ToSingleResponse<DoctorMaster, DoctorModel>("Doctor Master");
        }
        [HttpPost("InsertSP")]
        [Permission(PageCode = "DoctorMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertSP(DoctorModel obj)
        {
            DoctorMaster model = obj.MapTo<DoctorMaster>();
            if (obj.DoctorId == 0)
            {
                model.DateofBirth = Convert.ToDateTime(obj.DateofBirth);
                model.RegDate = Convert.ToDateTime(obj.RegDate);
                model.MahRegDate = Convert.ToDateTime(obj.MahRegDate);
                model.Addedby = CurrentUserId;
                model.IsActive = true;
                await _IDoctorMasterService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Doctor Name  added successfully.");
        }
        [HttpPost("InsertEDMX")]
        [Permission(PageCode = "DoctorMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(DoctorModel obj)
        {
            DoctorMaster model = obj.MapTo<DoctorMaster>();
            if (obj.DoctorId == 0)
            {
                model.DateofBirth = Convert.ToDateTime(obj.DateofBirth.Value.ToLocalTime());
                if (model.RegDate.HasValue)
                    model.RegDate = Convert.ToDateTime(obj.RegDate.Value.ToLocalTime()).Date;
                if (model.MahRegDate.HasValue)
                    model.MahRegDate = Convert.ToDateTime(obj.MahRegDate.Value.ToLocalTime()).Date;
                model.Addedby = CurrentUserId;
                model.IsActive = true;
                await _IDoctorMasterService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Doctor Name  added successfully.");
        }

        [HttpPut("Edit/{id:int}")]
        [Permission(PageCode = "DoctorMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(DoctorModel obj)
        {
            DoctorMaster model = obj.MapTo<DoctorMaster>();
            if (obj.DoctorId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.DateofBirth = Convert.ToDateTime(obj.DateofBirth.Value.ToLocalTime());
                if (model.RegDate.HasValue)
                    model.RegDate = Convert.ToDateTime(obj.RegDate.Value.ToLocalTime());
                if (model.MahRegDate.HasValue)
                    model.MahRegDate = Convert.ToDateTime(obj.MahRegDate.Value.ToLocalTime());
                await _IDoctorMasterService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Doctor Name updated successfully.");
        }
        [HttpDelete]
        [Permission(PageCode = "DoctorMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            DoctorMaster model = await _IDoctorMasterService.GetById(Id);
            if ((model?.DoctorId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _IDoctorMasterService.UpdateAsync(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DoctorMaster  deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }


        [HttpPost("DocList")]
        [Permission(PageCode = "DoctorMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List1(GridRequestModel objGrid)
        {
            IPagedList<LvwDoctorMasterList> List = await _IDoctorMasterService.GetListAsync1(objGrid);
            return Ok(List.ToGridResponse(objGrid, "Doctor List"));
        }

        [HttpGet]
        [Route("get-Doctor")]
        [Permission(PageCode = "DoctorMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetDropdown()
        {
            var List = await _repository1.GetAll();
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Doctor dropdown", List.Select(x => new { x.DoctorId, x.FirstName, x.MiddleName, x.LastName }));
        }
    }
}
