using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.Data.Models;
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

        [HttpPost("InsertSP")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
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
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(SupplierModel obj)
        {
            MSupplierMaster model = obj.MapTo<MSupplierMaster>();
            if (obj.SupplierId == 0)
            {
                model.SupplierTime = Convert.ToDateTime(obj.SupplierTime);
                model.AddedBy = CurrentUserId;
                model.IsActive = true;
                await _SupplierService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Supplier Name  added successfully.");
        }

        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(SupplierModel obj)
        {
            MSupplierMaster model = obj.MapTo<MSupplierMaster>();
            if (obj.SupplierId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.SupplierTime = Convert.ToDateTime(obj.SupplierTime);
                await _SupplierService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Supplier Name updated successfully.");
        }


    }
}
