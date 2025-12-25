using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.API.Models.Nursing;
using HIMS.Core.Infrastructure;

namespace HIMS.API.Controllers.NursingStation
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class TransactionConsentMasterController : BaseController
    {
        private readonly IGenericService<TConsentMaster> _repository;

        public TransactionConsentMasterController(IGenericService<TConsentMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "ConsentMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<TConsentMaster> TConsentMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(TConsentMasterList.ToGridResponse(objGrid, "TConsentMaster List"));
        }
        [HttpGet("{id?}")]
        //[Permission(PageCode = "ConsentMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.ConsentId == id);
            return data.ToSingleResponse<TConsentMaster, TransactionConsentMasterModel>("TConsentMaster");
        }
        [HttpPost]
        //[Permission(PageCode = "ConsentMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(TransactionConsentMasterModel obj)
        {
            TConsentMaster model = obj.MapTo<TConsentMaster>();
            model.IsActive = true;
            if (obj.ConsentId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.",model);
        }
        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "ConsentMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(TransactionConsentMasterModel obj)
        {
            TConsentMaster model = obj.MapTo<TConsentMaster>();
            model.IsActive = true;
            if (obj.ConsentId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model);
        }
        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "ConsentMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            TConsentMaster model = await _repository.GetById(x => x.ConsentId == Id);
            if ((model?.ConsentId ?? 0) > 0)
            {
                model.IsActive = model.IsActive == true ? false : true;
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
