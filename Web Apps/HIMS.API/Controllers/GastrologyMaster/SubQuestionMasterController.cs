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
using HIMS.API.Models.Masters;
using HIMS.Core;

namespace HIMS.API.Controllers.GastrologyMaster
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class SubQuestionMasterController : BaseController
    {
        private readonly IGenericService<MSubQuestionMaster> _repository;
        private readonly IQuestionMasterService _IQuestionMasterService;

        public SubQuestionMasterController(IQuestionMasterService repository1, IGenericService<MSubQuestionMaster> repository)
        {
            _repository = repository;
            _IQuestionMasterService = repository1;


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
        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "MQuestionMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.SubQuestionId == id);
            return data.ToSingleResponse<MSubQuestionMaster, MSubQuestionMasterModel>("MSubQuestionMaster");
        }

        [HttpPost("Insert")]
        //[Permission(PageCode = "MQuestionMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(MSubQuestionMasterModel obj)
        {
            MSubQuestionMaster model = obj.MapTo<MSubQuestionMaster>();
            model.IsActive = true;

            if (obj.SubQuestionId == 0)
            {
                foreach (var p in model.MSubQuestionValuesMasters)
                {
                    p.CreatedBy = CurrentUserId;
                    p.CreatedDate = AppTime.Now;
                    p.IsActive = true;

                }
                model.CreatedDate = AppTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IQuestionMasterService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.SubQuestionId);
        }

        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "MQuestionMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(MSubQuestionMasterModel obj)
        {
            MSubQuestionMaster model = obj.MapTo<MSubQuestionMaster>();
            model.IsActive = true;

            if (obj.SubQuestionId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                foreach (var q in model.MSubQuestionValuesMasters)
                {
                    if (q.SubQuestionValId == 0)
                    {
                        q.CreatedBy = CurrentUserId;
                        q.CreatedDate = AppTime.Now;
                        q.IsActive = true;

                    }
                    q.ModifiedBy = CurrentUserId;
                    q.ModifiedDate = AppTime.Now;
                    q.SubQuestionValId = 0;
                }

              
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IQuestionMasterService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });

            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model.SubQuestionId);
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
