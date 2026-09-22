using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Diet;
using HIMS.Core;
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
    public class DietRestrictionController : BaseController
    {
        private readonly IGenericService<MDietRestrictionMaster> _repository;

        public DietRestrictionController(IGenericService<MDietRestrictionMaster> repository)
        {
            _repository = repository;
        }

        // List API
        [HttpPost]
        [Route("[action]")]
        [Permission]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MDietRestrictionMaster> list = await _repository.GetAllPagedAsync(objGrid);
            return Ok(list.ToGridResponse(objGrid, "Diet Restriction List"));
        }

        // Get By Id API
        [HttpGet("{id?}")]
        [Permission]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0) return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            var data = await _repository.GetById(x => x.RestrictionId == id);
            return data.ToSingleResponse<MDietRestrictionMaster, DietRestrictionModel>("DietRestrictionMaster");
        }

        // Post / Insert API
        [HttpPost]
        [Permission]
        public async Task<ApiResponse> Post(DietRestrictionModel obj)
        {
            MDietRestrictionMaster model = obj.MapTo<MDietRestrictionMaster>();
            model.Active = true;
            if (obj.RestrictionId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        // Edit / Update API
        [HttpPut("{id:int}")]
        [Permission]
        public async Task<ApiResponse> Edit(DietRestrictionModel obj)
        {
            MDietRestrictionMaster model = obj.MapTo<MDietRestrictionMaster>();
            model.Active = true;
            if (obj.RestrictionId == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }

        // Delete API (Soft Delete / Toggle Status)
        [HttpDelete]
        [Permission]
        public async Task<ApiResponse> Delete(long Id)
        {
            MDietRestrictionMaster? model = await _repository.GetById(x => x.RestrictionId == Id);
            if ((model?.RestrictionId ?? 0) > 0)
            {
                model!.Active = model.Active == true ? false : true;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record deleted successfully.");
            }
            else
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }
        }
    }
}

