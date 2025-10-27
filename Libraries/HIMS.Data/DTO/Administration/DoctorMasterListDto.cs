namespace HIMS.Data.DTO.Administration
{
    public class DoctorMasterListDto
    {
        public long DoctorId { get; set; }
        public long? PrefixId { get; set; }
        public string? PrefixName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? DoctorName { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? Address { get; set; }
        public string CityId { get; set; }
        public string? CityName { get; set; }
        public string? Pin { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public long? GenderId { get; set; }
        public string? GenderName { get; set; }
        public string? Education { get; set; }
        public bool? IsConsultant { get; set; }
        public string? RegDate { get; set; }
        public string? MahRegDate { get; set; }
        public bool? IsRefDoc { get; set; }
        public bool? IsActive { get; set; }
        public string? DoctorType { get; set; }
        public long DoctorTypeId { get; set; }
        public bool? IsInHouseDoctor { get; set; }
        public bool? IsOnCallDoctor { get; set; }
        public string? PassportNo { get; set; }
        public string? Esino { get; set; }
        public string? REGNO { get; set; }
        public string? MAHREGNO { get; set; }
        public string? PANCARDNO { get; set; }
        public string? AadharCardNo { get; set; }
        public string? Signature { get; set; }
        public string? AgeYear { get; set; }
        public string? RefDocHospitalName { get; set; }

    }
}
