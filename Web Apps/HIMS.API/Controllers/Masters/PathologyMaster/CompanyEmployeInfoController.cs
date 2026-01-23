using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.PathologyMaster
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class CompanyEmployeInfoController : BaseController
    {
        private readonly IGenericService<MCompanyEmployeInfo> _repository;
        public CompanyEmployeInfoController(IGenericService<MCompanyEmployeInfo> repository)
        {
            _repository = repository;
        }
        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "CompanyEmployeInfo", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MCompanyEmployeInfo> CompanyEmployeInfo = await _repository.GetAllPagedAsync(objGrid);
            return Ok(CompanyEmployeInfo.ToGridResponse(objGrid, "Company Employe Info  List"));
        }

        //List API
        [HttpGet]
        [Route("get-employe")]
        // [Permission(PageCode = "CompanyEmployeInfo", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetDropdown()
        {
            var CompanyEmployeInfoList = await _repository.GetAll(x => x.IsActive.Value);         
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Company Employee dropdown", CompanyEmployeInfoList.Select(x => new { FullName = x.FirstName + " " + x.LastName , x.ExecutiveId }));
        }

        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "CompanyEmployeInfo", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.ExecutiveId == id);
            return data.ToSingleResponse<MCompanyEmployeInfo, CompanyEmployeInfoModel>("CompanyEmployeInfo");
        }

        //Add API
        [HttpPost]
        //[Permission(PageCode = "CompanyEmployeInfo", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(CompanyEmployeInfoModel obj)
        {
            MCompanyEmployeInfo model = obj.MapTo<MCompanyEmployeInfo>();
            model.IsActive = true;
            if (obj.ExecutiveId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "CompanyEmployeInfo", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(CompanyEmployeInfoModel obj)
        {
            MCompanyEmployeInfo model = obj.MapTo<MCompanyEmployeInfo>();
            model.IsActive = true;
            if (obj.ExecutiveId == 0)
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
        //[Permission(PageCode = "CompanyEmployeInfo", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MCompanyEmployeInfo model = await _repository.GetById(x => x.ExecutiveId == Id);
            if ((model?.ExecutiveId ?? 0) > 0)
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
