using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public class TallyListDto
    {
        public String BillDate { get; set; }
        public string CashCounterName { get; set; }

        public decimal NetPayableAmt { get; set; }
        public decimal CashPayAmount { get; set; }
        public decimal ChequePayAmount { get; set; }
        public decimal CardPayAmount { get; set; }
        public decimal AdvanceUsedAmount { get; set; }
        public decimal NEFTPayAmount { get; set; }
        public decimal PayTMAmount { get; set; }
    }

    public class OPRefundBillListCashCounterDto
    {
        public String BillDate { get; set; }
        public string CashCounterName { get; set; }

        public decimal NetPayableAmt { get; set; }
        public decimal CashPayAmount { get; set; }
        public decimal ChequePayAmount { get; set; }
        public decimal CardPayAmount { get; set; }
        public decimal AdvanceUsedAmount { get; set; }
        public decimal NEFTPayAmount { get; set; }
        public decimal PayTMAmount { get; set; }
    }

    public class IPAdvRefundPatientWisePaymentDto
    {
        public DateTime BillDate { get; set; }
        public decimal RefundAmount { get; set; }
        public decimal CashPayAmount { get; set; }
        public decimal ChequePayAmount { get; set; }
        public decimal CardPayAmount { get; set; }
        public decimal AdvanceUsedAmount { get; set; }
        public decimal NEFTPayAmount { get; set; }
        public decimal PayTMAmount { get; set; }
    }

    public class IPBillListPatientWisePaymentDto
    {
        public string PaymentDate { get; set; }

        public string PatientName { get; set; }

        public string RegNo { get; set; }

        public string IPDNo { get; set; }

        public string RefundNo { get; set; }

        public decimal CashPayAmount { get; set; }

        public decimal ChequePayAmount { get; set; }

        public decimal CardPayAmount { get; set; }

        public decimal NEFTPayAmount { get; set; }

        public decimal PayTMAmount { get; set; }

        public string Remark { get; set; }
    }

}
