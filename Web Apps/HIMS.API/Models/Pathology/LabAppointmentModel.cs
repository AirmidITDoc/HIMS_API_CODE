namespace HIMS.API.Models.Pathology
{
    public class LabAppointmentModel
    {
        public long LabAppId { get; set; }
        public long? UnitId { get; set; }
        public DateTime? AppDate { get; set; }
        public string? AppTime { get; set; }
        public long? PrefixId { get; set; }
        public long? GenderId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? MobileNo { get; set; }
        public long? CityId { get; set; }
        public long? StateId { get; set; }
        public long? CountryId { get; set; }
        public string? Address { get; set; }
        public long? DoctorId { get; set; }
        public long? CategoryId { get; set; }
        public DateTime? LabAppDate { get; set; }
        public string? LabAppTime { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public long? LabPatRegId { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
    }
    public class LabAppointmentUpdate
    {
        public long LabAppId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

    }
}
