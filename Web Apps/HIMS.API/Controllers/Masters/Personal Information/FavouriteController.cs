using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data.Models;
using HIMS.Services.Masters;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.Personal_Information
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class FavouriteController : BaseController
    {
        private readonly IFavouriteService _favouriteService;
        public FavouriteController(IFavouriteService favouriteService)
        {
            _favouriteService = favouriteService;
        }

        [HttpGet]
        [Route("[action]")]
        [Permission]
        public async Task<ApiResponse> List()
        {
            List<FavouriteModel> FavouriteList = await _favouriteService.GetFavouriteModules(CurrentRoleId, CurrentUserId);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", FavouriteList.Select(x => new { x.MenuId, x.Icon, x.LinkName, x.LinkAction, x.IsFavourite }));
        }
        [HttpPost]
        [Permission]
        public async Task<ApiResponse> Post(FavouriteDtoModel obj)
        {
            TFavouriteUserList model = obj.MapTo<TFavouriteUserList>();
            if (model.MenuId <= 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.UserId = CurrentUserId;
                await _favouriteService.InsertAsync(model);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
            }
        }
    }
}
