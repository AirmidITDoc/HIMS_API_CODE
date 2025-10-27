namespace HIMS.API.Models.Nursing
{
    public class NursingVitalsModel
    {
        public long VitalId { get; set; }
        public DateTime? VitalDate { get; set; }
        public string? VitalTime { get; set; }
        public long? AdmissionId { get; set; }
        public string? Temperature { get; set; }
        public string? Pulse { get; set; }
        public string? Respiration { get; set; }
        public string? BloodPresure { get; set; }
        public string? Cvp { get; set; }
        public string? Peep { get; set; }
        public string? ArterialBloodPressure { get; set; }
        public string? PapressureReading { get; set; }
        public string? Brady { get; set; }
        public string? Apnea { get; set; }
        public string? AbdominalGrith { get; set; }
        public string? Desaturation { get; set; }
        public string? SaturationWithO2 { get; set; }
        public string? SaturationWithoutO2 { get; set; }
        public string? Po2 { get; set; }
        public string? Fio2 { get; set; }
        public string? Pfration { get; set; }
        public int? SuctionType { get; set; }
    }
}