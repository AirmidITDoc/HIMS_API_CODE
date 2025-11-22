using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.API.Models.OPPatient;

namespace HIMS.API.Controllers.OPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PatientPolicyController : BaseController
    {
        private readonly IGenericService<TPatientPolicyInformation> _repository;
        public PatientPolicyController(IGenericService<TPatientPolicyInformation> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "PatientPolicy", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<TPatientPolicyInformation> PatientPolicyList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(PatientPolicyList.ToGridResponse(objGrid, "PatientPolicyInformation List"));
        }

        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "PatientPolicy", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.PatientPolicyId == id);
            return data.ToSingleResponse<TPatientPolicyInformation, PatientPolicyModel>("PatientPolicyInformation");
        }
        //Add API
        [HttpPost]
        //[Permission(PageCode = "PatientPolicy", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(PatientPolicyModel obj)
        {
            TPatientPolicyInformation model = obj.MapTo<TPatientPolicyInformation>();
            model.IsActive = true;
            if (obj.PatientPolicyId == 0)
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
        //[Permission(PageCode = "PatientPolicy", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(ItemTypeModel obj)
        {
            TPatientPolicyInformation model = obj.MapTo<TPatientPolicyInformation>();
            model.IsActive = true;
            if (obj.ItemTypeId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.");
        }
        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "PatientPolicy", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            TPatientPolicyInformation model = await _repository.GetById(x => x.PatientPolicyId == Id);
            if ((model?.PatientPolicyId ?? 0) > 0)
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
