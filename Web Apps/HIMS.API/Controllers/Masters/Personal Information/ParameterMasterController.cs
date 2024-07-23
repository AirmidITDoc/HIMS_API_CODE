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

namespace HIMS.API.Controllers.Masters.Personal_Information
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ParameterMasterController : BaseController
    {

        private readonly IGenericService<MPathParameterMaster> _repository;
        public ParameterMasterController(IGenericService<MPathParameterMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "ParameterMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MPathParameterMaster> PathParameterMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(PathParameterMasterList.ToGridResponse(objGrid, "Parameter Master List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "ParameterMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.ParameterId == id);
            return data.ToSingleResponse<MPathParameterMaster, ParameterMasterModel>("Parameter Master");
        }
        //Add API
        [HttpPost]
        //[Permission(PageCode = "ParameterMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(ParameterMasterModel obj)
        {
            MPathParameterMaster model = obj.MapTo<MPathParameterMaster>();
            model.IsActive = true;
            if (obj.ParameterId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Parameter Name added successfully.");
        }

        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "ParameterMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(ParameterMasterModel obj)
        {
            MPathParameterMaster model = obj.MapTo<MPathParameterMaster>();
            model.IsActive = true;
            if (obj.ParameterId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Parameter name updated successfully.");
        }
        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "ParameterMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> delete(int Id)
        {
            MPathParameterMaster model = await _repository.GetById(x => x.ParameterId == Id);
            if ((model?.ParameterId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Parameter Name deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }

    }
}
