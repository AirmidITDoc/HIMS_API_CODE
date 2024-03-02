using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MManufactureMaster
    {
        public long ManufId { get; set; }
        public string? ManufName { get; set; }
        public string? ManufShortName { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
