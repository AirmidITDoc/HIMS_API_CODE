using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MMaritalStatusMaster
    {
        public long MaritalStatusId { get; set; }
        public string? MaritalStatusName { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
