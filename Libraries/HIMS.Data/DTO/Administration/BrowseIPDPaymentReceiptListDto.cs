using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public class BrowseIPDPaymentReceiptListDto
    {



        public long PaymentId { get; set; }
        public long BillNo { get; set; }
        public string RegNo { get; set; }
        public string PatientName { get; set; }
        public string PrefixName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public decimal NetPayableAmt { get; set; }
        public decimal BalanceAmt { get; set; }
        public string Remark { get; set; }
        public DateTime PaymentDate { get; set; }
        public string HospitalName { get; set; }
        public string HospitalAddress { get; set; }
        public string City { get; set; }
        public string Pin { get; set; }
        public string Phone { get; set; }
        public bool IsCancelled { get; set; }
        public string IPDNo { get; set; }
        public string PBillNo { get; set; }
        public decimal CashPayAmount { get; set; }
        public decimal ChequePayAmount { get; set; }
        public decimal CardPayAmount { get; set; }
        public decimal AdvanceUsedAmount { get; set; }
        public long TransactionType { get; set; }
        public string ReceiptNo { get; set; }
        public long AdvanceId { get; set; }
        public long RefundId { get; set; }
        public string UserName { get; set; }
        public string PayDate { get; set; }
        public decimal PaidAmount { get; set; }

        public decimal TotalAmt { get; set; }

        public decimal NEFTPayAmount { get; set; }

        public decimal PayTMAmount { get; set; }


    }
}
