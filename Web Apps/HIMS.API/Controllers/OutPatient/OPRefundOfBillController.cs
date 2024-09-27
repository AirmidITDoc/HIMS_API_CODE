using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.OutPatient;
using HIMS.Data.Models;
using HIMS.Services.OutPatient;
using Microsoft.AspNetCore.Mvc;
//using static HIMS.API.Models.OutPatient.phoneAppModelValidator;

namespace HIMS.API.Controllers.OutPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class OPRefundOfBillController : BaseController
    {
        private readonly IOPRefundOfBillService _IRefundOfBillService;
        public OPRefundOfBillController(IOPRefundOfBillService repository)
        {
            _IRefundOfBillService = repository;
        }


        [HttpPost("InsertSP")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(RefundBillModel obj)
        {
            Refund model = obj.Refund.MapTo<Refund>();
            TRefundDetail objTRefundDetail = obj.TRefundDetails.MapTo<TRefundDetail>();
            if (obj.Refund.RefundId == 0)
            {
                model.RefundTime = Convert.ToDateTime(obj.Refund.RefundTime);
                model.AddedBy = CurrentUserId;

                obj.TRefundDetails.RefundId = obj.Refund.RefundId;
                objTRefundDetail.AddBy = CurrentUserId;
                objTRefundDetail.UpdatedBy = CurrentUserId;

                await _IRefundOfBillService.InsertAsyncSP(model, objTRefundDetail, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Refund added successfully.");
        }


        //[HttpPost("InsertIpSP")]
        ////[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> Insert(RefundBillModel obj)
        //{
        //    Refund model = obj.Refund.MapTo<Refund>();
        //    TRefundDetail objTRefundDetail = obj.TRefundDetails.MapTo<TRefundDetail>();
        //    if (obj.Refund.RefundId == 0)
        //    {
        //        model.RefundTime = Convert.ToDateTime(obj.Refund.RefundTime);
        //        model.AddedBy = CurrentUserId;

        //        obj.TRefundDetails.RefundId = obj.Refund.RefundId;
        //        objTRefundDetail.AddBy = CurrentUserId;
        //        objTRefundDetail.UpdatedBy = CurrentUserId;

        //        await _IRefundOfBillService.InsertAsyncSP(model, objTRefundDetail, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Refund added successfully.");
        //}

        [HttpPost("InsertEDMX")]
        //[Permission(PageCode = "ItemMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(OPRefundOfBillModel obj)
        {
            Refund model = obj.MapTo<Refund>();
            object returnId=0;
            if (obj.RefundId == 0)
            {
                model.RefundTime = Convert.ToDateTime(obj.RefundTime);
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = CurrentUserId;
                model.AddedBy = CurrentUserId;
                returnId = await _IRefundOfBillService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Refund added successfully.", returnId);
        }
        //[HttpPut("Edit/{id:int}")]
        ////[Permission(PageCode = "ItemMaster", Permission = PagePermission.Edit)]
        //public async Task<ApiResponse> Edit(OPRefundOfBillModel obj)
        //{
        //    Refund model = obj.MapTo<Refund>();
        //    if (obj.RefundId == 0)
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    else
        //    {
        //        model.RefundTime = Convert.ToDateTime(obj.RefundTime);
        //        await _IRefundOfBillService.UpdateAsync(model, CurrentUserId, CurrentUserName);
        //    }
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Refund updated successfully.");
        //}

    }
}

