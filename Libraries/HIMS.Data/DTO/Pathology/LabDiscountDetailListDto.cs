using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Pathology
{
    public class LabDiscountDetailListDto
    {
        public long BillNo { get; set; }
        public string BillDate { get; set; }
        public string BillTime { get; set; }
        public string ServiceName { get; set; }
        public double ConcessionPercentage { get; set; }
        public decimal ConcessionAmount { get; set; }
        public string DiscComments { get; set; }
        public string Username { get; set; }

    }

    public class LabPaymentDetailListDto
    {
        public DateTime PaymentDate { get; set; }
        public string PaymentTime { get; set; }
        public string? PayMode { get; set; }
        public long TransactionType { get; set; }
        public decimal PayAmount { get; set; }
        public string? Comments { get; set; }
        public string? UserName { get; set; }
        public long PaymentId { get; set; }

    }
    
    public class LabCreditDetailDto
    {
        public DateTime BillDate { get; set; }
        public DateTime BillTime { get; set; }
        public decimal TotalAmt { get; set; }
        public decimal PaidAmt { get; set; }
        public decimal BalanceAmt { get; set; }
        public string? UserName { get; set; }
        public long BillNo { get; set; }

    }


}
