using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Inventory;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Extensions;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.API.Models.Masters;

namespace HIMS.API.Controllers.Inventory
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ItemMasterController : BaseController
    {
        private readonly IGenericService<MItemMaster> _repository;
        public ItemMasterController(IGenericService<MItemMaster> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MItemMaster> ItemMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(ItemMasterList.ToGridResponse(objGrid, "Item  List"));
        }
        [HttpGet("{id?}")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.ItemId == id);
            return data.ToSingleResponse<MItemMaster, ItemMasterModel>("Supplier");
        }

         [HttpPost]
        //[Permission(PageCode = "Gender", Permission = PagePermission.Add)]
        public async Task<ApiResponse> post(ItemMasterModel obj)
        {
            MItemMaster model = obj.MapTo<MItemMaster>();
            if (obj.ItemId == 0)
            {
                await _repository.Add(model, CurrentUserId, CurrentUserName);

            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, " Item Name  added successfully.");
        }

        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(PatientTypeModel obj)
        {
            PatientTypeMaster model = obj.MapTo<PatientTypeMaster>();
            model.IsActive = true;
            if (obj.PatientTypeId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                //model.ModifiedBy = CurrentUserId;
                //model.ModifiedDate = DateTime.Now;
                //await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Patient Type updated successfully.");
        }
        

    }
}
