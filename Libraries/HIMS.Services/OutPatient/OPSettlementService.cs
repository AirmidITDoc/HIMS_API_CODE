using Aspose.Cells.Drawing;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OutPatient
{
    public class OPSettlementService : IOPSettlementService
    {
        private readonly HIMSDbContext _context;
        public OPSettlementService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<OPBillListSettlementListDto>> OPBillListSettlementList(GridRequestModel objGrid)
        {
            return await DatabaseHelper.GetGridDataBySp<OPBillListSettlementListDto>(objGrid, "m_Rtrv_OP_Bill_List_Settlement");
        }

        public virtual async Task InsertAsyncSP(Payment objpayment, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();

            string[] rpayEntity = { "ReceiptNo", "CashCounterId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount", "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode", "Tdsamount", "BillNoNavigation" };
            var payentity = objpayment.ToDictionary();
            foreach (var rProperty in rpayEntity)
            {
                payentity.Remove(rProperty);
            }
            string PaymentId = odal.ExecuteNonQuery("m_insert_Payment_OPIP_1", CommandType.StoredProcedure, "PaymentId", payentity);
            objpayment.PaymentId = Convert.ToInt32(PaymentId);
        }

    }
}
