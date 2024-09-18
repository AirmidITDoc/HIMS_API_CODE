using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
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
        [HttpPost("Insert")]
        //[Permission(PageCode = "TestMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(RefundOfBillModel obj)
        {
            Refund model = obj.MapTo<Refund>();
            if (obj.RefundId == 0)
            {
                //model.CreatedDate = DateTime.Now;
                model.AddBy = CurrentUserId;
                //model.IsActive = true;
                await _RefundOfBillService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Refund added successfully.");
        }
    }
}
