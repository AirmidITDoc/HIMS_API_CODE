using Asp.Versioning;
using HIMS.ABHA.Models.M2;
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

namespace HIMS.API.Controllers.Masters.OTMaster
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class SubSpecialtyMasterController : BaseController
    {
        private readonly IGenericService<MOtSubSpecialtyMaster> _repository;

        public SubSpecialtyMasterController(IGenericService<MOtSubSpecialtyMaster> repository)
        {
            _repository = repository;
        }

        // 1. List API (Paged Grid)
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "SubSpecialtyMaster", Permission = Permission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MOtSubSpecialtyMaster> list = await _repository.GetAllPagedAsync(objGrid);
            return Ok(list.ToGridResponse(objGrid, "SubSpecialty List"));
        }

        // 2. Get By Id API
        [HttpGet("{id?}")]
        //[Permission(PageCode = "SubSpecialtyMaster", Permission = Permission.View)]
        public async Task<ApiResponse> Get(long id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.SubSpecialtyId == id);
            return data.ToSingleResponse<MOtSubSpecialtyMaster, SubSpecialtyMasterModel>("SubSpecialty");
        }

        // 3. Add API (Post)
        [HttpPost]
        //[Permission(PageCode = "SubSpecialtyMaster", Permission = Permission.Add)]
        public async Task<ApiResponse> Post(SubSpecialtyMasterModel obj)
        {
            MOtSubSpecialtyMaster model = obj.MapTo<MOtSubSpecialtyMaster>();
            model.IsActive = true;

            if (obj.SubSpecialtyId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        // 4. Edit API (Put)
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "SubSpecialtyMaster", Permission = Permission.Edit)]
        public async Task<ApiResponse> Edit(SubSpecialtyMasterModel obj)
        {
            MOtSubSpecialtyMaster model = obj.MapTo<MOtSubSpecialtyMaster>();
            model.IsActive = true;

            if (obj.SubSpecialtyId == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }

        // 5. Delete API (Soft Delete / Toggle Status)
        [HttpDelete]
        //[Permission(PageCode = "SubSpecialtyMaster", Permission = Permission.Delete)]
        public async Task<ApiResponse> Delete(long Id)
        {
            MOtSubSpecialtyMaster model = await _repository.GetById(x => x.SubSpecialtyId == Id);
            if ((model?.SubSpecialtyId ?? 0) > 0)
            {
                model.IsActive = model.IsActive == true ? false : true;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record deleted successfully.");
            }
            else
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }
        }
    }
}