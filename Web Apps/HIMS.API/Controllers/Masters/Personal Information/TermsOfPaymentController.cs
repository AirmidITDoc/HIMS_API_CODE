using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.API.Models.Inventory.Masters;

namespace HIMS.API.Controllers.Masters.Personal_Information
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class TermsOfPaymentController : BaseController
    {
        private readonly IGenericService<MTermsOfPaymentMaster> _repository;
        public TermsOfPaymentController(IGenericService<MTermsOfPaymentMaster> repository)
        {
            _repository = repository;
        }
        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MTermsOfPaymentMaster> TermsOfPaymentMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(TermsOfPaymentMasterList.ToGridResponse(objGrid, "TermsOfPaymentMaster List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.Id == id);
            return data.ToSingleResponse<MTermsOfPaymentMaster, TermsOfPaymentModel>("TermsOfPaymentMaster");
        }
        //Add API
        [HttpPost]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(TermsOfPaymentModel obj)
        {
            MTermsOfPaymentMaster model = obj.MapTo<MTermsOfPaymentMaster>();
            model.IsActive = true;
            if (obj.Id == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "TermsOfPaymentMaster  added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(TermsOfPaymentModel obj)
        {
            MTermsOfPaymentMaster model = obj.MapTo<MTermsOfPaymentMaster>();
            model.IsActive = true;
            if (obj.Id == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "TermsOfPaymentMaster  updated successfully.");
        }
        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MTermsOfPaymentMaster model = await _repository.GetById(x => x.Id == Id);
            if ((model?.Id ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "TermsOfPaymentMaster  deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }

    }
}
