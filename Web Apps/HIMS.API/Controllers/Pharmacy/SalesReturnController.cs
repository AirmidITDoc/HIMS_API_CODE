using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Masters;
using HIMS.API.Models.OPPatient;
using HIMS.API.Models.Pharmacy;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;
using HIMS.Services.OPPatient;
using HIMS.Services.Pharmacy;
using HIMS.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security;

namespace HIMS.API.Controllers.Pharmacy
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class SalesReturnController : BaseController
    {
        private readonly ISalesReturnService _ISalesReturnService;
        public SalesReturnController(ISalesReturnService repository)
        {
            _ISalesReturnService = repository;
        }

     
        [HttpPost("SalesReturnSummaryList")]
        [Permission(PageCode = "SalesReturn", Permission = PagePermission.View)]
        public async Task<IActionResult> SalesSummaryList(GridRequestModel objGrid)
        {
            IPagedList<SalesRetrunCurrentSumryListDto> PharSalesList = await _ISalesReturnService.SalesReturnSummaryList(objGrid);
            return Ok(PharSalesList.ToGridResponse(objGrid, "Sales Return Summary   List"));
        }

        [HttpPost("SalesReturnDetailsList")]
        [Permission(PageCode = "SalesReturn", Permission = PagePermission.View)]
        public async Task<IActionResult> SalesReturnDetailsList(GridRequestModel objGrid)
        {
            IPagedList<SalesRetrunLCurrentDetListDto> SalesDetailsList = await _ISalesReturnService.SalesReturnDetailsList(objGrid);
            return Ok(SalesDetailsList.ToGridResponse(objGrid, "Sales Return Details  List"));
        }
        [HttpPost("SalesReturnBrowseList")]
        [Permission(PageCode = "SalesReturn", Permission = PagePermission.View)]
        public async Task<IActionResult> salesreturnlist(GridRequestModel objGrid)
        {
            IPagedList<SalesReturnBillListDto> salesreturnlist = await _ISalesReturnService.salesreturnlist(objGrid);
            return Ok(salesreturnlist.ToGridResponse(objGrid, "Sales Return Details  List"));
        }

        [HttpPost("salesReturnBrowseDetaillist")]
        [Permission(PageCode = "SalesReturn", Permission = PagePermission.View)]
        public async Task<IActionResult> salesreturndetaillist(GridRequestModel objGrid)
        {
            IPagedList<SalesReturnDetailsListDto> SalesDetailsList = await _ISalesReturnService.salesreturndetaillist(objGrid);
            return Ok(SalesDetailsList.ToGridResponse(objGrid, "Sales Return Details  List"));
        }

        [HttpPost("salesbilllist")]
        [Permission(PageCode = "SalesReturn", Permission = PagePermission.View)]
        public async Task<IActionResult> BrowseSalesBillList(GridRequestModel objGrid)
        {
            IPagedList<BrowseSalesBillListDto> salesbilllist = await _ISalesReturnService.BrowseSalesBillList(objGrid);
            return Ok(salesbilllist.ToGridResponse(objGrid, "sales bill  List"));
        }

        [HttpPost("salesbillwithcash")]
        [Permission(PageCode = "SalesReturn", Permission = PagePermission.View)]
        public async Task<IActionResult> SalesBillReturnCashList(GridRequestModel objGrid)
        {
            IPagedList<SalesBillReturnCashListDto> salesbillwithcash = await _ISalesReturnService.SalesBillReturnCashList(objGrid);
            return Ok(salesbillwithcash.ToGridResponse(objGrid, "salesbillwithcash  List"));
        }
        [HttpPost("salesbillwithcredit")]
        [Permission(PageCode = "SalesReturn", Permission = PagePermission.View)]
        public async Task<IActionResult> SalesBillReturnCreditList(GridRequestModel objGrid)
        {
            IPagedList<SalesBillReturnCreditListDto> salesbillwithcredit = await _ISalesReturnService.SalesBillReturnCreditList(objGrid);
            return Ok(salesbillwithcredit.ToGridResponse(objGrid, "salesbillwithcredit  List"));
        }
        [HttpPost("SalesReturnWithCash")]
        [Permission(PageCode = "SalesReturn", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertSP(SalesReturnsModel obj)
        {
            TSalesReturnHeader model = obj.SalesReturn.MapTo<TSalesReturnHeader>();
            List <TSalesReturnDetail> model1 = obj.SalesReturnDetails.MapTo<List<TSalesReturnDetail>>();
            List<TCurrentStock> model2 = obj.CurrentStock.MapTo<List<TCurrentStock>>();
            List<TSalesDetail> model3 = obj.SalesDetail.MapTo<List<TSalesDetail>>();
            PaymentPharmacy model4 = obj.Payment.MapTo<PaymentPharmacy>();

            if (obj.SalesReturn.SalesReturnId == 0)
            {
                model.Date = Convert.ToDateTime(obj.SalesReturn.Date);
                model.Time = Convert.ToDateTime(obj.SalesReturn.Time);
                await _ISalesReturnService.InsertAsyncSP(model, model1, model2, model3, model4, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.",model.SalesReturnId);
        }


        [HttpPost("SalesReturnWithCredit")]
        [Permission(PageCode = "SalesReturn", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertSPC(SalesReturnsModel obj)
        {
            TSalesReturnHeader model = obj.SalesReturn.MapTo<TSalesReturnHeader>();
            List<TSalesReturnDetail> model1 = obj.SalesReturnDetails.MapTo<List<TSalesReturnDetail>>();
            List<TCurrentStock> model2 = obj.CurrentStock.MapTo<List<TCurrentStock>>();
            List<TSalesDetail> model3 = obj.SalesDetail.MapTo<List<TSalesDetail>>();        

            if (obj.SalesReturn.SalesReturnId == 0)
            {
                model.Date = Convert.ToDateTime(obj.SalesReturn.Date);
                model.Time = Convert.ToDateTime(obj.SalesReturn.Time);
                await _ISalesReturnService.InsertAsyncSPCredit(model, model1, model2, model3,  CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.",model.SalesReturnId);
        }

    }
}
