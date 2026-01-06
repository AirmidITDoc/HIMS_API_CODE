using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.GastrologyMasterModel;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services.GastrologyService;
using Microsoft.AspNetCore.Mvc;
using static HIMS.API.Models.GastrologyMasterModel.MSubQuestionMasterModel;

namespace HIMS.API.Controllers.GastrologyMaster
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class SubQuestionValuesMasterController : BaseController
    {
        private readonly IGenericService<MSubQuestionValuesMaster> _repository;
        public SubQuestionValuesMasterController(IGenericService<MSubQuestionValuesMaster> repository)
        {
            _repository = repository;

        }
        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "MSubQuestionValuesMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MSubQuestionValuesMaster> MSubQuestionValuesMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MSubQuestionValuesMasterList.ToGridResponse(objGrid, "MSubQuestionValuesMaster List"));
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
            var data = await _repository.GetById(x => x.SubQuestionValId == id);
            return data.ToSingleResponse<MSubQuestionValuesMaster, MSubQuestionValuesMasterModel>("MSubQuestionValuesMaster");
        }

        //Add API
        [HttpPost]
        //[Permission(PageCode = "MSubQuestionValuesMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(MSubQuestionValuesMasterModel obj)
        {
            MSubQuestionValuesMaster model = obj.MapTo<MSubQuestionValuesMaster>();
            model.IsActive = true;
            if (obj.SubQuestionValId == 0)
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
        //[Permission(PageCode = "MSubQuestionValuesMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(MSubQuestionValuesMasterModel obj)
        {
            MSubQuestionValuesMaster model = obj.MapTo<MSubQuestionValuesMaster>();
            model.IsActive = true;

            if (obj.SubQuestionValId == 0)
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
        //[Permission(PageCode = "MSubQuestionValuesMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MSubQuestionValuesMaster model = await _repository.GetById(x => x.SubQuestionValId == Id);
            if ((model?.SubQuestionValId ?? 0) > 0)
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
