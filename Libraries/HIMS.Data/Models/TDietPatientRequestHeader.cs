using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TDietPatientRequestHeader
    {
        public TDietPatientRequestHeader()
        {
            TDietPatReqDetails = new HashSet<TDietPatReqDetail>();
        }

        public long DietReqId { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Time { get; set; }
        public long? UnitId { get; set; }
        public string? DietReqNo { get; set; }
        public long? DietMenuId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }

        public virtual ICollection<TDietPatReqDetail> TDietPatReqDetails { get; set; }
    }
}
