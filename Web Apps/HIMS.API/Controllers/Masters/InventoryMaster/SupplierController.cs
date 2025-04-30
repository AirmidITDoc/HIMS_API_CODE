using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Common;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.InventoryMaster
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class SupplierController : BaseController
    {
        private readonly IGenericService<MSupplierMaster> _repository;
        private readonly ISupplierService _SupplierService;
        public SupplierController(ISupplierService repository, IGenericService<MSupplierMaster> repository1)
        {
            _SupplierService = repository;
            _repository = repository1;
        }
        [HttpPost("SupplierList")]
        [Permission(PageCode = "SupplierMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<SupplierListDto> SupplierList = await _SupplierService.GetListAsync(objGrid);
            return Ok(SupplierList.ToGridResponse(objGrid, "Supplier List"));
        }
        [HttpGet("{id?}")]
        [Permission(PageCode = "SupplierMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _SupplierService.GetById(id);
            return data.ToSingleResponse<MSupplierMaster, SupplierModel>("Supplier Master");
        }
        [HttpPost("InsertSP")]
        [Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(SupplierModel obj)
        {
            MSupplierMaster model = obj.MapTo<MSupplierMaster>();
            if (obj.SupplierId == 0)
            {
                model.SupplierTime = Convert.ToDateTime(obj.SupplierTime);
                model.AddedBy = CurrentUserId;
                model.IsActive = true;
                await _SupplierService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Supplier Name  added successfully.");
        }


        [HttpPost("InsertEDMX")]
        [Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(SupplierModel obj)
        {
            MSupplierMaster model = obj.MapTo<MSupplierMaster>();
            if (obj.SupplierId == 0)
            {
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = CurrentUserId;
                model.AddedBy = CurrentUserId;
                model.IsActive = true;
                await _SupplierService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Supplier Name  added successfully.");
        }


        [HttpPut("Edit/{id:int}")]
        [Permission(PageCode = "SupplierMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(SupplierModel obj)
        {
            MSupplierMaster model = obj.MapTo<MSupplierMaster>();
            if (obj.SupplierId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.UpdatedBy = CurrentUserId;
                model.IsActive = true;
                await _SupplierService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Supplier Name updated successfully.");
        }
        [HttpDelete("SupplierDelete")]
        [Permission(PageCode = "ParameterMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MSupplierMaster model = await _repository.GetById(x => x.SupplierId == Id);
            if ((model?.SupplierId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Supplier deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");

        }

    }
}
