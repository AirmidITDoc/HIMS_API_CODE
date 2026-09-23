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
using HIMS.Services.DietkitchenMaster;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.DietKitchenMaster
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class FeedingRouteController : BaseController
    {
        private readonly IGenericService<MFeedingRouteMaster> _repository;
        private readonly IFeedingRouteMasterService _FeedingRouteMasterService;

        public FeedingRouteController(IGenericService<MFeedingRouteMaster> repository, IFeedingRouteMasterService feedingRouteMasterService)
        {
            _repository = repository;
            _FeedingRouteMasterService = feedingRouteMasterService;
        }

        // List API
        [HttpPost]
        [Route("[action]")]
        [Permission]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MFeedingRouteMaster> list = await _repository.GetAllPagedAsync(objGrid);
            return Ok(list.ToGridResponse(objGrid, "Feeding Route List"));
        }

        // Get By Id API
        [HttpGet("{id?}")]
        [Permission]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0) return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            var data = await _repository.GetById(x => x.FeedingRouteId == id);
            return data.ToSingleResponse<MFeedingRouteMaster, FeedingRouteModel>("FeedingRouteMaster");
        }

        // Post / Insert API
        [HttpPost("Insert")]
        [Permission]
        public async Task<ApiResponse> Post(FeedingRouteModel obj)
        {
            MFeedingRouteMaster model = obj.MapTo<MFeedingRouteMaster>();
            model.Active = true;
            if (obj.FeedingRouteId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _FeedingRouteMasterService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        // Edit / Update API
        [HttpPut("Edit/{id:int}")]
        [Permission]
        public async Task<ApiResponse> Edit(FeedingRouteModel obj)
        {
            if (obj.FeedingRouteId == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }

            MFeedingRouteMaster model = obj.MapTo<MFeedingRouteMaster>();

            model.Active = true;
            model.ModifiedBy = CurrentUserId;
            model.ModifiedDate = AppTime.Now;

            await _FeedingRouteMasterService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model.FeedingRouteId);
        }

        // Delete API (Soft Delete / Toggle Status)
        [HttpDelete]
        [Permission]
        public async Task<ApiResponse> Delete(long Id)
        {
            MFeedingRouteMaster? model = await _repository.GetById(x => x.FeedingRouteId == Id);
            if ((model?.FeedingRouteId ?? 0) > 0)
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