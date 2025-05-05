using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pharmacy
{
    public class SalesBillListDto
    {
        public long SalesId { get; set; }
        public DateTime Date { get; set; }
        public string SalesNo { get; set; }
        public long RegNo { get; set; }
        public string PatientName { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal DiscAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal CashPayAmount { get; set; }
        public decimal ChequePayAmount { get; set; }
        public decimal CardPayAmount { get; set; }
        public long TransactionType { get; set; }
        public long OP_IP_Type { get; set; }
        public string PatientType { get; set; }
        public string PaidType { get; set; }
        public long IsPrescription { get; set; }
        public long CashCounterID { get; set; }
        public bool IsRefundFlag { get; set; }

        public long IPNO { get; set; }

        public bool IsPrint { get; set; }
    }
}
