using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;

namespace HIMS.API.Controllers.Masters.InventoryMaster
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ModeOfPaymentController : BaseController
    {

        private readonly IGenericService<MModeOfPayment> _repository;
        public ModeOfPaymentController(IGenericService<MModeOfPayment> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "ModeOfPayment", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MModeOfPayment> ModeOfPaymentList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(ModeOfPaymentList.ToGridResponse(objGrid, "mode of payment  List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        [Permission(PageCode = "ModeOfPayment", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.Id == id);
            return data.ToSingleResponse<MModeOfPayment, ModeOfPaymentModel>("Mode Of Payment");
        }
        //Add API
        [HttpPost]
        [Permission(PageCode = "ModeOfPayment", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(ModeOfPaymentModel obj)
        {
            MModeOfPayment model = obj.MapTo<MModeOfPayment>();
            model.IsActive = true;
            if (obj.Id == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Mode Of Payment Added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        [Permission(PageCode = "ModeOfPayment", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(ModeOfPaymentModel obj)
        {
            MModeOfPayment model = obj.MapTo<MModeOfPayment>();
            model.IsActive = true;
            if (obj.Id == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Mode Of Payment updated successfully.");
        }
        //Delete API
        [HttpDelete]
        [Permission(PageCode = "ModeOfPayment", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MModeOfPayment model = await _repository.GetById(x => x.Id == Id);
            if ((model?.Id ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Mode of Payment  deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}
