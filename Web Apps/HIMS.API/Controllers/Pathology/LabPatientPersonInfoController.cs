using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.API.Models.Pathology;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Pathology
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class LabPatientPersonInfoController : BaseController
    {
        private readonly IGenericService<TLabPatientPersonInfo> _repository;
        public LabPatientPersonInfoController(IGenericService<TLabPatientPersonInfo> repository)
        {
            _repository = repository;
        }
  
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "LabPatientPersonInfo", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<TLabPatientPersonInfo> LabPatientPersonInfoList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(LabPatientPersonInfoList.ToGridResponse(objGrid, "LabPatientPersonInfo  List"));
        }


        [HttpGet("{id?}")]
        //[Permission(PageCode = "LabPatientPersonInfo", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.PatientInfoId == id);
            return data.ToSingleResponse<TLabPatientPersonInfo, LabPatientPersonInfoModel>("LabPatientPersonInfo");
        }


        [HttpPost]
        //[Permission(PageCode = "LabPatientPersonInfo", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(LabPatientPersonInfoModel obj)
        {
            TLabPatientPersonInfo model = obj.MapTo<TLabPatientPersonInfo>();
            if (obj.PatientInfoId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }


        [HttpPut("{id:int}")]
        //[Permission(PageCode = "LabPatientPersonInfo", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(LabPatientPersonInfoModel obj)
        {
            TLabPatientPersonInfo model = obj.MapTo<TLabPatientPersonInfo>();
            if (obj.PatientInfoId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.");
        }
    }
}
