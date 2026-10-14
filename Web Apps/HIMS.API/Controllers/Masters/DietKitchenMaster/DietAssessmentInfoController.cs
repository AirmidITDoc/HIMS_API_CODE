using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Diet;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.DTO.DietKitchen;
using HIMS.Data.Models;
using HIMS.Services.DietKitchen;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HIMS.API.Controllers.Masters.DietKitchenMaster
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class DietAssessmentInfoController : BaseController
    {
        private readonly IGenericService<TDietAssessmentInfo> _repository;
        private readonly IDietAssessmentInfoService _assessmentService;

        public DietAssessmentInfoController(IGenericService<TDietAssessmentInfo> repository, IDietAssessmentInfoService assessmentService)
        {
            _repository = repository;
            _assessmentService = assessmentService;
        }
        [HttpPost("List")]
        //[Permission]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<DietAssessmentInfoDtoList> list = await _assessmentService.GetListAsync(objGrid);
            return Ok(list.ToGridResponse(objGrid, "DietAssessmentInfo List"));
        }

        [HttpGet("{id?}")]
        //[Permission]
        public async Task<ApiResponse> Get(long id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.DietAssessmentId == id);
            return data.ToSingleResponse<TDietAssessmentInfo, DietAssessmentInfoModel>("DietAssessmentInfo");
        }

        [HttpPost("Insert")]
        //[Permission]
        public async Task<ApiResponse> Post(DietAssessmentInfoModel obj)
        {
            TDietAssessmentInfo model = obj.MapTo<TDietAssessmentInfo>();
            model.IsCancelled = false;
            if (obj.DietAssessmentId == 0)
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

        [HttpPut("Edit/{id:int}")]
        //[Permission]
        public async Task<ApiResponse> Edit(DietAssessmentInfoModel obj)
        {
            TDietAssessmentInfo model = obj.MapTo<TDietAssessmentInfo>();
            if (obj.DietAssessmentId == 0)
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

        [HttpDelete]
        //[Permission]
        public async Task<ApiResponse> Delete(long id)
        {
            TDietAssessmentInfo model = await _repository.GetById(x => x.DietAssessmentId == id);
            if ((model?.DietAssessmentId ?? 0) > 0)
            {
                model.IsCancelled = model.IsCancelled == true ? false : true;
                model.IsCancelledBy = CurrentUserId;
                model.IsCancelledDate = AppTime.Now;
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