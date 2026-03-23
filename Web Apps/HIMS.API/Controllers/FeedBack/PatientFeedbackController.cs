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
using HIMS.Services.Nursing;
using HIMS.Services.FeedBack;

namespace HIMS.API.Controllers.FeedBack
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PatientFeedbackController : BaseController
    {
        private readonly IGenericService<TPatientFeedback> _repository;
        private readonly IPatientFeedBackService _IPatientFeedBackService;

        public PatientFeedbackController(IGenericService<TPatientFeedback> repository, IPatientFeedBackService repository1)
        {
            _repository = repository;
            _IPatientFeedBackService = repository1;


        }
        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<TPatientFeedback> PatientFeedbackList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(PatientFeedbackList.ToGridResponse(objGrid, "PatientFeedback List"));
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
            return data.ToSingleResponse<TPatientFeedback, PatientFeedbackModel>("TPatientFeedback");
        }
       
        [HttpPost]
        //[Permission]
        public async Task<ApiResponse> Post(List<PatientFeedbackModel> objList)
        {
            if (objList == null || objList.Count == 0)return ApiResponseHelper.GenerateResponse( ApiStatusCode.Status400BadRequest,  "No data received");

            foreach (var obj in objList)
            {
                TPatientFeedback model = obj.MapTo<TPatientFeedback>();

                model.IsActive = true;

                if (obj.PatientFeedbackId == 0)
                {
                    model.CreatedBy = CurrentUserId;
                    model.CreatedDate = AppTime.Now;
                    model.ModifiedBy = CurrentUserId;
                    model.ModifiedDate = AppTime.Now;

                    await _IPatientFeedBackService.InsertAsync( model, CurrentUserId,CurrentUserName );
                }
            }

            return ApiResponseHelper.GenerateResponse( ApiStatusCode.Status200OK, "Records added successfully.");
        }
        [HttpPut]
        //[Permission]
        public async Task<ApiResponse> Edit(List<PatientFeedbackModel> objList)
        {
            if (objList == null || objList.Count == 0) return ApiResponseHelper.GenerateResponse( ApiStatusCode.Status400BadRequest,  "No data received");

            foreach (var obj in objList)
            {
                if (obj.PatientFeedbackId == 0)continue; 

                TPatientFeedback model = obj.MapTo<TPatientFeedback>();

                model.IsActive = true;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;

                await _IPatientFeedBackService.UpdateAsync( model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" }  );
            }

            return ApiResponseHelper.GenerateResponse( ApiStatusCode.Status200OK,"Records updated successfully." );
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
