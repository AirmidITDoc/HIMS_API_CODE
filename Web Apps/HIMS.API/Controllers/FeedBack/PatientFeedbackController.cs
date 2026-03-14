using HIMS.Api.Controllers;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.Services.Pathlogy;
using Asp.Versioning;
using HIMS.Core.Domain.Grid;
using HIMS.API.Extensions;
using HIMS.Core;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Infrastructure;
using HIMS.API.Models.FeedBack;

namespace HIMS.API.Controllers.FeedBack
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PatientFeedbackController : BaseController
    {
        private readonly IGenericService<TPatientFeedback> _repository;
        public PatientFeedbackController(IGenericService<TPatientFeedback> repository)
        {
            _repository = repository;

        }
        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<TPatientFeedback> PatientTypeList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(PatientTypeList.ToGridResponse(objGrid, "PatientType List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.PatientFeedbackId == id);
            return data.ToSingleResponse<TPatientFeedback, PatientFeedbackModel>("PatientType");
        }
        //Add API
        [HttpPost]
        //[Permission]
        public async Task<ApiResponse> Post(PatientFeedbackModel obj)
        {
            TPatientFeedback model = obj.MapTo<TPatientFeedback>();
            model.IsActive = true;
            if (obj.PatientFeedbackId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        //[Permission]
        public async Task<ApiResponse> Edit(PatientFeedbackModel obj)
        {
            TPatientFeedback model = obj.MapTo<TPatientFeedback>();
            model.IsActive = true;
            if (obj.PatientFeedbackId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.");
        }
        //Delete API
        [HttpDelete]
        //[Permission]
        public async Task<ApiResponse> Delete(int Id)
        {
            TPatientFeedback model = await _repository.GetById(x => x.PatientFeedbackId == Id);
            if ((model?.PatientFeedbackId ?? 0) > 0)
            {
                model.IsActive = model.IsActive == true ? false : true;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }


    }
}
