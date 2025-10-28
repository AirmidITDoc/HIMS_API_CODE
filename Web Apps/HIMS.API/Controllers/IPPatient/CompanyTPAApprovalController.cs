﻿using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.IPPatient;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;

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
        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "TPACompanyDet", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<TCompanyApprovalDetail> CompanyApprovalDetailList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(CompanyApprovalDetailList.ToGridResponse(objGrid, "CompanyApprovalDetailList"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "TPACompanyDet", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.Id == id);
            return data.ToSingleResponse<TCompanyApprovalDetail, CompanyApprovalDetModel>("TCompanyApprovalDetail");
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
