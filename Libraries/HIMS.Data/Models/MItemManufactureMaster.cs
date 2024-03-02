using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MItemManufactureMaster
    {
        public long ItemManufactureId { get; set; }
        public string? ManufactureName { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public string? ManufactureCode { get; set; }
    }
}
