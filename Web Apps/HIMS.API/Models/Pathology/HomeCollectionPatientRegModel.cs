using HIMS.API.Models.Masters;
using HIMS.Data.Models;

namespace HIMS.API.Models.Pathology
{
    public class HomeCollectionPatientRegModel
    {
        public long PatientRegId { get; set; }
        public long? PrefixId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public long? GenderId { get; set; }
        public DateTime? DateofBirth { get; set; }
        public long? AgeY { get; set; }
        public long? AgeM { get; set; }
        public long? AgeD { get; set; }
        public string? MobileNo { get; set; }
        public string? Address { get; set; }
        public long? PatRegId { get; set; }
        public long? CityId { get; set; }


    }
    public class HomeCollectionPatientRegistrationModel
    {
        public long PatientRegId { get; set; }
        public long? PrefixId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public long? GenderId { get; set; }
        public DateTime? DateofBirth { get; set; }
        public long? AgeY { get; set; }
        public long? AgeM { get; set; }
        public long? AgeD { get; set; }
        public string? MobileNo { get; set; }
        public string? Address { get; set; }
        public long? PatRegId { get; set; }
        public List<HomeCollectionPatientRegDetailModel> THomeCollectionPatientRegDetails { get; set; }

    }
    public  class HomeCollectionPatientRegDetailModel
    {
        public long PatientRegDetId { get; set; }
        public long? PatientRegId { get; set; }
        public bool? Priority { get; set; }
        public string? Status { get; set; }
        public string? Remark { get; set; }
        public long? PatientType { get; set; }

    }
}
