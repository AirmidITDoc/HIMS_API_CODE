using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Diet;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.DietKitchenMaster
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class DietTypeMasterController : BaseController
    {
        private readonly IGenericService<MDietTypeMaster> _repository;
        public DietTypeMasterController(IGenericService<MDietTypeMaster> repository)
        {
            _repository = repository;
        }
        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MDietTypeMaster> DietTypeMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(DietTypeMasterList.ToGridResponse(objGrid, "DietTypeMaster List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        [Permission]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.DietTypeId == id);
            return data.ToSingleResponse<MDietTypeMaster, DietTypeMasterModel>("MDietTypeMaster");
        }
        //Add API
        [HttpPost]
        [Permission]
        public async Task<ApiResponse> Post(DietTypeMasterModel obj)
        {
            MDietTypeMaster model = obj.MapTo<MDietTypeMaster>();
            model.Active = true;
            if (obj.DietTypeId == 0)
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
        [Permission]
        public async Task<ApiResponse> Edit(DietTypeMasterModel obj)
        {
            MDietTypeMaster model = obj.MapTo<MDietTypeMaster>();
            model.Active = true;
            if (obj.DietTypeId == 0)
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
        [Permission]
        public async Task<ApiResponse> Delete(int Id)
        {
            MDietTypeMaster model = await _repository.GetById(x => x.DietTypeId == Id);
            if ((model?.DietTypeId ?? 0) > 0)
            {
                model.Active = model.Active == true ? false : true;
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
