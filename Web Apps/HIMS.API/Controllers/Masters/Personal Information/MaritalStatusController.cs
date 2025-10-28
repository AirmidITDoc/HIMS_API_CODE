using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.Personal_Information
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class MaritalStatusController : BaseController
    {
        private readonly IGenericService<MMaritalStatusMaster> _repository;
        public MaritalStatusController(IGenericService<MMaritalStatusMaster> repository)
        {
            _repository = repository;
        }
        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "MaritalStatusMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MMaritalStatusMaster> MaritalStatusMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MaritalStatusMasterList.ToGridResponse(objGrid, "MaritalStatus List"));
        }

        //List API Get By Id
        [HttpGet("{id?}")]
        [Permission(PageCode = "MaritalStatusMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.MaritalStatusId == id);
            return data.ToSingleResponse<MMaritalStatusMaster, MaritalStatusModel>("MaritalStatusMaster");
        }

        //Add API
        [HttpPost]
        [Permission(PageCode = "MaritalStatusMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(MaritalStatusModel obj)
        {
            MMaritalStatusMaster model = obj.MapTo<MMaritalStatusMaster>();
            model.IsActive = true;
            if (obj.MaritalStatusId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record   added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        [Permission(PageCode = "MaritalStatusMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(MaritalStatusModel obj)
        {
            MMaritalStatusMaster model = obj.MapTo<MMaritalStatusMaster>();
            model.IsActive = true;
            if (obj.MaritalStatusId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record name updated successfully.");
        }
        //Delete API
        [HttpDelete]
        [Permission(PageCode = "MaritalStatusMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MMaritalStatusMaster model = await _repository.GetById(x => x.MaritalStatusId == Id);
            if ((model?.MaritalStatusId ?? 0) > 0)
            {
                model.IsActive = model.IsActive == true ? false : true;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record name deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}
