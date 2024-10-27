using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Extensions;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;

namespace HIMS.API.Controllers.Masters.Personal_Information
{


    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class BankMasterController : BaseController
    {

        private readonly IGenericService<MBankMaster> _repository;
        public BankMasterController(IGenericService<MBankMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
       // [Permission(PageCode = "BankMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MBankMaster> BankMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(BankMasterList.ToGridResponse(objGrid, "Bank List"));
        }
        [HttpGet("{id?}")]
       // [Permission(PageCode = "BankMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.BankId == id);
            return data.ToSingleResponse<MBankMaster, BankMasterModel>("BankMaster");
        }


        [HttpPost]
        //[Permission(PageCode = "BankMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(BankMasterModel obj)
        {
            MBankMaster model = obj.MapTo<MBankMaster>();
            model.IsActive = true;
            if (obj.BankId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Bank added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "BankMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(BankMasterModel obj)
        {
            MBankMaster model = obj.MapTo<MBankMaster>();
            model.IsActive = true;
            if (obj.BankId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Bank updated successfully.");
        }
        //Delete API
        [HttpDelete]
       // [Permission(PageCode = "BankMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MBankMaster model = await _repository.GetById(x => x.BankId == Id);
            if ((model?.BankId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Bank deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }



    }
}

