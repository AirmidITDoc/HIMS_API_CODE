﻿using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.Personal_Information
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]

    public class SubTpaCompanyController : BaseController
    {
        private readonly IGenericService<MSubTpacompanyMaster> _repository;
        public SubTpaCompanyController(IGenericService<MSubTpacompanyMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "SubTpacompanyMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MSubTpacompanyMaster> SubTpacompanyMastereList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(SubTpacompanyMastereList.ToGridResponse(objGrid, "SubTpacompanyMastere List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "SubTpacompanyMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.SubCompanyId == id);
            return data.ToSingleResponse<MSubTpacompanyMaster, SubTpaModel>("SubTpacompanyMaster");
        }
        //Add API
        [HttpPost]
        //[Permission(PageCode = "SubTpacompanyMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(SubTpaModel obj)
        {
            MSubTpacompanyMaster model = obj.MapTo<MSubTpacompanyMaster>();
            model.IsActive = true;
            if (obj.SubCompanyId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "CompanyName added successfully.");
        }

        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "SubTpacompanyMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(SubTpaModel obj)
        {
            MSubTpacompanyMaster model = obj.MapTo<MSubTpacompanyMaster>();
            model.IsActive = true;
            if (obj.SubCompanyId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Company Name  updated successfully.");
        }
        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "SubTpacompanyMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MSubTpacompanyMaster model = await _repository.GetById(x => x.SubCompanyId == Id);
            if ((model?.SubCompanyId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Company Name deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}