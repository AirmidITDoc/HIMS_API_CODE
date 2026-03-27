using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Models.FeedBack;
using Asp.Versioning;
using HIMS.Data.DTO.Inventory;
using HIMS.Services.FeedBack;
using HIMS.Data.DTO.FeedBack;

namespace HIMS.API.Controllers.FeedBack
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class FeedBackQuestionController : BaseController
    {
        private readonly IGenericService<MFeedbackQuestion> _repository;
        private readonly IFeedBackQuestionService _IFeedBackQuestionService;

        public FeedBackQuestionController(IGenericService<MFeedbackQuestion> repository, IFeedBackQuestionService repository1)
        {
            _repository = repository;
            _IFeedBackQuestionService = repository1;

        }
       
        [HttpPost("FeedbackQuestionList")]
        //[Permission]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<FeedbackQuestionListDto> FeedbackQuestionList = await _IFeedBackQuestionService.GetListAsync(objGrid);
            return Ok(FeedbackQuestionList.ToGridResponse(objGrid, "FeedbackQuestion List"));
        }

        [HttpPost("FeedbackDepartmentList")]
        //[Permission]
        public async Task<IActionResult> DepartmentList(GridRequestModel objGrid)
        {
            IPagedList<DepartmentWithFeedbackListDto> DepartmentList = await _IFeedBackQuestionService.DepartmentListAsync(objGrid);
            return Ok(DepartmentList.ToGridResponse(objGrid, "Department List"));
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
            var data = await _repository.GetById(x => x.FeedbackId == id);
            return data.ToSingleResponse<MFeedbackQuestion, FeedBackQuestionModel>("PatientType");
        }
        //Add API
        [HttpPost]
        //[Permission]
        public async Task<ApiResponse> Post(FeedBackQuestionModel obj)
        {
            MFeedbackQuestion model = obj.MapTo<MFeedbackQuestion>();
            model.IsActive = true;
            if (obj.FeedbackId == 0)
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
        public async Task<ApiResponse> Edit(FeedBackQuestionModel obj)
        {
            MFeedbackQuestion model = obj.MapTo<MFeedbackQuestion>();
            model.IsActive = true;
            if (obj.FeedbackId == 0)
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
            MFeedbackQuestion model = await _repository.GetById(x => x.FeedbackId == Id);
            if ((model?.FeedbackId ?? 0) > 0)
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
