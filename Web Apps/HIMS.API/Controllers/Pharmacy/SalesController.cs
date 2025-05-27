using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
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
                model.AddedBy = CurrentUserId;
                model.UpdatedBy = 0;
                foreach (var objItem in model.TSalesDetails)
                {
                    objItem.Sgstamt = (objItem.TotalAmount.Value - objItem.DiscAmount.Value) * 100 / ((decimal)(objItem.VatPer.Value + 100)) * (decimal)objItem.Cgstper / 100;
                    objItem.Cgstamt = (objItem.TotalAmount.Value - objItem.DiscAmount.Value) * 100 / ((decimal)(objItem.VatPer.Value + 100)) * (decimal)objItem.Cgstper / 100;
                }
                await _ISalesService.InsertAsync(model, objPayment, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Sales added successfully.");
        }


        [HttpPost("SalesSummaryList")]
        [Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> SalesSummaryList(GridRequestModel objGrid)
        {
            IPagedList<PharSalesCurrentSumryListDto> PharSalesList = await _ISalesService.GetList(objGrid);
            return Ok(PharSalesList.ToGridResponse(objGrid, "Sales Summary  List"));
        }

        [HttpPost("SalesDetailsList")]
        [Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> SalesDetailsList(GridRequestModel objGrid)
        {
            IPagedList<PharCurrentDetListDto> SalesDetailsList = await _ISalesService.SalesDetailsList(objGrid);
            return Ok(SalesDetailsList.ToGridResponse(objGrid, "Sales Details  List"));
        }

        // Changes done by Subhash Date : 17-May-2025
        [HttpPost("SalesBrowseDetailList")]
        [Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> salesdetaillist(GridRequestModel objGrid)
        {
            IPagedList<SalesDetailsListDto> SalesDetailsList = await _ISalesService.Getsalesdetaillist(objGrid);
            return Ok(SalesDetailsList.ToGridResponse(objGrid, "Sales Details List"));
        }

        // Changes done by Subhash Date : 17-May-2025
        [HttpPost("salesbrowselist")]
        [Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> salesbrowselist(GridRequestModel objGrid)
        {
            IPagedList<SalesBillListDto> SalesDetailsList = await _ISalesService.salesbrowselist(objGrid);
            return Ok(SalesDetailsList.ToGridResponse(objGrid, "Sales Bill List"));
        }
        // done by Ashu Date : 20-May-2025
        [HttpPost("salesDraftlist")]
        [Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> SalesDraftBillList(GridRequestModel objGrid)
        {
            IPagedList<SalesDraftBillListDto> SalesDraftBillList = await _ISalesService.SalesDraftBillList(objGrid);
            return Ok(SalesDraftBillList.ToGridResponse(objGrid, "Sales Draft List"));
        }


        [HttpPost("StockavailableList")]
        [Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> BalAvaStoreList(GridRequestModel objGrid)
        {
            IPagedList<BalAvaStoreListDto> BalAvaStoreList = await _ISalesService.BalAvaStoreList(objGrid);
            return Ok(BalAvaStoreList.ToGridResponse(objGrid, "Stockavailable List"));
        }

        [HttpPost("Prescriptionheaderlist")]
        [Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> PrescriptionList(GridRequestModel objGrid)
        {
            IPagedList<PrescriptionListforSalesDto> PrescriptionList = await _ISalesService.PrescriptionList(objGrid);
            return Ok(PrescriptionList.ToGridResponse(objGrid, "PrescriptionListforSales List"));
        }

        [HttpPost("PrescriptionDetaillist")]
        [Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> PrescriptionDetList(GridRequestModel objGrid)
        {
            IPagedList<PrescriptionDetListDto> PrescriptionDetList = await _ISalesService.PrescriptionDetList(objGrid);
            return Ok(PrescriptionDetList.ToGridResponse(objGrid, "PrescriptionDet  List"));
        }

        [HttpPost("PharSalesSettlemet")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> PharSalesBillSettlemet(GridRequestModel objGrid)
        {
            IPagedList<Pharbillsettlementlist> PrescriptionDetList = await _ISalesService.PharIPBillSettlement(objGrid);
            return Ok(PrescriptionDetList.ToGridResponse(objGrid, "PrescriptionDet  List"));
        }

        [HttpPost("BrowseIPPharAdvanceReceiptList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> BrowseIPPharAdvanceReceiptList(GridRequestModel objGrid)
        {
            IPagedList<BrowseIPPharAdvanceReceiptListDto> PrescriptionDetList = await _ISalesService.BrowseIPPharAdvanceReceiptList(objGrid);
            return Ok(PrescriptionDetList.ToGridResponse(objGrid, "BrowseIPPharAdvanceReceiptList"));
        }

        [HttpPost("PharAdvanceList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> PharAdvanceList(GridRequestModel objGrid)
        {
            IPagedList<PharAdvanceListDto> PrescriptionDetList = await _ISalesService.PharAdvanceList(objGrid);
            return Ok(PrescriptionDetList.ToGridResponse(objGrid, "PharAdvanceList"));
        }

        // done by Ashu Date : 20-May-2025

        [HttpPost("SalesSaveWithPayment")]
        [Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertSP(SaleReqModel obj)
        {
            TSalesHeader model = obj.Sales.MapTo<TSalesHeader>();
            List<TCurrentStock> CurrentStock = obj.TCurrentStock.MapTo<List<TCurrentStock>>();
            Payment modelPayment = obj.Payment.MapTo<Payment>();
            TIpPrescription modelPrescription = obj.Prescription.MapTo<TIpPrescription>();
            TSalesDraftHeader modelDraftHeader = obj.SalesDraft.MapTo<TSalesDraftHeader>();


            if (obj.Sales.SalesId == 0)
            {
                model.Date = Convert.ToDateTime(obj.Sales.Date);
                model.Time = Convert.ToDateTime(obj.Sales.Time);
                model.AddedBy = CurrentUserId;
                await _ISalesService.InsertAsyncSP(model, CurrentStock, modelPayment, modelPrescription, modelDraftHeader, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }



        // done by Ashu Date : 20-May-2025

        [HttpPost("SalesSaveWithCredit")]
        [Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertSPC(SaleReqModel obj)
        {
            TSalesHeader model = obj.Sales.MapTo<TSalesHeader>();
            List<TCurrentStock> CurrentStock = obj.TCurrentStock.MapTo<List<TCurrentStock>>();
            TIpPrescription modelPrescription = obj.Prescription.MapTo<TIpPrescription>();
            TSalesDraftHeader modelDraftHeader = obj.SalesDraft.MapTo<TSalesDraftHeader>();

            if (obj.Sales.SalesId == 0)
            {
                model.Date = Convert.ToDateTime(obj.Sales.Date);
                model.Time = Convert.ToDateTime(obj.Sales.Time);
                model.AddedBy = CurrentUserId;
                await _ISalesService.InsertAsyncSPC(model, CurrentStock, modelPrescription, modelDraftHeader, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        [HttpPost("SalesDraftBillSave")]
        [Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertSPD(SalesDraftHeadersModel obj)
        {
            TSalesDraftHeader model = obj.SalesDraft.MapTo<TSalesDraftHeader>();
            List<TSalesDraftDet> modelTSales = obj.SalesDraftDet.MapTo<List<TSalesDraftDet>>();
            if (obj.SalesDraft.DsalesId == 0)
            {
                model.Date = Convert.ToDateTime(obj.SalesDraft.Date);
                model.Time = Convert.ToDateTime(obj.SalesDraft.Time);
                model.AddedBy = CurrentUserId;
                await _ISalesService.InsertAsyncSPD(model, modelTSales, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "SalesDraftBill added successfully.");
        }

        [HttpPost("PharmacyAdvanceInsert")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(PharAdvanceModel obj)
        {
            TPhadvanceHeader model = obj.PharmacyAdvance.MapTo<TPhadvanceHeader>();
            TPhadvanceDetail model1 = obj.PharmacyAdvanceDetails.MapTo<TPhadvanceDetail>();
            PaymentPharmacy model3 = obj.PaymentPharmacy.MapTo<PaymentPharmacy>();

            
            if (obj.PharmacyAdvance.AdvanceId == 0)
            {
                model.Date = Convert.ToDateTime(obj.PharmacyAdvance.Date);
                model.AddedBy = CurrentUserId;
                await _ISalesService.InsertAsyncS(model, model1, model3, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PharmacyAdvance added successfully.");
        }

    }

}
