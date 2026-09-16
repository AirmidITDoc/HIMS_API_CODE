using System.Text.Json.Serialization;

namespace HIMS.API.Models.AbhaIntegration
{
    public class InitiateClientModel
    {
        public int clientId { get; set; }
        public int hospitalId { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string requestBy { get; set; }
        public string requestType { get; set; }
        public string callbackUrl { get; set; }
    }

    public class AbhaCallbackModel
    {
        public string? PatientId { get; set; }
        public string PatientName { get; set; }
        public string SbxId { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public string Dob { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string CreatedDate { get; set; }
        public int ClientId { get; set; }
        public string AbhaNumber { get; set; }
        public string Json { get; set; }
        public string TransactionId { get; set; }
    }

    public class CareContextModel
    {
        public string abhaId { get; set; }
        public string abhaNumber { get; set; }
        public string patientReferenceNumber { get; set; }
        public string yearOfBirth { get; set; }
        public List<CareContext2> careContexts { get; set; }
        public string hipId { get; set; }
    }

    public class CareContext2
    {
        public string referenceNumber { get; set; }
        public string comment { get; set; }
    }

    public class PatientEncounterRequest
    {
        public PatientModel Patient { get; set; }
        public string VisitId { get; set; }
        public string HipId { get; set; }
        public List<string> Types { get; set; }
    }

    public class PatientModel
    {
        public string PatientRegistrationNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string HealthId { get; set; }
        public string HealthIdNumber { get; set; }
        public string DayOfBirth { get; set; }
        public string MonthOfBirth { get; set; }
        public string YearOfBirth { get; set; }
    }

}