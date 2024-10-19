using HIMS.Data.Models;

namespace HIMS.Data.DTO.OPPatient
{
    public class VisitDetailListDto
    {
        public long VisitId { get; set; }
        public long RegId { get; set; }
        public string PatientName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime VisitDate { get; set; }
        public string DVisitDate { get; set; }
    }
}
