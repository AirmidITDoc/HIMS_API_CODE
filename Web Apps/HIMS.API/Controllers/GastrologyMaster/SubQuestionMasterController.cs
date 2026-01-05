using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.Services.GastrologyService;
using HIMS.Api.Models.Common;
using HIMS.API.Models.GastrologyMasterModel;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.API.Extensions;

namespace HIMS.API.Controllers.GastrologyMaster
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class SubQuestionMasterController : BaseController
    {
        private readonly IGenericService<MSubQuestionMaster> _repository;
        public SubQuestionMasterController( IGenericService<MSubQuestionMaster> repository)
        {
            _repository = repository;

        }
        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "MSubQuestionMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MSubQuestionMaster> MSubQuestionMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MSubQuestionMasterList.ToGridResponse(objGrid, "MSubQuestionMaster List"));
        }


        //Add API
        [HttpPost]
        //[Permission(PageCode = "MSubQuestionMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(MSubQuestionMasterModel obj)
        {
            MSubQuestionMaster model = obj.MapTo<MSubQuestionMaster>();
            model.IsActive = true;
            if (obj.SubQuestionId == 0)
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
        //[Permission(PageCode = "MSubQuestionMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(MSubQuestionMasterModel obj)
        {
            MSubQuestionMaster model = obj.MapTo<MSubQuestionMaster>();
            model.IsActive = true;

            if (obj.SubQuestionId == 0)
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
        //[Permission(PageCode = "MSubQuestionMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MSubQuestionMaster model = await _repository.GetById(x => x.SubQuestionId == Id);
            if ((model?.SubQuestionId ?? 0) > 0)
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
