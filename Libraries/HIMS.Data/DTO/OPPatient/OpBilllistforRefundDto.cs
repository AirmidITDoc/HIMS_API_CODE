using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OPPatient
{
    public class OpBilllistforRefundDto
    {

        public long BillNo { get; set; }
        public decimal? TotalAmt { get; set; }
        public double? ConcessionAmt { get; set; }
        public decimal? NetPayableAmt { get; set; }
        public string? PBillNo { get; set; }
        public decimal? RefundAmount { get; set; }
        public long OPD_IPD_Type { get; set; }
        public long OPD_IPD_ID { get; set; }
        public DateTime? BillDate { get; set; }
        public decimal? BalanceAmt { get; set; }
        public long RegId { get; set; }
        public long VisitId { get; set; }
        public string BilDate { get; set; }

    }
}
