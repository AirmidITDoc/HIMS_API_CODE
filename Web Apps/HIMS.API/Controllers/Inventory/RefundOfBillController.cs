using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.OutPatient;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Inventory
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class RefundOfBillController : BaseController
    {
        private readonly IRefundOfBillService _RefundOfBillService;
        public RefundOfBillController(IRefundOfBillService repository)
        {
            _RefundOfBillService = repository;
        }
       
        [HttpPost("RefundInsert")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(RefundBillModel obj)
        {
            Refund model = obj.Refund.MapTo<Refund>();
            TRefundDetail objTRefundDetail = obj.RefundDetail.MapTo<TRefundDetail>();
            if (obj.Refund.RefundId == 0)
            {
                model.RefundTime = Convert.ToDateTime(obj.Refund.RefundTime);
                model.AddBy = CurrentUserId;

                if (obj.RefundDetail.RefundDetId == 0)
                {
                    //objTRefundDetail.VisitTime = Convert.ToDateTime(obj.RefundDetail.VisitTime);
                    //objTRefundDetail.AddBy = CurrentUserId;
                    //objTRefundDetail.UpdatedBy = CurrentUserId;
                }
                await _RefundOfBillService.InsertAsyncSP(model, objTRefundDetail, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Refund added successfully.");
        }




    }
}
