using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MVehicleMaster
    {
        public long VehicleId { get; set; }
        public string VehicleName { get; set; } = null!;
        public string VehicleNo { get; set; } = null!;
        public string? VehicleModel { get; set; }
        public DateTime? ManuDate { get; set; }
        public string? VehicleType { get; set; }
        public string? Note { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
