using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Inventory.Masters;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using HIMS.Services.Masters;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Inventory
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class MenuMastersController : BaseController
    {
        private readonly IMenuMasterService _IMenuMasterService;
        public MenuMastersController(IMenuMasterService repository)
        {
            _IMenuMasterService = repository;
        }
        [HttpPost("InsertEDMX")]
        //[Permission(PageCode = "ItemMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(MenuMasterModel obj)
        {
            MenuMaster model = obj.MapTo<MenuMaster>();
            if (obj.Id == 0)
            {
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = CurrentUserId;
                model.IsActive = true;
                await _IMenuMasterService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "MenuMaster   added successfully.");
        }
    }
}
