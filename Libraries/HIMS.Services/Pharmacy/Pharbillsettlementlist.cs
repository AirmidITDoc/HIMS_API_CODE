using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pharmacy
{
    public  class Pharbillsettlementlist
    {
        public long SalesId { get; set; }
        public long OP_IP_ID { get; set; }
        public string Date { get; set; }
        public String SalesNo { get; set; }
        public String RegNo { get; set; }
        public decimal TotalAmount { get; set; }

        public decimal DiscAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public long OP_IP_Type { get; set; }
        public long RegId { get; set; }
        public decimal PreDiscAmt { get; set; }
        public decimal PaidAmountPayment { get; set; }
        public long TransactionType { get; set; }
        //public long RefundId { get; set; }

       
        public String PatientName { get; set; }
        public long StoreId { get; set; }
        public String PatientType { get; set; }
        public String CpmpanyName { get; set; }
    }
}
