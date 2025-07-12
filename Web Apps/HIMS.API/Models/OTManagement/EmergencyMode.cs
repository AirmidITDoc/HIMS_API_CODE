namespace HIMS.API.Models.OTManagement
{
    public class EmergencyMode
    {
        public long? RegId { get; set; }
        public DateTime? EmgDate { get; set; }
        public string? EmgTime { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? MobileNo { get; set; }
        public long? DepartmentId { get; set; }
        public long? DoctorId { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public long EmgId { get; set; }

      
    }
}
