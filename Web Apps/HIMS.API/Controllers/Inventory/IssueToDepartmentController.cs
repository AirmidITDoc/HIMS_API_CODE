using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using HIMS.Services.OPPatient;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Inventory
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class IssueToDepartmentController : BaseController
    {
        private readonly IIssueToDepService _IIssueToDepService;
        public IssueToDepartmentController(IIssueToDepService repository)
        {
            _IIssueToDepService = repository;
        }
       
        [HttpPost("InsertSP")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertSP(IssueToDepartmentModel obj)
        {
            TIssueToDepartmentHeader model = obj.MapTo<TIssueToDepartmentHeader>();
            if (obj.IssueId == 0)
            {
                model.IssueDate = Convert.ToDateTime(obj.IssueDate);
                model.IssueTime = Convert.ToDateTime(obj.IssueTime);
                model.Addedby = CurrentUserId;
                await _IIssueToDepService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "IssueToDepartment added successfully.");
        }

        [HttpPost("Insert")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(IssueToDepartmentModel obj)
        {
            TIssueToDepartmentHeader model = obj.MapTo<TIssueToDepartmentHeader>();
            if (obj.IssueId == 0)
            {
                model.IssueDate = Convert.ToDateTime(obj.IssueDate);
                model.IssueTime = Convert.ToDateTime(obj.IssueTime);
                model.Addedby = CurrentUserId;
                await _IIssueToDepService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "IssueToDepartment added successfully.");
        }
         [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(IssueToDepartmentModel obj)
        {
            TIssueToDepartmentHeader model = obj.MapTo<TIssueToDepartmentHeader>();
            if (obj.IssueId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.IssueDate = Convert.ToDateTime(obj.IssueDate);
                model.IssueTime = Convert.ToDateTime(obj.IssueTime);
                await _IIssueToDepService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "IssueToDepartment updated successfully.");
        }

        [HttpPost("Update")]
        public async Task<ApiResponse> Insert(IssueModel obj)
        {
            if (obj.Depissue == null || obj.curruntissue == null)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "Invalid input data.");
            }

            TIssueToDepartmentHeader model = obj.Depissue.MapTo<TIssueToDepartmentHeader>();
            TCurrentStock objCurrentStock = obj.curruntissue.MapTo<TCurrentStock>();

            if (obj.Depissue.IssueId == 0)
            {
                if (obj.curruntissue.ItemId == 0)
                {
                    //model.BatchExpDate = DateTime.Now.Date;
                }

                await _IIssueToDepService.UpdateAsync(model, objCurrentStock, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "IssueToDepartment Update  added successfully.");
            }
            else
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }
        }
        //[HttpPost("InsertSP")]
        ////[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> Insert(RefundBillModel obj)
        //{
        //    Refund model = obj.Refund.MapTo<Refund>();
        //    TRefundDetail objTRefundDetail = obj.TRefundDetails.MapTo<TRefundDetail>();
        //    AddCharge objAddCharge = obj.AddCharges.MapTo<AddCharge>();
        //    Payment objPayment = obj.Payment.MapTo<Payment>();
        //    if (obj.Refund.RefundId == 0)
        //    {
        //        model.RefundTime = Convert.ToDateTime(obj.Refund.RefundTime);
        //        model.AddedBy = CurrentUserId;

        //        obj.TRefundDetails.RefundId = obj.Refund.RefundId;
        //        objTRefundDetail.AddBy = CurrentUserId;
        //        objTRefundDetail.UpdatedBy = CurrentUserId;

        //        obj.AddCharges.ChargesId = obj.Refund.RefundId;


        //        obj.Payment.RefundId = obj.Refund.RefundId;
        //        objPayment.AddBy = CurrentUserId;
        //        objPayment.IsCancelledBy = CurrentUserId;

        //        await _IRefundOfBillService.InsertAsyncSP(model, objTRefundDetail, objAddCharge, objPayment, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Refund added successfully.");
        //}

        [HttpPost("UpdateIssue")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> UpdateIssue(TCurrentStockModel obj)
        {
            TCurrentStock model = obj.MapTo<TCurrentStock>();
            if (obj.ItemId == 0)
            {
                model.BatchExpDate = DateTime.Now.Date;
                await _IIssueToDepService.updateStock(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "updateissuetoDepartmentStock successfully.");
        }
    }
}
