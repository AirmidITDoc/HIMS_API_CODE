using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public  class ApprovalListDto
    {
        public long ApprovalId { get; set; }
        public string? ApprovalNo { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Time { get; set; }
        public long? TranId { get; set; }
        public string? TransactionType { get; set; }
        public byte? ApprovalStatus { get; set; }
        public long? AuthorizeBy { get; set; }
        public DateTime? ApprovedDateTime { get; set; }
        public string? Comment { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
