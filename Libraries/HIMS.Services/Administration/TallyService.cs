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
    }
}
