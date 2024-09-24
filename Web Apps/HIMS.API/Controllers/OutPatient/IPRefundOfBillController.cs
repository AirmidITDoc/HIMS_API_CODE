using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.OutPatient;
using HIMS.Data.Models;
using HIMS.Services.OutPatient;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.OutPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class IPRefundOfBillController : BaseController
    {
        private readonly IOPRefundOfBillService _IIPRefundOfBillService;
        public IPRefundOfBillController(IOPRefundOfBillService repository)
        {
            _IIPRefundOfBillService = repository;
        }
        [HttpPost("IPRefundInsert")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(IPRefundBillModel obj)
        {
            Refund model = obj.IPRefund.MapTo<Refund>();
            TRefundDetail objIPTRefundDetail = obj.IPRefundDetail.MapTo<TRefundDetail>();
            if (obj.IPRefund.RefundId == 0)
            {
                model.RefundTime = Convert.ToDateTime(obj.IPRefund.RefundTime);
                model.AddedBy = CurrentUserId;

                //if (obj.IPRefundDetail.RefundDetId == 0)
                {
                    //objIPTRefundDetail.RefundDetailsTime = Convert.ToDateTime(obj.IPRefundDetail.RefundDetailsTime);
                    objIPTRefundDetail.AddBy = CurrentUserId;
                    objIPTRefundDetail.UpdatedBy = CurrentUserId;
                }
                //await _IIPRefundOfBillService.InsertAsyncSP(model, objIPTRefundDetail, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "IPRefund added successfully.");
        }
    }
}
