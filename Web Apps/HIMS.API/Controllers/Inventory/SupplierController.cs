using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Common;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Inventory
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class SupplierController : BaseController
    {
        private readonly ISupplierService _SupplierService;
        public SupplierController(ISupplierService repository)
        {
            _SupplierService = repository;
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
                await _SupplierService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Supplier Name updated successfully.");
        }

        [HttpPost("Cancel")]
        [Permission(PageCode = "SupplierMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Cancel(SupplierCancel obj)
        {
            MSupplierMaster model = new();
            if (obj.SupplierId != 0)
            {
                model.SupplierId = obj.SupplierId;
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _SupplierService.CancelAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Supplier Canceled successfully.");
        }

    }
}
