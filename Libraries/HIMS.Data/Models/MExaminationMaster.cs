using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MExaminationMaster
    {
        public long ExaminationId { get; set; }
        public string? ExaminationDescr { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
