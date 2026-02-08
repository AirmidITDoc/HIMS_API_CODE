using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.OPPatient;
using HIMS.API.Models.OutPatient;
using HIMS.API.Models.Pharmacy;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data.Models;
using HIMS.Services.Pharmacy;
using HIMS.Services.Users;
using Microsoft.AspNetCore.Mvc;

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
        [Permission]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
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
        [Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> PharSalesBillSettlemet(GridRequestModel objGrid)
        {
            IPagedList<Pharbillsettlementlist> PrescriptionDetList = await _ISalesService.PharIPBillSettlement(objGrid);
            return Ok(PrescriptionDetList.ToGridResponse(objGrid, "PrescriptionDet  List"));
        }

        [HttpPost("BrowseIPPharAdvanceReceiptList")]
        [Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> BrowseIPPharAdvanceReceiptList(GridRequestModel objGrid)
        {
            IPagedList<BrowseIPPharAdvanceReceiptListDto> PrescriptionDetList = await _ISalesService.BrowseIPPharAdvanceReceiptList(objGrid);
            return Ok(PrescriptionDetList.ToGridResponse(objGrid, "BrowseIPPharAdvanceReceiptList"));
        }

        [HttpPost("PharAdvanceList")]
        [Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> PharAdvanceList(GridRequestModel objGrid)
        {
            IPagedList<PharAdvanceListDto> PrescriptionDetList = await _ISalesService.PharAdvanceList(objGrid);
            return Ok(PrescriptionDetList.ToGridResponse(objGrid, "PharAdvanceList"));
        }

        [HttpPost("PhAdvRefundReceiptList")]
        [Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> PhAdvRefundReceiptList(GridRequestModel objGrid)
        {
            IPagedList<PhAdvRefundReceiptListDto> PhAdvRefundReceiptList = await _ISalesService.PhAdvRefundReceiptList(objGrid);
            return Ok(PhAdvRefundReceiptList.ToGridResponse(objGrid, "PhAdvRefundReceiptList"));
        }
        [HttpPost("PhARefundOfAdvanceList")]
        [Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> PhARefundOfAdvanceList(GridRequestModel objGrid)
        {
            IPagedList<PhARefundOfAdvanceListDto> PhARefundOfAdvanceList = await _ISalesService.PhARefundOfAdvanceList(objGrid);
            return Ok(PhARefundOfAdvanceList.ToGridResponse(objGrid, "PhARefundOfAdvanceList"));
        }
        //Create By Ashu 28 May 2025
        [HttpPost("BalqtysalesDraftlist")]
        [Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> BalqtysalesDraftlist(GridRequestModel objGrid)
        {
            IPagedList<ItemNameBalanceQtyListDto> BalqtysalesDraftlist = await _ISalesService.BalqtysalesDraftlist(objGrid);
            return Ok(BalqtysalesDraftlist.ToGridResponse(objGrid, "BalqtysalesDraft List"));
        }

        [HttpPost("BalqtysalesDraftlistKenya")]
        [Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> BalqtysalesDraftlistKenya(GridRequestModel objGrid)
        {
            IPagedList<ItemNameBalanceQtyListDtoKenya> BalqtysalesDraftlist = await _ISalesService.BalqtysalesDraftlistKenya(objGrid);
            return Ok(BalqtysalesDraftlist.ToGridResponse(objGrid, "BalqtysalesDraft List"));
        }

        //Create By Ashu 28 jun 2025
        [HttpPost("GetRefundByAdvanceList")]
        [Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> GetRefundByAdvanceId(GridRequestModel objGrid)
        {
            IPagedList<GetRefundByAdvanceIdListDto> GetRefundByAdvancelist = await _ISalesService.GetRefundByAdvanceId(objGrid);
            return Ok(GetRefundByAdvancelist.ToGridResponse(objGrid, "GetRefundByAdvance List"));
        }

        [HttpPost("SalesDraftBillItemDet")]
        [Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> SalesDraftBillItemDet(GridRequestModel objGrid)
        {
            IPagedList<SalesDraftBillItemListDto> GetRefundByAdvancelist = await _ISalesService.SalesDraftBillItemDet(objGrid);
            return Ok(GetRefundByAdvancelist.ToGridResponse(objGrid, "SalesDraftBillItemDet List"));
        }

        [HttpPost("PrescriptionItemDetList")]
        [Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> PrescriptionItemDetList(GridRequestModel objGrid)
        {
            IPagedList<PrescriptionItemDetListDto> PrescriptionItemDetList = await _ISalesService.PrescriptionItemDetList(objGrid);
            return Ok(PrescriptionItemDetList.ToGridResponse(objGrid, "PrescriptionItemDet List"));
        }

        [HttpPost("SalesPatientWiseCreditAmountList")]
        [Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> salespatientwiseList(GridRequestModel objGrid)
        {
            IPagedList<salespatientwiseListDto> salespatientwiseList = await _ISalesService.salespatientwiseList(objGrid);
            return Ok(salespatientwiseList.ToGridResponse(objGrid, "salespatientwiseList List"));
        }

        [HttpPost("ItemGenericByNameList")]
        [Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> saleList(GridRequestModel objGrid)
        {
            IPagedList<ItemGenericByNameListDto> ItemGenericByNameList = await _ISalesService.ItemGenericByNameList(objGrid);
            return Ok(ItemGenericByNameList.ToGridResponse(objGrid, "ItemGenericByName List"));
        }

        // done by Ashu Date : 20-May-2025
        [HttpPost("SalesSaveWithPayment")]
        [Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertSP(SaleReqModel obj)
        {
            TSalesHeader model = obj.Sales.MapTo<TSalesHeader>();
            List<TCurrentStock> CurrentStock = obj.TCurrentStock.MapTo<List<TCurrentStock>>();
            PaymentPharmacy modelPayment = obj.Payment.MapTo<PaymentPharmacy>();
            TIpPrescription modelPrescription = obj.Prescription.MapTo<TIpPrescription>();
            TSalesDraftHeader modelDraftHeader = obj.SalesDraft.MapTo<TSalesDraftHeader>();
            List<TPaymentPharmacy> TPayments = obj.TPayments.MapTo<List<TPaymentPharmacy>>();

            string stockmsg = "";
            foreach (var item in model.TSalesDetails)
            {
                var stock = await _ISalesService.GetStock(item.StkId.Value);
                if (stock < item.Qty)
                {
                    stockmsg += item.BatchNo + " has only " + stock + " available stock. \n";
                }
            }
            if (!string.IsNullOrWhiteSpace(stockmsg))
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, stockmsg);
            }
            if (obj.Sales.SalesId == 0)
            {
                model.Date = Convert.ToDateTime(obj.Sales.Date);
                model.Time = Convert.ToDateTime(obj.Sales.Time);
                model.AddedBy = CurrentUserId;
                await _ISalesService.InsertSalesSaveWithPayment(model, CurrentStock, modelPayment, modelPrescription, modelDraftHeader, TPayments ,CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.SalesId);
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
            string stockmsg = "";
            foreach (var item in model.TSalesDetails)
            {
                var stock = await _ISalesService.GetStock(item.StkId.Value);
                if (stock < item.Qty)
                {
                    stockmsg += item.BatchNo + " has only " + stock + " available stock. \n";
                }
            }
            if (!string.IsNullOrWhiteSpace(stockmsg))
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, stockmsg);
            }
            if (obj.Sales.SalesId == 0)
            {
                model.Date = Convert.ToDateTime(obj.Sales.Date);
                model.Time = Convert.ToDateTime(obj.Sales.Time);
                model.AddedBy = CurrentUserId;
                await _ISalesService.InsertAsyncSPC(model, CurrentStock, modelPrescription, modelDraftHeader, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.SalesId);
        }

        // done by Ashu Date : 20-May-2025
        [HttpPost("SaveSalesInpatient")]
        [Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> SaveSalesInpatient(SaleReqModelInpatient obj)
        {
            TSalesInpatientHeader model = obj.Sales.MapTo<TSalesInpatientHeader>();
            List<TCurrentStock> CurrentStock = obj.TCurrentStock.MapTo<List<TCurrentStock>>();
            TIpPrescription modelPrescription = obj.Prescription.MapTo<TIpPrescription>();
            TSalesDraftHeader modelDraftHeader = obj.SalesDraft.MapTo<TSalesDraftHeader>();
            string stockmsg = "";
            foreach (var item in model.TSalesInpatientDetails)
            {
                var stock = await _ISalesService.GetStock(item.StkId.Value);
                if (stock < item.Qty)
                {
                    stockmsg += item.BatchNo + " has only " + stock + " available stock. \n";
                }
            }
            if (!string.IsNullOrWhiteSpace(stockmsg))
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, stockmsg);
            }
            if (obj.Sales.SalesId == 0)
            {
                model.Date = Convert.ToDateTime(obj.Sales.Date);
                model.Time = Convert.ToDateTime(obj.Sales.Time);
                model.AddedBy = CurrentUserId;
                await _ISalesService.InsertSalesInPatientAsyncSPC(model, CurrentStock, modelPrescription, modelDraftHeader, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.SalesId);
        }

        [HttpPost("SalesDraftBillSave")]
        [Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public ApiResponse InsertSPD(SalesDraftHeadersModel obj)
        {
            TSalesDraftHeader model = obj.SalesDraft.MapTo<TSalesDraftHeader>();
            List<TSalesDraftDet> modelTSales = obj.SalesDraftDet.MapTo<List<TSalesDraftDet>>();
            if (obj.SalesDraft.DsalesId == 0)
            {
                model.Date = Convert.ToDateTime(obj.SalesDraft.Date);
                model.Time = Convert.ToDateTime(obj.SalesDraft.Time);
                model.AddedBy = CurrentUserId;
                _ISalesService.InsertSPD(model, modelTSales, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.DsalesId);
        }

        [HttpPost("SalesDraftbillcancel")]
        [Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public ApiResponse Delete(TSalesDraftsHeaderModel obj)
        {
            TSalesDraftHeader model = obj.MapTo<TSalesDraftHeader>();
            if (obj.DsalesId != 0)
            {
                _ISalesService.Delete(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Cancelled successfully.");
        }


        //shilpa 26/05/2025//

        [HttpPost("PharmacyAdvanceInsert")]
        [Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public ApiResponse Insert(PharAdvanceModel obj)
        {
            TPhadvanceHeader model = obj.PharmacyAdvance.MapTo<TPhadvanceHeader>();
            TPhadvanceDetail model1 = obj.PharmacyAdvanceDetails.MapTo<TPhadvanceDetail>();
            PaymentPharmacy model3 = obj.PaymentPharmacy.MapTo<PaymentPharmacy>();
            List<TPaymentPharmacy> TPayments = obj.TPayments.MapTo<List<TPaymentPharmacy>>();


            if (obj.PharmacyAdvance.AdvanceId == 0)
            {
                model.Date = Convert.ToDateTime(obj.PharmacyAdvance.Date);
                model.AddedBy = CurrentUserId;
                _ISalesService.InsertS(model, model1, model3, TPayments, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model1.AdvanceDetailId);
        }
        [HttpPut("PharmacyAdvanceUpdate")]
        [Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public ApiResponse Update(PharmacyHeaderUpdate obj)
        {
            TPhadvanceHeader model = obj.PharmacyHeader.MapTo<TPhadvanceHeader>();
            TPhadvanceDetail model1 = obj.PharmacyAdvanceDetails.MapTo<TPhadvanceDetail>();
            PaymentPharmacy model3 = obj.PaymentPharmacy.MapTo<PaymentPharmacy>();
            List<TPaymentPharmacy> TPayments = obj.TPayments.MapTo<List<TPaymentPharmacy>>();




            if (obj.PharmacyHeader.AdvanceId != 0)
            {
                model.AddedBy = CurrentUserId;
                _ISalesService.UpdateS(model, model1, model3, TPayments, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Update  successfully.", model1.AdvanceDetailId);
        }
        //shilpa 27/05/2025//

        [HttpPost("PharmacyRefundInsert")]
        [Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public ApiResponse InsertSP(PharRefundModel obj)
        {
            TPhRefund model = obj.PharmacyRefund.MapTo<TPhRefund>();
            TPhadvanceHeader model1 = obj.PhAdvanceHeader.MapTo<TPhadvanceHeader>();
            List<TPhadvRefundDetail> model3 = obj.PHAdvRefundDetail.MapTo<List<TPhadvRefundDetail>>();
            List<TPhadvanceDetail> model4 = obj.PHAdvanceDetailBalAmount.MapTo<List<TPhadvanceDetail>>();
            PaymentPharmacy model5 = obj.PharPayment.MapTo<PaymentPharmacy>();
            List<TPaymentPharmacy> TPayments = obj.TPayments.MapTo<List<TPaymentPharmacy>>();


            if (obj.PharmacyRefund.RefundId == 0)
            {
                model.AddBy = CurrentUserId;
                _ISalesService.InsertR(model, model1, model3, model4, model5, TPayments, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Added Successfully.", model.RefundId);
        }


        [HttpPost("PaymentSettlement")]
        [Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public ApiResponse Insert(PharmacyModel obj)
        {
            List<PaymentPharmacy> model = obj.Payment.MapTo<List<PaymentPharmacy>>();
            List<TSalesHeader> model1 = obj.Saless.MapTo<List<TSalesHeader>>();
            List<AdvanceDetail> model2 = obj.AdvanceDetail.MapTo<List<AdvanceDetail>>();
            AdvanceHeader model3 = obj.AdvanceHeader.MapTo<AdvanceHeader>();
            List<TPaymentPharmacy> TPayments = obj.TPayments.MapTo<List<TPaymentPharmacy>>();



            if (model.Count > 0)
            {
                _ISalesService.Insert(model, model1, model2, model3, TPayments, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Added Successfully.");
        }
        [HttpPost("PhBillDiscountAfter")]
        [Permission(PageCode = "Sales", Permission = PagePermission.Edit)]
        public ApiResponse InsertSP(PhBillDiscountAfterModel obj)
        {

            TSalesHeader Model = obj.MapTo<TSalesHeader>();
            if (obj.SalesId != 0)
            {
                _ISalesService.InsertPhBillDiscount(Model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Updated Successfully.");
        }

        [HttpPost("ExtpatientDetUpdate")]
        [Permission(PageCode = "Sales", Permission = PagePermission.Edit)]
        public ApiResponse InsertSP(ExtpatientDetModel obj)
        {

            TSalesHeader Model = obj.MapTo<TSalesHeader>();
            if (obj.SalesId != 0)
            {
                _ISalesService.InsertSP(Model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Updated Successfully.");
        }


        [HttpGet("External-auto-complete")]
        [Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetAutoComplete(string Keyword)
        {
            var data = await _ISalesService.SearchRegistration(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Sales External Patient Data.", data.Select(x => new
            {
                Text = x.ExternalPatientName + " | " + x.ExtMobileNo + " | " + x.DoctorName,
                Value = x.ExtMobileNo,
                x.ExtMobileNo,
                x.DoctorName,
                PatientName = x.ExternalPatientName
            }));
        }


        [HttpGet("ExternalDoctor-auto-complete")]
        [Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetExtDocAutoComplete(string Keyword)
        {
            var data = await _ISalesService.SearchExtDoctor(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Sales External Doctor Data.", data.Select(x => new
            {
                Text = x.DoctorName,
                Value = x.DoctorName,
                x.DoctorName,
            }));
        }
      
        [HttpPut("SalesUpdate/{id:int}")]
        [Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Update(SalesUpdate obj)
        {
            TSalesHeader objSalesHeader = obj.SalesHeader.MapTo<TSalesHeader>();
            List<TSalesDetail> ObjSalesDetail = obj.SalesDetails.MapTo<List<TSalesDetail>>();
            List<TCurrentStock> model2 = obj.CurrentStockUpdate.MapTo<List<TCurrentStock>>();
            if (obj.SalesHeader.SalesId != 0)
            {

               await _ISalesService.Update(objSalesHeader, ObjSalesDetail, model2,CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Updated successfully.");
        }

        [HttpPut("SalesInpatientUpdate/{id:int}")]
        [Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> SaleUpdate(SalesUpdate obj)
        {
            TSalesInpatientHeader objSalesHeader = obj.SalesHeader.MapTo<TSalesInpatientHeader>();
            List<TSalesInpatientDetail> ObjSalesDetail = obj.SalesDetails.MapTo<List<TSalesInpatientDetail>>();
            List<TCurrentStock> model2 = obj.CurrentStockUpdate.MapTo<List<TCurrentStock>>();

            if (obj.SalesHeader.SalesId != 0)
            {

                await _ISalesService.SalesUpdate(objSalesHeader, ObjSalesDetail, model2,CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Updated successfully.");
        }


    }

}
