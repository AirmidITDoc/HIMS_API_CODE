using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
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
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MaterialConsumptionListDto> MaterialConsumptionList = await _IMaterialConsumption.MaterialConsumptionList(objGrid);
            return Ok(MaterialConsumptionList.ToGridResponse(objGrid, "MaterialConsumption App List"));
        }

        [HttpPost("InsertEDMX")]
        //[Permission(PageCode = "TestMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(MaterialConsumptionModel obj)
        {
            TMaterialConsumptionHeader model = obj.MaterialConsumptionHeader.MapTo<TMaterialConsumptionHeader>();
          List<TMaterialConsumptionDetail> materialmodel = obj.MaterialConsumptionDetail.MapTo<List<TMaterialConsumptionDetail>>();
            if (obj.MaterialConsumptionHeader.MaterialConsumptionId == 0)
            {
 
                model.AddedBy = CurrentUserId;       
                await _IMaterialConsumption.InsertAsync(model ,materialmodel, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Material Consumption  added successfully.");
        }

        //[HttpPut("Edit/{id:int}")]
        ////  [Permission(PageCode = "TestMaster", Permission = PagePermission.Edit)]
        //public async Task<ApiResponse> Edit(MaterialConsumptionHeaderModel obj)
        //{
        //    TMaterialConsumptionHeader model = obj.MapTo<TMaterialConsumptionHeader>();
        //    if (obj.MaterialConsumptionId == 0)
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    else
        //    {
        //        model.UpdatedBy = CurrentUserId;
        //        await _IMaterialConsumption.UpdateAsync(model, CurrentUserId, CurrentUserName);
        //    }
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Material Consumption   updated successfully.");
        //}


        //[HttpPost("Insert")]
        ////[Permission(PageCode = "TestMaster", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> InsertEDMX(MaterialConsumptionDetailModel obj)
        //{
        //    TMaterialConsumptionDetail model = obj.MapTo<TMaterialConsumptionDetail>();
        //    if (obj.MaterialConDetId == 0)
        //    {

        //   //     model.AddedBy = CurrentUserId;
        //        await _IMaterialConsumption.InsertAsync1(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Material Consumption details  added successfully.");
        //}
        //[HttpPut("Edit")]
        ////  [Permission(PageCode = "TestMaster", Permission = PagePermission.Edit)]
        //public async Task<ApiResponse> DetailEdit(MaterialConsumptionDetailModel obj)
        //{
        //    TMaterialConsumptionDetail model = obj.MapTo<TMaterialConsumptionDetail>();
        //    if (obj.MaterialConDetId == 0)
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    else
        //    {
        //        //  model.UpdatedBy = CurrentUserId;
        //        await _IMaterialConsumption.UpdateAsync1(model, CurrentUserId, CurrentUserName);
        //    }
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Material Consumption details   updated successfully.");
       //   }
    }
}
