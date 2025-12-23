using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;
using HIMS.Services.Masters;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Administration
{
    //SHILPA//
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class MenuMasterController : BaseController
    {
        private readonly IMenuMasterService _MenuMasterService;
        public MenuMasterController(IMenuMasterService repository)
        {
            _MenuMasterService = repository;
        }
        [HttpPost("MenuMasterList")]
        [Permission(PageCode = "Menu", Permission = PagePermission.View)]
        public async Task<IActionResult> MenuMasterList(GridRequestModel objGrid)
        {
            IPagedList<MenuMasterListDto> MenuMasterList = await _MenuMasterService.MenuMasterList(objGrid);
            return Ok(MenuMasterList.ToGridResponse(objGrid, "Menu Master List"));
        }

        [HttpPost("Insertsp")]
        [Permission(PageCode = "Menu", Permission = PagePermission.Add)]
        public ApiResponse InsertSP(MenuMasterModel obj)
        {
            MenuMaster model = obj.MapTo<MenuMaster>();
            if (obj.Id == 0)
            {

                model.IsActive = true;
                _MenuMasterService.InsertSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        [HttpPut("Edit/{id:int}")]
        [Permission(PageCode = "Menu", Permission = PagePermission.Edit)]
        public ApiResponse Edit(MenuMasterModel obj)
        {
            MenuMaster model = obj.MapTo<MenuMaster>();
            model.IsActive = true;
            if (obj.Id == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                _MenuMasterService.UpdateSP(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }

    }
}
