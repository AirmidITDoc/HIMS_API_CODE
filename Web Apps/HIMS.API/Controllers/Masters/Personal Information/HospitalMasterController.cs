using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.Personal_Information
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class HospitalMasterController : BaseController

    {
        private readonly IHospitalMasterService _IHospitalMasterService;
        private readonly IGenericService<HospitalMaster> _repository;
        public HospitalMasterController(IHospitalMasterService repository, IGenericService<HospitalMaster> repository1)
        {
            _IHospitalMasterService = repository;
            _repository = repository1;

        }

        [HttpPost("HospitalMasterList")]
        [Permission(PageCode = "HospitalMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<HospitalMasterListDto> HospitalMasterList = await _IHospitalMasterService.GetListAsyncH(objGrid);
            return Ok(HospitalMasterList.ToGridResponse(objGrid, "HospitalMaster List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        [Permission(PageCode = "HospitalMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.HospitalId == id);
            return data.ToSingleResponse<HospitalMaster, HospitalMasterModel>("HospitalMaster");
        }
        //Add API
        [HttpPost]
        [Permission(PageCode = "HospitalMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(HospitalMasterModel obj)
        {
            HospitalMaster model = obj.MapTo<HospitalMaster>();
            model.IsActive = true;
            if (obj.HospitalId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        [Permission(PageCode = "HospitalMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(HospitalMasterModel obj)
        {
            HospitalMaster model = obj.MapTo<HospitalMaster>();
            model.IsActive = true;
            if (obj.HospitalId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.");
        }

        //Delete API
        [HttpDelete]
        [Permission(PageCode = "HospitalMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            HospitalMaster model = await _repository.GetById(x => x.HospitalId == Id);
            if ((model?.HospitalId ?? 0) > 0)
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

    }
}

