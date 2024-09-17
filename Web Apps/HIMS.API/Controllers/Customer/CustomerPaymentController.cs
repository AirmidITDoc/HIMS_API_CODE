using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Customer;
using HIMS.Core.Domain.Grid;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Extensions;

namespace HIMS.API.Controllers.Customer
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class CustomerPaymentController : BaseController
    {
        private readonly IGenericService<ACustomerPaymentSummary> _repository;
        public CustomerPaymentController(IGenericService<ACustomerPaymentSummary> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<ACustomerPaymentSummary> CustomerPaymentSummaryList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(CustomerPaymentSummaryList.ToGridResponse(objGrid, "CustomerPaymentSummary List"));
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
            var data = await _repository.GetById(x => x.CustPayTranId == id);
            return data.ToSingleResponse<ACustomerPaymentSummary, CustomerPaymentSummaryModel>("CustomerPaymentSummary");
        }

        //Add API
        [HttpPost]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(CustomerPaymentSummaryModel obj)
        {
            ACustomerPaymentSummary model = obj.MapTo<ACustomerPaymentSummary>();
            model.IsActive = true;
            if (obj.CustPayTranId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "CustomerPayment added successfully.");
        }

        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(CustomerPaymentSummaryModel obj)
        {
            ACustomerPaymentSummary model = obj.MapTo<ACustomerPaymentSummary>();
            model.IsActive = true;
            if (obj.CustPayTranId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "CustomerPayment  updated successfully.");
        }
    }

}

