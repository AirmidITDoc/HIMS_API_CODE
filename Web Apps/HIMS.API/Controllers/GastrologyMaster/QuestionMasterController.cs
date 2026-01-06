using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Data.Models;
using HIMS.Data;
using HIMS.Services.OTManagment;
using Microsoft.AspNetCore.Mvc;
using HIMS.Services.GastrologyService;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Infrastructure;
using HIMS.Core;
using HIMS.Services.IPPatient;
using HIMS.API.Models.GastrologyMasterModel;
using HIMS.Core.Domain.Grid;

namespace HIMS.API.Controllers.GastrologyMaster
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class QuestionMasterController : BaseController
    {
        private readonly IGenericService<MQuestionMaster> _repository;
      
        public QuestionMasterController(IGenericService<MQuestionMaster> repository1)
        {
            _repository = repository1;
           
        }
        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "MQuestionMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MQuestionMaster> MQuestionMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MQuestionMasterList.ToGridResponse(objGrid, "QuestionMaster List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "MQuestionMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.QuestionId == id);
            return data.ToSingleResponse<MQuestionMaster, QuestionMasterModel>("MQuestionMaster");
        }


        //Add API
        [HttpPost]
        //[Permission(PageCode = "MQuestionMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(QuestionMasterModel obj)
        {
            MQuestionMaster model = obj.MapTo<MQuestionMaster>();
            model.IsActive = true;
            if (obj.QuestionId == 0)
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
        //[Permission(PageCode = "MQuestionMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(QuestionMasterModel obj)
        {
            MQuestionMaster model = obj.MapTo<MQuestionMaster>();
            model.IsActive = true;

            if (obj.QuestionId == 0)
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
        //[Permission(PageCode = "MQuestionMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MQuestionMaster model = await _repository.GetById(x => x.QuestionId == Id);
            if ((model?.QuestionId ?? 0) > 0)
            {
                model.IsActive = false;
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
