using HIMS.Data.Models;
using System.ComponentModel.Design;
using System.Net;

namespace HIMS.Data.DTO.OPPatient
{
    public class VisitDetailListDto
    {

             public long VisitId { get; set; }
        public long RegId { get; set; }
        public string PatientName { get; set; }
        public long PrefixId { get; set; }
        public string AadharCardNo { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Address { get; set; }
        public long MaritalStatusId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime VisitDate { get; set; }
        public string DVisitDate { get; set; }
        public string VisitTime { get; set; }
        public long HospitalId { get; set; }
        public string HospitalName { get; set; }
        public long PatientTypeId { get; set; }
        public string PatientType { get; set; }
        public string VistDateTime { get; set; }
        public string OPDNo { get; set; }
        public long TariffId { get; set; }
        public string TariffName { get; set; }
        public long DepartmentId { get; set; }
        public long AppPurposeId { get; set; }
        public long CompanyId { get; set; }
        public string CompanyName { get; set; }



    }
}
