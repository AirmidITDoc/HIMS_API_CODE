using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class CompanyTypeMaster
    {
        public long CompanyTypeId { get; set; }
        public string? TypeName { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
