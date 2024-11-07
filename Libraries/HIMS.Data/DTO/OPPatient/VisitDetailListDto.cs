using HIMS.Data.Models;

namespace HIMS.Data.DTO.OPPatient
{
    public class VisitDetailListDto
    {
        public long VisitId { get; set; }
        public long RegId { get; set; }
        public string PatientName { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Address { get; set; }
        //public string Address { get; set; }
        //public string Address { get; set; }

        public DateTime VisitDate { get; set; }
        public string DVisitDate { get; set; }
    }
}
