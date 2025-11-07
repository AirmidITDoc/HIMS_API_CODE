using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class PatientTransport
    {
        public int AmbulanceTransId { get; set; }
        public long? BillNo { get; set; }
        public int? AdmissionId { get; set; }
        public string PatientName { get; set; } = null!;
        public int? VehicleId { get; set; }
        public string? VehicleNo { get; set; }
        public string? VehicleModel { get; set; }
        public string? DriverName { get; set; }
        public string? DriverContactNo { get; set; }
        public string? PatientAddress { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }
        public DateTime? PickupDate { get; set; }
        public string? PickupAddress { get; set; }
        public string? DropoffAddress { get; set; }
        public bool? IsFress { get; set; }
        public string? Note { get; set; }
    }
}
