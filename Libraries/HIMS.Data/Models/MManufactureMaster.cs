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
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
