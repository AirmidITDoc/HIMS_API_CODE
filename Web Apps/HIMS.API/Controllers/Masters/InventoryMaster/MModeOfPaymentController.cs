using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Extensions;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.API.Models.Inventory.Masters;

namespace HIMS.API.Controllers.Masters.InventoryMaster
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class MModeOfPaymentController : BaseController
    {

        private readonly IGenericService<MModeOfPayment> _repository;
        public MModeOfPaymentController(IGenericService<MModeOfPayment> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        //  [Permission(PageCode = "MModeOfPayment", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MModeOfPayment> MModeOfPaymentList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MModeOfPaymentList.ToGridResponse(objGrid, "MModeOfPayment List"));
        }
        [HttpGet("{id?}")]
        //  [Permission(PageCode = "MModeOfPayment", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.Id == id);
            return data.ToSingleResponse<MModeOfPayment, MModeOfPaymentModel>("MModeOfPayment");
        }


        [HttpPost]
        //  [Permission(PageCode = "MModeOfPayment", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(MModeOfPaymentModel obj)
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
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "MModeOfPayment added successfully.");
        }
        //  Edit API
        [HttpPut("{id:int}")]
        //   [Permission(PageCode = "MModeOfPayment", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(MModeOfPaymentModel obj)
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
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "MModeOfPayment updated successfully.");
        }

        //Delete API
        [HttpDelete]
        //  [Permission(PageCode = "AreaMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MModeOfPayment model = await _repository.GetById(x => x.Id == Id);
            if ((model?.Id ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "MModeOfPayment deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}
