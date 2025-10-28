using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
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

        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "Favourite", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            List<SearchGrid> list = objGrid.Filters.MapTo<List<SearchGrid>>();
            IPagedList<FavouriteModel> FavouriteList = await _favouriteService.GetFavouriteModules(objGrid, list);
            return Ok(FavouriteList.ToGridResponse(objGrid, "Favourite List"));
        }
        [HttpPost]
        //[Permission(PageCode = "Favourite", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(FavouriteDtoModel obj)
        {
            TFavouriteUserList model = obj.MapTo<TFavouriteUserList>();
            if (model.UserId <= 0 || model.MenuId <= 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
                await _favouriteService.InsertAsync(model);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }
    }
}
