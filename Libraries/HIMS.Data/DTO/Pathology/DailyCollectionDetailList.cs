using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Pathology
{
    public class DailyCollectionDetailList
    {
        public string BillDate { get; set; }
        public string PrintBillNo { get; set; }
        public string LabRequestNo { get; set; }
        public string PatientName { get; set; }
        public string TestNames { get; set; }

        public decimal TotalAmt { get; set; }
        public decimal ReversalAmt { get; set; }
         public double ConcessionAmt { get; set; }
         public decimal DiscountAmtReversed { get; set; }

        public decimal GrossAmt { get; set; }
          public double GrossDiscount { get; set; }
        public double NetPayableAmt { get; set; }

        public decimal CashReceived { get; set; }
        public decimal UPI { get; set; }
        public decimal CardAmount { get; set; }
        public decimal TotalMoneyReceived { get; set; }

        public decimal CashPatientBalance { get; set; }
        public decimal CreditPatientBalance { get; set; }

         public decimal DueCollectionCash { get; set; }
        public decimal DueCollectionCard { get; set; }

        public decimal LessRefundAmt { get; set; }
        public decimal NetUpiCollection { get; set; }
        public decimal NetCashInHand { get; set; }
        public decimal NetCardCollection { get; set; }

        public string UserName { get; set; }
    }
}
