using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Administration
{
    public class TallyService : ITallyService
    {
        private readonly HIMSDbContext _context;
        public TallyService(HIMSDbContext context)
        {
            _context = context;
        }

        public virtual async Task<IPagedList<TallyListDto>> OPBillCashCounterListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<TallyListDto>(model, "PS_Tally_OPBillList_CashCounter");
        }

        public virtual async Task<IPagedList<OPRefundBillListCashCounterDto>> OPRefundBillListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<OPRefundBillListCashCounterDto>(model, "PS_Tally_OPRefundBillList_CashCounter");
        }

        public virtual async Task<IPagedList<IPAdvRefundPatientWisePaymentDto>> IPAdvRefundPatientWisePaymentlistAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<IPAdvRefundPatientWisePaymentDto>(model, "PS_tally_IPAdvRefund_PatientWise_Payment");
        }

        public virtual async Task<IPagedList<IPBillListPatientWisePaymentDto>> IPBillListPatientWisePaymentListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<IPBillListPatientWisePaymentDto>(model, "PS_tally_IP_BillList_PatientWise_Payment");
        }

        public virtual async Task<IPagedList<IPAdvPatientWisePaymentDto>> IPAdvPatientWisePaymentListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<IPAdvPatientWisePaymentDto>(model, "ps_tally_IPAdv_PatientWise_Payment");
        }

        public virtual async Task<IPagedList<IPBillListPatientWiseDto>> IPBillListPatientWiseListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<IPBillListPatientWiseDto>(model, "ps_tally_IP_BillList_PatientWise");
        }

        public virtual async Task<IPagedList<IPBillListCashCounterDto>> IPBillCashCounterListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<IPBillListCashCounterDto>(model, "PS_tally_IP_BillList_CashCounter");
        }

        public virtual async Task<IPagedList<IPBillRefundBillListPatientWisePaymentDto>> IPBillRefundBillPatientWisePaymentListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<IPBillRefundBillListPatientWisePaymentDto>(model, "ps_tally_IPBillRefund_BillList_PatientWise_Payment");
        }

        public virtual async Task<IPagedList<PurchaseWiseSupplierDto>> PurchaseWiseSupplierListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PurchaseWiseSupplierDto>(model, "ps_Tally_PurchaseWiseSupplier");
        }

        public virtual async Task<IPagedList<TallyPhar2SalesDto>> TallyPhar2SalesListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<TallyPhar2SalesDto>(model, "ps_Tally_Phar2_Sales");
        }

        public virtual async Task<IPagedList<TallyPhar2PaymentDto>> TallyPhar2PaymentAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<TallyPhar2PaymentDto>(model, "ps_Tally_Phar2_Payment");
        }

        public virtual async Task<IPagedList<TallyPhar2SalesReturnDto>> TallyPhar2SalesReturnListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<TallyPhar2SalesReturnDto>(model, "ps_Tally_Phar2_SalesReturn");
        }
        public virtual async Task<IPagedList<TallyPhar2ReceiptDto>> TallyPhar2ReceiptListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<TallyPhar2ReceiptDto>(model, "ps_Tally_Phar2_Receipt");
        }

        public virtual async Task<IPagedList<TallyIPBillListMediforteDto>> TallyIPBillListMediforteAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<TallyIPBillListMediforteDto>(model, "ps_Tally_IPBillList_Mediforte");
        }
        public virtual async Task<IPagedList<TallyIPBillDetailListMediforteDto>> TallyIPBillDetailListMediforteAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<TallyIPBillDetailListMediforteDto>(model, "ps_Tally_IPBillDetailList_Mediforte");
        }

        public virtual async Task<IPagedList<TallyOPBillListMediforteDto>> TallyOPBillListMediforteAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<TallyOPBillListMediforteDto>(model, "ps_Tally_OPBillList_Mediforte");
        }
        public virtual async Task<IPagedList<TallyOPBillDetailListMediforteDto>> TallyOPBillDetailListMediforteAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<TallyOPBillDetailListMediforteDto>(model, "ps_Tally_OPBillDetailList_Mediforte");
        }
        public virtual async Task<IPagedList<TallyIPBillRefundListMediforteDto>> TallyIPBillRefundListMediforteAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<TallyIPBillRefundListMediforteDto>(model, "Tally_IPBillRefund_Payment_Mediforte");
        }
        public virtual async Task<IPagedList<TallyIPAdvancePaymentListMediforteDto>> TallyIPAdvancePaymentListMediforteAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<TallyIPAdvancePaymentListMediforteDto>(model, "Tally_IPAdvance_Payment_Mediforte");
        }
        public virtual async Task<IPagedList<TallyIPAdvanceRefundPaymentListMediforteDto>> TallyIPAdvanceRefundPaymentListMediforteAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<TallyIPAdvanceRefundPaymentListMediforteDto>(model, "Tally_IPAdvanceRefund_Payment_Mediforte");
        }
        public virtual async Task<IPagedList<TallyIPBillPaymentListMediforteDto>> TallyIPBillPaymentListMediforteAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<TallyIPBillPaymentListMediforteDto>(model, "Tally_IPBill_Payment_Mediforte");
        }


        public virtual async Task<IPagedList<TallyOPIPSalesDetailListMediforteDto>> TallyOPIPSalesDetailListMediforteAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<TallyOPIPSalesDetailListMediforteDto>(model, "Tally_OP_IP_Sales_DetailList_Mediforte");
        }

        public virtual async Task<IPagedList<TallyPharmacyOPIPSalesReturnDetailListMediforteDto>> TallyPharmacyOPIPSalesReturnDetailListMediforteAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<TallyPharmacyOPIPSalesReturnDetailListMediforteDto>(model, "ps_Tally_OPIP_Sales_ReturnBillDetailList_Mediforte");
        }

        public virtual async Task<IPagedList<TallyPharmacyOPIPSalesPaymentListMediforteDto>> TallyPharmacyOPIPSalesPaymentListMediforteAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<TallyPharmacyOPIPSalesPaymentListMediforteDto>(model, "ps_Tally_OPIPSalsePayment_Mediforte");
        }



    }
}
