using HIMS.API.Models.OPPatient;

namespace HIMS.API.Models.Pathology
{
    public class LabPatientRegistrationMasterModels
    {

        public long LabPatRegId { get; set; }
        public DateTime? RegDate { get; set; }
        public string? RegTime { get; set; }
        public long? UnitId { get; set; }
        public string? LabRequestNo { get; set; }
        public long? PrefixId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public long? GenderId { get; set; }
        public string? MobileNo { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? AgeYear { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public string? Address { get; set; }
        public long? AdharCardNo { get; set; }
        public long? CityId { get; set; }
        public long? StateId { get; set; }
        public long? CountryId { get; set; }
        public int? ModifiedBy { get; set; }



    }
    public class LabPatientRegModel
    {
        public long LabPatientId { get; set; }
        public long? UnitId { get; set; }
        public long? PatientTypeId { get; set; }
        public long? TariffId { get; set; }
        public long? DoctorId { get; set; }
        public long? RefDocId { get; set; }
        public long? CompanyId { get; set; }
        public long? PatientType { get; set; }
        public string? Comments { get; set; }
        public string? ReferByName { get; set; }
        public int? ModifiedBy { get; set; }

    }
    public class PatientRegistrationMasterModels
    {

        public long LabPatRegId { get; set; }
        public DateTime? RegDate { get; set; }
        public string? RegTime { get; set; }
        public long? UnitId { get; set; }
        public long? PrefixId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public long? GenderId { get; set; }
        public string? MobileNo { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? AgeYear { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public string? Address { get; set; }
        public long? AdharCardNo { get; set; }
        public long? CityId { get; set; }
        public long? StateId { get; set; }
        public long? CountryId { get; set; }
        public int? ModifiedBy { get; set; }



    }
    public class LabPatientRegnewModel
    {
        public PatientRegistrationMasterModels LabPatientRegistrationMaster { get; set; }
        public LabPatientRegModel LabPatientRegModel { get; set; }
    }
}