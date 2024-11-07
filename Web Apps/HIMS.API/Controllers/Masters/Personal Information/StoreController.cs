using Asp.Versioning;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;

namespace HIMS.API.Controllers.Masters.Personal_Information
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class StoreController : BaseController
    {
        private readonly IGenericService<MStoreMaster> _repository;
        public StoreController(IGenericService<MStoreMaster> repository)
        {
            _repository = repository;
        }
        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MStoreMaster> MStoreMastereList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MStoreMastereList.ToGridResponse(objGrid, "StoreMastere List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.StoreId == id);
            return data.ToSingleResponse<MStoreMaster, StoreMasterModel>("StoreMastere");
        }
        //Add API
        [HttpPost]
        // [Permission(PageCode = "PatientType", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(StoreMasterModel obj)
        {
            MStoreMaster model = obj.MapTo<MStoreMaster>();
            model.IsActive = true;
            if (obj.StoreId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Store name  added successfully.");
        }
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(StoreMasterModel obj)
        {
            MStoreMaster model = obj.MapTo<MStoreMaster>();
            model.IsActive = true;
            if (obj.StoreId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Store  name updated successfully.");
        }
        //Delete API
        [HttpDelete]
        // [Permission(PageCode = "PatientType", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MStoreMaster model = await _repository.GetById(x => x.StoreId == Id);
            if ((model?.StoreId ?? 0) > 0)
            {
                model.IsActive = false;
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Store Name  deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}
