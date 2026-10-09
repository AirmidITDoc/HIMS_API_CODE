using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TVaccineInformation
    {
        public long VaccineId { get; set; }
        public string? VaccineCode { get; set; }
        public string? VaccineName { get; set; }
        public long? GenericId { get; set; }
        public string? VaccineType { get; set; }
        public string? DiseaseIndication { get; set; }
        public long? Manufacturer { get; set; }
        public string? BrandName { get; set; }
        public long? Route { get; set; }
        public string? Dose { get; set; }
        public long? AgeGroup { get; set; }
        public long? NoofDoses { get; set; }
        public long? DoseSchedule { get; set; }
        public long? MinimumAge { get; set; }
        public long? MaximumAge { get; set; }
        public bool? BoosterRequired { get; set; }
        public string? StorageTemperature { get; set; }
        public string? VialType { get; set; }
        public string? VialSize { get; set; }
        public bool? DiluentRequired { get; set; }
        public string? DiluentVolume { get; set; }
        public string? ShelfLife { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool? VaccineStatus { get; set; }
        public string? Remarks { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
