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
    public class ItemClassMasterController : BaseController
    {
        private readonly IGenericService<MItemClassMaster> _repository;
        public ItemClassMasterController(IGenericService<MItemClassMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
       // [Permission(PageCode = "ItemClassMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MItemClassMaster> MItemClassMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MItemClassMasterList.ToGridResponse(objGrid, "Item Class Master List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "ItemClassMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.ItemClassId == id);
            return data.ToSingleResponse<MItemClassMaster, ItemClassMasterModel>("ItemClassMaster");
        }
        //Add API
        [HttpPost]
        //[Permission(PageCode = "ItemClassMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(ItemClassMasterModel obj)
        {
            MItemClassMaster model = obj.MapTo<MItemClassMaster>();
            model.IsActive = true;
            if (obj.ItemClassId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Item Class Name added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "ItemClassMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(ItemClassMasterModel obj)
        {
            MItemClassMaster model = obj.MapTo<MItemClassMaster>();
            model.IsActive = true;
            if (obj.ItemClassId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Item Class Name updated successfully.");
        }
        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "ItemClassMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> delete(int Id)
        {
            MItemClassMaster model = await _repository.GetById(x => x.ItemClassId == Id);
            if ((model?.ItemClassId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Item Class Name deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }

    }
}
