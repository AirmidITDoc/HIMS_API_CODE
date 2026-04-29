using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public class DoctprPaymentListDo
    {

        public long PaymentId { get; set; }

        public long UnitId { get; set; }

        public long? BillNo { get; set; }
        public byte? OPDIPDType { get; set; }
        public string? ReceiptNo { get; set; }
        public string? PaymentDate { get; set; }
      
        public decimal? PayAmount { get; set; }
        public string? TranNo { get; set; }
        public string? BankName { get; set; }
        public string? Comments { get; set; }


        public string OnlineTranNo { get; set; }

        //public string? OnlineTranResponse { get; set; }
        public string? TranMode { get; set; }

        public string? TransactionLabel { get; set; }

        public string PayMode { get; set; }

    }
}
