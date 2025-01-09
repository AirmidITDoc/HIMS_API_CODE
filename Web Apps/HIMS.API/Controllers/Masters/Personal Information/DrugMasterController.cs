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

namespace HIMS.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class DrugMasterController : BaseController
    {
        private readonly IGenericService<MDrugMaster> _repository;
        public DrugMasterController(IGenericService<MDrugMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "DrugMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MDrugMaster> DrugMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(DrugMasterList.ToGridResponse(objGrid, "Drug List"));
        }
        [HttpGet("{id?}")]
        [Permission(PageCode = "DrugMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.DrugId == id);
            return data.ToSingleResponse<MDrugMaster, DrugMasterModel>("DrugMaster");
        }


        [HttpPost]
        [Permission(PageCode = "DrugMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(DrugMasterModel obj)
        {
            MDrugMaster model = obj.MapTo<MDrugMaster>();
            model.IsActive = true;
            if (obj.DrugId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DrugMaster added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        [Permission(PageCode = "DrugMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(DrugMasterModel obj)
        {
            MDrugMaster model = obj.MapTo<MDrugMaster>();
            model.IsActive = true;
            if (obj.DrugId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DrugMaster updated successfully.");
        }
        //Delete API
        [HttpDelete]
        [Permission(PageCode = "DrugMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MDrugMaster model = await _repository.GetById(x => x.DrugId == Id);
            if ((model?.DrugId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DrugMaster deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }


    }
}
