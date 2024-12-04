using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Inventory
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ItemMovementController : BaseController
    {
        private readonly IItemMovementService _IItemMovementService;
        public ItemMovementController(IItemMovementService repository)
        {
            _IItemMovementService = repository;
        }
        [HttpPost("ItemMovementList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<ItemMovementListDto> ItemMovementList = await _IItemMovementService.ItemMovementList(objGrid);
            return Ok(ItemMovementList.ToGridResponse(objGrid, "ItemMovement App List"));
        }
    }
}
