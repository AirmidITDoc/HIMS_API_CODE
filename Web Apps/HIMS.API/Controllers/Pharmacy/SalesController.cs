using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.API.Models.Pharmacy;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;
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
    public class SalesController : BaseController
    {
        private readonly ISalesService _ISalesService;
        public SalesController(ISalesService repository)
        {
            _ISalesService = repository;
        }
        [HttpPost]
        //[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public async Task<ApiResponse> Post(SalesReqDto obj)
        {
            TSalesHeader model = obj.Sales.MapTo<TSalesHeader>();
            Payment objPayment = obj.Payment.MapTo<Payment>();
            if (obj.Sales.SalesId == 0)
            {
                model.Date = DateTime.Now.Date;
                model.Time = DateTime.Now;
                foreach (var objItem in model.TSalesDetails)
                {
                    objItem.Sgstamt = (objItem.TotalAmount.Value - objItem.DiscAmount.Value) * 100 / ((decimal)(objItem.VatPer.Value + 100)) * (decimal)objItem.Cgstper / 100;
                    objItem.Cgstamt = (objItem.TotalAmount.Value - objItem.DiscAmount.Value) * 100 / ((decimal)(objItem.VatPer.Value + 100)) * (decimal)objItem.Cgstper / 100;
                }
                await _ISalesService.InsertAsync(model, objPayment, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Prefix added successfully.");
        }

        [HttpPost("SalesList")]
     //   [Permission(PageCode = "Menu", Permission = PagePermission.View)]
        public async Task<IActionResult> PharSalesList(GridRequestModel objGrid)
        {
            IPagedList<PharSalesListDto> PharSalesList = await _ISalesService.GetListAsync(objGrid);
            return Ok(PharSalesList.ToGridResponse(objGrid, "Phar Sales  List"));
        }
        [HttpPost("SalesSummaryList")]
        ///   [Permission(PageCode = "Menu", Permission = PagePermission.View)]
        public async Task<IActionResult> SalesSummaryList(GridRequestModel objGrid)
        {
            IPagedList<PharSalesCurrentSumryListDto> PharSalesList = await _ISalesService.GetList(objGrid);
            return Ok(PharSalesList.ToGridResponse(objGrid, "Sales Summary  List"));
        }

        [HttpPost("SalesDetailsList")]
        ///   [Permission(PageCode = "Menu", Permission = PagePermission.View)]
        public async Task<IActionResult> SalesDetailsList(GridRequestModel objGrid)
        {
            IPagedList<PharCurrentDetListDto> SalesDetailsList = await _ISalesService.SalesDetailsList(objGrid);
            return Ok(SalesDetailsList.ToGridResponse(objGrid, "Sales Details  List"));
        }


        [HttpPost("SalesBrowseDetailList")]
        ///   [Permission(PageCode = "Menu", Permission = PagePermission.View)]
        public async Task<IActionResult> salesdetaillist(GridRequestModel objGrid)
        {
            IPagedList<SalesDetailsListDto> SalesDetailsList = await _ISalesService.Getsalesdetaillist(objGrid);
            return Ok(SalesDetailsList.ToGridResponse(objGrid, "Sales Details  List"));
        }


        [HttpPost("salesbrowselist")]
        ///   [Permission(PageCode = "Menu", Permission = PagePermission.View)]
        public async Task<IActionResult> salesbrowselist(GridRequestModel objGrid)
        {
            IPagedList<SalesBillListDto> SalesDetailsList = await _ISalesService.salesbrowselist(objGrid);
            return Ok(SalesDetailsList.ToGridResponse(objGrid, "Sales Details  List"));
        }

     
    }
}
