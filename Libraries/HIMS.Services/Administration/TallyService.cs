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
    }
}
