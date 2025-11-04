using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TLabTestRequest
    {
        public long LabTestRequestId { get; set; }
        public long? LabPatientId { get; set; }
        public long? LabServiceId { get; set; }
        public decimal? Price { get; set; }
        public long? Qty { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
