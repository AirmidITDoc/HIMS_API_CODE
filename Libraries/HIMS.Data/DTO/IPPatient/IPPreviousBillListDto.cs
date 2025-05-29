using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class IPPreviousBillListDto
    {
        public long? BillNo { get; set; }
        public string? BillTime { get; set; }
        public decimal? TotalAmt { get; set; }
        public double? ConcessionAmt { get; set; }
        public decimal? NetPayableAmt { get; set; }
        public decimal? PaidAmt { get; set; }
        public decimal? BalanceAmt { get; set; }
        public string? BDate { get; set; }
        public long OPD_IPD_Type { get; set; }
        public string? PBillNo { get; set; }
        public decimal? CashPayAmount { get; set; }
        public decimal? ChequePayAmount { get; set; }
        public decimal? CardPayAmount { get; set; }
        public decimal? NEFTPayAmount { get; set; }
        public decimal? PayTMAmount { get; set; }
        public decimal? AdvanceUsedAmount { get; set; }
    }

}
