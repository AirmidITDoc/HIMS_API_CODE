using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public  class OPBillListForPaymentModeChangeListDto
    {
        public long PaymentId { get; set; }
        public long CitUnitIdyId { get; set; }
        public string BillNo { get; set; }
        public string ReceiptNo { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? PayAmount { get; set; }
        public string? TranNo { get; set; }
        public string BankName { get; set; }
        public string PayMode { get; set; }
        public long? TransactionType { get; set; }
        public string? TransactionLabel { get; set; }
        public bool? IsCancelled { get; set; }
        public long? CreatedBy { get; set; }
        public string PBillNo { get; set; }
        public string BillDate { get; set; }
        public string RegNo { get; set; }
        public string PatientName { get; set; }
        public string PatieNetPayableAmtntName { get; set; }
        public string PayUserName { get; set; }







    }
}
