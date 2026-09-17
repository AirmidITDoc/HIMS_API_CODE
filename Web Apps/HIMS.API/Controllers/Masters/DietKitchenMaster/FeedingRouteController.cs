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
    public class FeedingRouteController : BaseController
    {
        private readonly IGenericService<MFeedingRouteMaster> _repository;

        public FeedingRouteController(IGenericService<MFeedingRouteMaster> repository)
        {
            _repository = repository;
        }

        // List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "DietMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MFeedingRouteMaster> list = await _repository.GetAllPagedAsync(objGrid);
            return Ok(list.ToGridResponse(objGrid, "Feeding Route List"));
        }

        // Get By Id API
        [HttpGet("{id?}")]
        //[Permission(PageCode = "DietMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }

            var data = await _repository.GetById(x => x.FeedingRouteId == id);
            return data.ToSingleResponse<MFeedingRouteMaster, FeedingRouteModel>("FeedingRouteMaster");
        }

        // Post / Insert API
        [HttpPost]
        //[Permission(PageCode = "DietMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(FeedingRouteModel obj)
        {
            MFeedingRouteMaster model = obj.MapTo<MFeedingRouteMaster>();
            model.Active = true;
            if (obj.FeedingRouteId == 0)
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
        //[Permission(PageCode = "DietMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(FeedingRouteModel obj)
        {
            MFeedingRouteMaster model = obj.MapTo<MFeedingRouteMaster>();
            model.Active = true;
            if (obj.FeedingRouteId == 0)
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
        //[Permission(PageCode = "DietMaster", Permission = PagePermission.Delete)]
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
