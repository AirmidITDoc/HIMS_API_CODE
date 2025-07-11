﻿using HIMS.Api.Controllers;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using HIMS.API.Extensions;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;

namespace HIMS.API.Controllers.Masters.Personal_Information
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class CityMasterController : BaseController
    {
        private readonly IGenericService<MCityMaster> _repository;
        public CityMasterController(IGenericService<MCityMaster> repository)
        {
            _repository = repository;
        }
        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "CityMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MCityMaster> MCityMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MCityMasterList.ToGridResponse(objGrid, "City List"));
        }
       
        [HttpGet("{id?}")]
        [Permission(PageCode = "CityMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.CityId == id);
            return data.ToSingleResponse<MCityMaster, CityMasterModel>("CityMaster");
        }
        //Add API
        [HttpPost]
        [Permission(PageCode = "CityMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(CityMasterModel obj)
        {
            MCityMaster model = obj.MapTo<MCityMaster>();
            model.IsActive = true;
            if (obj.CityId == 0)
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
        [Permission(PageCode = "CityMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(CityMasterModel obj)
        {
            MCityMaster model = obj.MapTo<MCityMaster>();
            if (obj.CityId == 0)
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
        [Permission(PageCode = "CityMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MCityMaster model = await _repository.GetById(x => x.CityId == Id);
            if ((model?.CityId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }

        [HttpGet]
        [Route("get-cities")]
        //[Permission(PageCode = "CityMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetDropdown()
        {
            var McityMasterList = await _repository.GetAll(x => x.IsActive.Value);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "City dropdown", McityMasterList.Select(x => new { x.CityId, x.StateId, x.CityName }));
        }
    }
}
