using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.Api.Controllers;
using Asp.Versioning;

namespace HIMS.API.Controllers.Masters.DoctorMasterm
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class DoctorTypeMasterController : BaseController
    {
        private readonly IGenericService<DoctorTypeMaster> _repository;
        public DoctorTypeMasterController(IGenericService<DoctorTypeMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "DoctorTypeMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<DoctorTypeMaster> DoctorTypeMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(DoctorTypeMasterList.ToGridResponse(objGrid, "DoctorType List"));
        }
        [HttpGet("{id?}")]
        [Permission(PageCode = "DoctorTypeMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.Id == id);
            return data.ToSingleResponse<DoctorTypeMaster, DoctorTypeMasterModel>("DoctorTypeMaster");
        }


        [HttpPost]
        [Permission(PageCode = "DoctorTypeMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(DoctorTypeMasterModel obj)
        {
            DoctorTypeMaster model = obj.MapTo<DoctorTypeMaster>();
            model.IsActive = true;
            if (obj.Id == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DoctorType added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        [Permission(PageCode = "DoctorTypeMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(DoctorTypeMasterModel obj)
        {
            DoctorTypeMaster model = obj.MapTo<DoctorTypeMaster>();
            model.IsActive = true;
            if (obj.Id == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DoctorType updated successfully.");
        }
        //Delete API
        [HttpDelete]
        [Permission(PageCode = "DoctorTypeMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            DoctorTypeMaster model = await _repository.GetById(x => x.Id == Id);
            if ((model?.Id ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DoctorType deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }


    }
}
