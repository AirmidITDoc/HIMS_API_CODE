using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class IpdDrugSchedule
    {
        public long IpdDrugScheduleId { get; set; }
        public long AdmissionId { get; set; }
        public long DrugId { get; set; }
        public int DoseNo { get; set; }
        public DateTime DoseTime { get; set; }
        public int Status { get; set; }
        public bool IsActive { get; set; }
        public string? Comment { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Admission Admission { get; set; } = null!;
    }
}
