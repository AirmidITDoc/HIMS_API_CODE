using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class IPBillList
    {
        public long BillNo { get; set; }
        public long OPD_IPD_ID { get; set; }
        public decimal TotalAmt { get; set; }
        public double ConcessionAmt { get; set; }
        public decimal NetPayableAmt { get; set; }
        public decimal BalanceAmt { get; set; }
        public DateTime BillDate { get; set; }
        public byte OPD_IPD_Type { get; set; }
        public string? PaidAmount { get; set; }
        public long IsCancelled { get; set; }
        public string? PBillNo { get; set; }
        public long TransactionType { get; set; }
        public double AdvanceAmount { get; set; }
        public double AdvanceUsedAmount { get; set; }
        public double AdvBalanceAmount { get; set; }
        public long CashCounterId { get; set; }
        public long AddedBy { get; set; }
        public string PaymentBillNo { get; set; }
        public DateTime BillTime { get; set; }
        public string RegNo { get; set; }
        public decimal CashPay { get; set; }
        public decimal ChequePay { get; set; }
        public decimal CardPay { get; set; }
        public decimal NeftPay { get; set; }
        public decimal PayTMPay { get; set; }
        public decimal AdvUsdPay { get; set; }
        public long? InterimOrFinal { get; set; }
        public string? CompanyName { get; set; }
        public string? PatientType { get; set; }
        public string? IPDNo { get; set; }
        public string? DepartmentName { get; set; }



    }

}
