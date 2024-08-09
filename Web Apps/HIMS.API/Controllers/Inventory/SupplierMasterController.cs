using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.API.Models.OutPatient;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services.OutPatient;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Models.Inventory;

namespace HIMS.API.Controllers.Inventory
{
    public class SupplierMasterController : BaseController
    {
        private readonly IGenericService<MSupplierMaster> _repository;
        public SupplierMasterController(IGenericService<MSupplierMaster> repository)
        {
            _repository = repository;
        }
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "SupplierMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MSupplierMaster> SupplierMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(SupplierMasterList.ToGridResponse(objGrid, "Supplier  List"));
        }
        [HttpGet("{id?}")]
        [Permission(PageCode = "SupplierMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.SupplierId == id);
            return data.ToSingleResponse<MSupplierMaster, SupplierModel>("Supplier");
        }

        [HttpPost]
        [Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> post(SupplierModel obj)
        {
            MSupplierMaster model = obj.MapTo<MSupplierMaster>();
            model.IsActive = true;
            if (obj.SupplierId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Supplier  Name added successfully.");
        }

        [HttpPut("{id:int}")]
        [Permission(PageCode = "SupplierMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(SupplierModel obj)
        {
            MSupplierMaster model = obj.MapTo<MSupplierMaster>();
            model.IsActive = true;
            if (obj.SupplierId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Supplier Name  updated successfully.");
        }
        [HttpDelete]
        [Permission(PageCode = "SupplierMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> delete(int Id)
        {
            MSupplierMaster model = await _repository.GetById(x => x.SupplierId == Id);
            if ((model?.SupplierId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Supplier Name deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}
