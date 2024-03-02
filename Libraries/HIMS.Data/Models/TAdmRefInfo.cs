using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TAdmRefInfo
    {
        public long AdmVisitId { get; set; }
        public long? AdmId { get; set; }
        public DateTime? RefVdate { get; set; }
        public DateTime? RefVtime { get; set; }
        public byte? IsEmg { get; set; }
        public long? DoctorId { get; set; }
        public long? IsAddedBy { get; set; }
        public long? IsUpdatedBy { get; set; }
    }
}
