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

namespace HIMS.API.Controllers.Inventory
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
        //[Permission(PageCode = "OpeningBalance", Permission = PagePermission.View)]
        public async Task<IActionResult> GetOpningBalance(GridRequestModel objGrid)
        {
            IPagedList<OpeningBalListDto> List1 = await _IOpeningBalanceService.GetOpeningBalanceList(objGrid);
            return Ok(List1.ToGridResponse(objGrid, "Opening Balance List"));
        }


        [HttpPost("OpeningBalnceItemDetailList")]
        //    [Permission(PageCode = "OpeningBalance", Permission = PagePermission.View)]
        public async Task<IActionResult> GetOpeningBalItemDetail(GridRequestModel objGrid)
        {
            IPagedList<OpeningBalanaceItemDetailListDto> List1 = await _IOpeningBalanceService.GetOPningBalItemDetailList(objGrid);
            return Ok(List1.ToGridResponse(objGrid, "Opening Balance Item Detail List"));
        }

        // changes done by Subhash -- Date : 19 May 2025
        [HttpPost("OpeningBalanceSave")]
        [Permission(PageCode = "OpeningBalance", Permission = PagePermission.Add)]
        public ApiResponse OpeningBalAsyncSp(OpeningBalanceModel obj)
        {
            TOpeningTransactionHeader Model = obj.OpeningBal.MapTo<TOpeningTransactionHeader>();
            List<TOpeningTransactionDetail> Models = obj.OpeningTransaction.MapTo<List<TOpeningTransactionDetail>>();

            if (obj.OpeningBal.OpeningHid == 0)
            {
                Model.CreatedBy = CurrentUserId;
                Model.OpeningDate = Convert.ToDateTime(Model.OpeningDate);
                _IOpeningBalanceService.OpeningBalSp(Model, Models, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Added successfully.");
        }
    }
}
