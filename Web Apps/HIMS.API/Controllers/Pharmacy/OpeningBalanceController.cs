using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Purchase;
using HIMS.Data.Models;
using HIMS.Services.Pharmacy;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Pharmacy
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class OpeningBalanceController : BaseController
    {
        private readonly IOpeningBalanceService _IOpeningBalanceService;
        public OpeningBalanceController(IOpeningBalanceService repository)
        {
            _IOpeningBalanceService = repository;
        }
        [HttpPost("OpeningBalanceList")]
        //[Permission(PageCode = "PurchaseOrder", Permission = PagePermission.View)]
        public async Task<IActionResult> GetOpningBalance(GridRequestModel objGrid)
        {
            IPagedList<OpeningBalListDto> List1 = await _IOpeningBalanceService.GetOpeningBalanceList(objGrid);
            return Ok(List1.ToGridResponse(objGrid, "Oening Balance List"));
        }
        

        [HttpPost("OpeningBalnceItemDetailList")]
        //[Permission(PageCode = "PurchaseOrder", Permission = PagePermission.View)]
        public async Task<IActionResult> GetOpeningBalItemDetail(GridRequestModel objGrid)
        {
            IPagedList<OpeningBalanaceItemDetailListDto> List1 = await _IOpeningBalanceService.GetOPningBalItemDetailList(objGrid);
            return Ok(List1.ToGridResponse(objGrid, "Opening Balance Item Detail List"));
        }

        [HttpPost("OpeningBalanceSave")]
       // [Permission(PageCode = "Bill", Permission = PagePermission.Add)]
        public async Task<ApiResponse> OpeningBalAsyncSp(OpeningBalanceModel obj)
        {
           TOpeningTransactionHeader Model = obj.OpeningBal.MapTo<TOpeningTransactionHeader>();
           List<TOpeningTransaction> Models = obj.OpeningTransaction.MapTo<List<TOpeningTransaction>>();
            if (obj.OpeningBal.OpeningHId == 0)
            {
                Model.AddedBy = CurrentUserId;
                Model.UpdatedBy = CurrentUserId;
                await _IOpeningBalanceService.OpeningBalAsyncSp( Model, Models,CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Opening Balance Save successfully.");
        }
    }
}
