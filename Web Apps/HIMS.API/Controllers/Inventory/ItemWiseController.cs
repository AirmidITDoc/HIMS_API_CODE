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
    public class ItemWiseController : BaseController
    {
        private readonly IItemWiseService _IItemWiseService;
        public ItemWiseController(IItemWiseService repository)
        {
            _IItemWiseService = repository;
        }
        [HttpPost("ItemWiseList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<ItemWiseListDto> ItemWiseList = await _IItemWiseService.ItemWiseList(objGrid);
            return Ok(ItemWiseList.ToGridResponse(objGrid, "ItemWise App List"));
        }
    }
}
