using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Core;
using Microsoft.AspNetCore.Mvc;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.API.Models.Pathology;
using Asp.Versioning;

namespace HIMS.API.Controllers.Masters.Personal_Information
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class AdmissionTypeController : BaseController
    {
        private readonly IGenericService<MAdmissionType> _repository;
        public AdmissionTypeController(IGenericService<MAdmissionType> repository)
        {
            _repository = repository;
        }
        //List API

        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "MAdmissionType", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MAdmissionType> MAdmissionTypeList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MAdmissionTypeList.ToGridResponse(objGrid, "AdmissionType List"));
        }

        [HttpGet("{id?}")]
        //[Permission(PageCode = "MAdmissionType", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.AdmissiontypeId == id);
            return data.ToSingleResponse<MAdmissionType, AdmissionTypeModel>("MAdmissionType");
        }
        //Add API
        [HttpPost]
        //[Permission(PageCode = "MAdmissionType", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(AdmissionTypeModel obj)
        {
            MAdmissionType model = obj.MapTo<MAdmissionType>();
            model.IsActive = true;
            if (obj.AdmissiontypeId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "MAdmissionType", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(AdmissionTypeModel obj)
        {
            MAdmissionType model = obj.MapTo<MAdmissionType>();
            model.IsActive = true;

            if (obj.AdmissiontypeId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }
        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "MAdmissionType", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MAdmissionType model = await _repository.GetById(x => x.AdmissiontypeId == Id);
            if ((model?.AdmissiontypeId ?? 0) > 0)
            {
                model.IsActive = model.IsActive == true ? false : true;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}
