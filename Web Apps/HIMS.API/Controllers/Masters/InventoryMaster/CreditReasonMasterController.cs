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

namespace HIMS.API.Controllers.Masters.InventoryMaster
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class CreditReasonMasterController : BaseController
    {
        private readonly IGenericService<MCreditReasonMaster> _repository;
        public CreditReasonMasterController(IGenericService<MCreditReasonMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "CreditReasonMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MCreditReasonMaster> CreditReasonMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(CreditReasonMasterList.ToGridResponse(objGrid, "CreditReasonMaster List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        [Permission(PageCode = "CreditReasonMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.CreditId == id);
            return data.ToSingleResponse<MCreditReasonMaster, CreditReasonModel>("CreditReasonMaster");
        }

        //Add API
        [HttpPost]
        [Permission(PageCode = "CreditReasonMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(CreditReasonModel obj)
        {
            MCreditReasonMaster model = obj.MapTo<MCreditReasonMaster>();
            model.IsActive = true;
            if (obj.CreditId == 0)
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
        [Permission(PageCode = "CreditReasonMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(CreditReasonModel obj)
        {
            MCreditReasonMaster model = obj.MapTo<MCreditReasonMaster>();
            model.IsActive = true;
            if (obj.CreditId == 0)
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
        [Permission(PageCode = "CreditReasonMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MCreditReasonMaster model = await _repository.GetById(x => x.CreditId == Id);
            if ((model?.CreditId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, " Record deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }

    }
}
