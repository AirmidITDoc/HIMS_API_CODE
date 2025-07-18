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
    public class StateMasterController : BaseController
    {
        private readonly IGenericService<MStateMaster> _repository;
        public StateMasterController(IGenericService<MStateMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "StateMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MStateMaster> StateMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(StateMasterList.ToGridResponse(objGrid, "StateMaster List"));
        }

        //[HttpGet("StateListWithCountry/{CountryId?}")]
        ////[Permission(PageCode = "StateListWithCountry", Permission = PagePermission.View)]
        //public async Task<ApiResponse> StateListWithCountry(int CountryId)
        //{
        //    var data = await _repository.GetAll(x => x.IsActive == true && x.CountryId == CountryId);
        //    return new ApiResponse { StatusCode = 200, Data = data };
        //}
        //List API Get By Id
        [HttpGet("{id?}")]
        [Permission(PageCode = "StateMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.StateId == id);
            return data.ToSingleResponse<MStateMaster, StateMasterModel>("StateMaster");
        }
        //Add API
        [HttpPost]
        [Permission(PageCode = "StateMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(StateMasterModel obj)
        {
            MStateMaster model = obj.MapTo<MStateMaster>();
            model.IsActive = true;
            if (obj.StateId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        [Permission(PageCode = "StateMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(StateMasterModel obj)
        {
            MStateMaster model = obj.MapTo<MStateMaster>();
            model.IsActive = true;
            if (obj.StateId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record   updated successfully.");
        }
        //Delete API
        [HttpDelete]
        [Permission(PageCode = "StateMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MStateMaster model = await _repository.GetById(x => x.StateId == Id);
            if ((model?.StateId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }

        [HttpGet]
        [Route("get-state")]
        //[Permission(PageCode = "StateMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetDropdown()
        {
            var MstateMasterList = await _repository.GetAll(x => x.IsActive.Value);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "State dropdown", MstateMasterList.Select(x => new { x.StateId, x.CountryId, x.StateName }));
        }

    }
}
