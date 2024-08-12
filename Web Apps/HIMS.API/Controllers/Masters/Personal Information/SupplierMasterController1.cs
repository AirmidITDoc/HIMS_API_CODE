using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Models.Inventory;

namespace HIMS.API.Controllers.Masters.Personal_Information
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class SupplierMasterController1 : BaseController
    {
        private readonly IGenericService<MSupplierMaster> _repository;
        public SupplierMasterController1(IGenericService<MSupplierMaster> repository)
        {
            _repository = repository;
        }


        [HttpPost("Insert")]
        [Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(SupplierMasterModel1 obj)
        {
            MSupplierMaster model = obj.MapTo<MSupplierMaster>();
            if (obj.SupplierId == 0)
            {
                model.CreatedDate = Convert.ToDateTime(obj.CreatedDate);
                model.SupplierTime = Convert.ToDateTime(obj.SupplierTime);
                model.AddedBy = CurrentUserId;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Supplier Name  added successfully.");
        }


        //Edit API
        [HttpPut("{id:int}")]
        [Permission(PageCode = "SupplierMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(SupplierMasterModel1 obj)
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
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Supplier Name updated successfully.");
        }
        //Delete API
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
