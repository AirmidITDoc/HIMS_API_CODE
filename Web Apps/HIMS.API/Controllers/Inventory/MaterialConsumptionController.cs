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
    public class MaterialConsumptionController : BaseController
    {
        private readonly IMaterialConsumption _IMaterialConsumption;
        public MaterialConsumptionController(IMaterialConsumption repository)
        {
            _IMaterialConsumption = repository;
        }
        [HttpPost("MaterialConsumptionList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MaterialConsumptionListDto> MaterialConsumptionList = await _IMaterialConsumption.MaterialConsumptionList(objGrid);
            return Ok(MaterialConsumptionList.ToGridResponse(objGrid, "MaterialConsumption App List"));
        }
    }
}
