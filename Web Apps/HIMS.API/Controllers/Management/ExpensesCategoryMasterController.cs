using HIMS.Api.Controllers;
using HIMS.API.Models.Administration;
using HIMS.Api.Models.Common;
using HIMS.Core.Domain.Grid;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Extensions;
using HIMS.API.Models.Management;
using Asp.Versioning;

namespace HIMS.API.Controllers.Management
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ExpensesCategoryMasterController : BaseController
    {
        private readonly IGenericService<MExpensesCategoryMaster> _repository;
        public ExpensesCategoryMasterController(IGenericService<MExpensesCategoryMaster> repository)
        {
            _repository = repository;
        }
        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MExpensesCategoryMaster> ExpensesHeadMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(ExpensesHeadMasterList.ToGridResponse(objGrid, "MExpensesHeadMaster "));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.ExpCatId == id);
            return data.ToSingleResponse<MExpensesCategoryMaster, ExpensesCategoryMasterMode>("ExpensesHeadMaster");
        }
        //Add API
        [HttpPost]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(ExpensesCategoryMasterMode obj)
        {
            MExpensesCategoryMaster model = obj.MapTo<MExpensesCategoryMaster>();
            model.IsActive = true;
            if (obj.ExpCatId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.AddedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(ExpensesCategoryMasterMode obj)
        {
            MExpensesCategoryMaster model = obj.MapTo<MExpensesCategoryMaster>();
            model.IsActive = true;
            if (obj.ExpCatId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.UpdatedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.");
        }
        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MExpensesCategoryMaster model = await _repository.GetById(x => x.ExpCatId == Id);
            if ((model?.ExpCatId ?? 0) > 0)
            {
                model.IsActive = model.IsActive == true ? false : true;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}
