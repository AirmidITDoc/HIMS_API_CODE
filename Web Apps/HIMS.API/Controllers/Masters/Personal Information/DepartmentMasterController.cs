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
    public class DepartmentMasterController : BaseController
    {
        private readonly IGenericService<MDepartmentMaster> _repository;
        public DepartmentMasterController(IGenericService<MDepartmentMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "DepartmentMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MDepartmentMaster> MDepartmentMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MDepartmentMasterList.ToGridResponse(objGrid, "Department List"));
        }

        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "DepartmentMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.DepartmentId == id);
            return data.ToSingleResponse<MDepartmentMaster, DepartmentMasterModel>("DepartmentMaster ");
        }

        //Add API
        [HttpPost]
        //[Permission(PageCode = "DepartmentMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(DepartmentMasterModel obj)
        {
            MDepartmentMaster model = obj.MapTo<MDepartmentMaster>();
            model.IsDeleted = true;
            if (obj.DepartmentId == 0)
            {
                //model.CreatedBy = CurrentUserId;
                //model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Department added successfully.");
        }
    }
}


 