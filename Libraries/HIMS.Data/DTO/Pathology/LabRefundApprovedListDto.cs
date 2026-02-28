using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Pathology
{
    public class LabRefundApprovedListDto
    {
        public long RefundId { get; set; }
        public DateTime? RefundDate { get; set; }
        public DateTime? RefundTime { get; set; }
        public string? RefundNo { get; set; }
        public long BillNo { get; set; }

        public string? PbillNo { get; set; }
        public long? UnitId { get; set; }
        public long OpdIpdType { get; set; }
        public decimal? RefundAmount { get; set; }
        public string? Remark { get; set; }
        public bool IsApproval { get; set; }
        public long ApprovedBy { get; set; }
        public DateTime ApprovalDatetime { get; set; }
        public string? Comment { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? LabRequestNo { get; set; }

        public string? CompanyName { get; set; }
    }
}
