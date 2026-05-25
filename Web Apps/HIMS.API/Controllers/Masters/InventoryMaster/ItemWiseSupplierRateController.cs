using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Models.Inventory;
using HIMS.Data.DTO.Inventory;
using HIMS.Services.Administration;
using HIMS.Services.Inventory;

namespace HIMS.API.Controllers.Masters.InventoryMaster
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ItemWiseSupplierRateController : BaseController
    {
        
            private readonly IGenericService<MItemWiseSupplierRate> _repository;
            private readonly IItemWiseSupplierRateService _IItemWiseSupplierRateService;

        public ItemWiseSupplierRateController(IGenericService<MItemWiseSupplierRate> repository, IItemWiseSupplierRateService repository1)
            {
                _repository = repository;
                _IItemWiseSupplierRateService = repository1;

        }
        [HttpPost("ItemWiseSupplierRateList")]
        [Permission]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<ItemWiseSupplierRateDto> ItemWiseSupplierRateList = await _IItemWiseSupplierRateService.GetListAsync(objGrid);
            return Ok(ItemWiseSupplierRateList.ToGridResponse(objGrid, "ItemWiseSupplierRate List"));
        }

        //List API Get By Id
            [HttpGet("{id?}")]
            [Permission]
            public async Task<ApiResponse> Get(int id)
            {
                if (id == 0)
                {
                    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
                }
                var data = await _repository.GetById(x => x.DefId == id);
                return data.ToSingleResponse<MItemWiseSupplierRate, ItemWiseSupplierRateModel>("MItemWiseSupplierRate");
            }
            //Add API
            [HttpPost]
            [Permission]
            public async Task<ApiResponse> Post(ItemWiseSupplierRateModel obj)
            {
            MItemWiseSupplierRate model = obj.MapTo<MItemWiseSupplierRate>();
                model.IsActive = true;
                if (obj.DefId == 0)
                {
                    model.CreatedBy = CurrentUserId;
                    model.CreatedDate = AppTime.Now;
                    model.ModifiedBy = CurrentUserId;
                    model.ModifiedDate = AppTime.Now;
                    await _repository.Add(model, CurrentUserId, CurrentUserName);
                }
                else
                    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.");
            }
            //Edit API
            [HttpPut("{id:int}")]
            [Permission]
            public async Task<ApiResponse> Edit(ItemWiseSupplierRateModel obj)
            {
            MItemWiseSupplierRate model = obj.MapTo<MItemWiseSupplierRate>();
                model.IsActive = true;
                if (obj.DefId == 0)
                    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
                else
                {
                    model.ModifiedBy = CurrentUserId;
                    model.ModifiedDate = AppTime.Now;
                    await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
                }
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.");
            }
            //Delete API
            [HttpDelete]
            [Permission]
            public async Task<ApiResponse> Delete(int Id)
            {
            MItemWiseSupplierRate model = await _repository.GetById(x => x.DefId == Id);
                if ((model?.DefId ?? 0) > 0)
                {
                    model.IsActive = model.IsActive == true ? false : true;
                    model.ModifiedBy = CurrentUserId;
                    model.ModifiedDate = AppTime.Now;
                    await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  deleted successfully.");
                }
                else
                    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }

        }
    }
