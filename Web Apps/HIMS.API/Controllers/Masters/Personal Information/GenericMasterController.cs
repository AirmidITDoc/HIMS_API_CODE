using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;

namespace HIMS.API.Controllers.Masters.Personal_Information
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class GenericMasterController : BaseController
    {
        private readonly IGenericService<MGenericMaster> _repository;
        public GenericMasterController(IGenericService<MGenericMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "GenericMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MGenericMaster> MGenericMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MGenericMasterList.ToGridResponse(objGrid, "Generic List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "GenericMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.GenericId == id);
            return data.ToSingleResponse<MGenericMaster, GenericMasterModel>("Generic");
        }
        //Add API
        [HttpPost]
        //[Permission(PageCode = "GenericMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(GenericMasterModel obj)
        {
            MGenericMaster model = obj.MapTo<MGenericMaster>();
            model.IsActive = true;
            if (obj.GenericId == 0)
            {
                //model.CreatedBy = CurrentUserId;
                //model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Generic name added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "GenericMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(GenericMasterModel obj)
        {
            MGenericMaster model = obj.MapTo<MGenericMaster>();
            model.IsActive = true;
            if (obj.GenericId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Generic Name updated successfully.");
        }
        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "GenericMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse>  Delete(int Id)
        {
            MGenericMaster model = await _repository.GetById(x => x.GenericId == Id);
            if ((model?.GenericId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Generic name deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}
