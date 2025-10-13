using Asp.Versioning;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.API.Models.IPPatient;
using HIMS.Api.Controllers;

namespace HIMS.API.Controllers.IPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class CompanyTPAApprovalController : BaseController
    {
        private readonly IGenericService<TCompanyApprovalDetail> _repository;
        public CompanyTPAApprovalController(IGenericService<TCompanyApprovalDetail> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        //[Permission(PageCode = "TPACompanyDet", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(CompanyApprovalDetModel obj)
        {
            TCompanyApprovalDetail model = obj.MapTo<TCompanyApprovalDetail>();
            model.IsActive = true;
            if (obj.Id == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "TPACompanyDet", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(CompanyApprovalDetModel obj)
        {
            TCompanyApprovalDetail model = obj.MapTo<TCompanyApprovalDetail>();
            model.IsActive = true;
            if (obj.Id == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }
        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "TPACompanyDet", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            TCompanyApprovalDetail model = await _repository.GetById(x => x.Id == Id);
            if ((model?.Id ?? 0) > 0)
            {
                model.IsActive = model.IsActive == true ? false : true;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}
