using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OPPatient
{
    public  class OPBillListSettlementListDto
    {
        public long BillNo { get; set; }
        public long OPDIPDID { get; set; }
        public long RegID { get; set; }
        public decimal TotalAmt { get; set; }
        public double ConcessionAmt { get; set; }
        public decimal NetPayableAmt { get; set; }
        public decimal BalanceAmt { get; set; }
        public DateTime BillDate { get; set; }
        public byte OPD_IPD_Type { get; set; }
        public string?PaidAmount { get; set; }
        public long IsCancelled { get; set; }
        public string? PBillNo { get; set; }
        public long TransactionType { get; set; }
        public long AdvanceId { get; set; }
        public long RefundId { get; set; }
        public long AddedBy { get; set; }
        public long CashCounterId { get; set; }
        public long PaymentBillNo { get; set; }
        public long CompanyId { get; set; }
        public decimal CashPay { get; set; }
        public decimal ChequePay { get; set; }
        public decimal CardPay { get; set; }
        public decimal NeftPay { get; set; }
        public decimal PayTMPay { get; set; }
        public decimal AdvUsdPay { get; set; }
        public string? CompanyName { get; set; }
        public string? PatientType { get; set; }
        public string? OPDNo { get; set; }
        public string? DepartmentName { get; set; }


    }
}
