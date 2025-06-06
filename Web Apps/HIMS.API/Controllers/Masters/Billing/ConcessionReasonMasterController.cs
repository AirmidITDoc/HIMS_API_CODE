using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.Api.Controllers;
using HIMS.API.Controllers.Masters.Personal_Information;
using Asp.Versioning;
using HIMS.API.Models.Inventory.Masters;

namespace HIMS.API.Controllers.Masters.Billing
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ConcessionReasonMasterController : BaseController
    {
        private readonly IGenericService<MConcessionReasonMaster> _repository;
        public ConcessionReasonMasterController(IGenericService<MConcessionReasonMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "ConcessionReasonMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MConcessionReasonMaster> ConcessionReasonMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(ConcessionReasonMasterList.ToGridResponse(objGrid, "ConcessionReason List"));
        }
        [HttpGet("{id?}")]
        [Permission(PageCode = "ConcessionReasonMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.ConcessionId == id);
            return data.ToSingleResponse<MConcessionReasonMaster, ConcessionReasonMasterModel>("ConcessionReasonMaster");
        }


        [HttpPost]
        [Permission(PageCode = "ConcessionReasonMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(ConcessionReasonMasterModel obj)
        {
            MConcessionReasonMaster model = obj.MapTo<MConcessionReasonMaster>();
            model.IsActive = true;
            if (obj.ConcessionId == 0)
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
        [Permission(PageCode = "ConcessionReasonMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(ConcessionReasonMasterModel obj)
        {
            MConcessionReasonMaster model = obj.MapTo<MConcessionReasonMaster>();
            model.IsActive = true;
            if (obj.ConcessionId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }
        //Delete API
        [HttpDelete]
        [Permission(PageCode = "ConcessionReasonMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MConcessionReasonMaster model = await _repository.GetById(x => x.ConcessionId == Id);
            if ((model?.ConcessionId ?? 0) > 0)
            {
                model.IsActive = false;
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
