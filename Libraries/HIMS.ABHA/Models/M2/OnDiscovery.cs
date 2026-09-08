using System.Text.Json.Serialization;

namespace HIMS.ABHA.Models.M2
{
    public class Rootobject
    {
        public string transactionId { get; set; }
        public PatientOnDiscovery patient { get; set; }
    }

    public class PatientOnDiscovery
    {
        public string id { get; set; }
        public Verifiedidentifier[] verifiedIdentifiers { get; set; }
        public object unverifiedIdentifiers { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public int yearOfBirth { get; set; }
    }

    public class Verifiedidentifier
    {
        public string type { get; set; }
        public string value { get; set; }
    }

}
