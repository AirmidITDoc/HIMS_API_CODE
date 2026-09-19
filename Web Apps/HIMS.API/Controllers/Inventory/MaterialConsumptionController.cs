using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
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
        [Permission]
        //[Permission(PageCode = "MaterialConsumption", Permission = PagePermission.View)]

        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MaterialConsumptionListDto> MaterialConsumptionList = await _IMaterialConsumption.MaterialConsumptionList(objGrid);
            return Ok(MaterialConsumptionList.ToGridResponse(objGrid, "MaterialConsumption App List"));
        }
        [HttpPost("MaterialConsumptionDetailsList")]
        //[Permission(PageCode = "MaterialConsumption", Permission = PagePermission.View)]
        public async Task<IActionResult> MaterialConsumptionDetailList(GridRequestModel objGrid)
        {
            IPagedList<MaterialConsumDetailListDto> MaterialConsumptionList = await _IMaterialConsumption.MaterialConsumptiondetailList(objGrid);
            return Ok(MaterialConsumptionList.ToGridResponse(objGrid, "MaterialConsumption Detail App List"));
        }


        [HttpPost("InsertEDMX")]
        [Permission(PageCode = "MaterialConsumption", Permission = PagePermission.Add)]

        public async Task<ApiResponse> InsertEDMX(MaterialConsumptionHeader obj)
        {
            TMaterialConsumptionHeader model = obj.MaterialConsumption.MapTo<TMaterialConsumptionHeader>();
            List<TCurrentStock> ObjCurrentStock = obj.CurrentStockUpdate.MapTo<List<TCurrentStock>>();
            if (obj.MaterialConsumption.MaterialConsumptionId == 0)
            {

                model.AddedBy = CurrentUserId;
                await _IMaterialConsumption.InsertAsync(model, ObjCurrentStock, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Material Consumption  added successfully.", model.MaterialConsumptionId);
        }


        
    }
}
