using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MAdmissionType
    {
        public long AdmissiontypeId { get; set; }
        public string? AdmissiontypeName { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
