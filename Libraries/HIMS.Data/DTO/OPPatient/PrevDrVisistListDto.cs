using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OPPatient
{
    public class PrevDrVisistListDto
    {
        public string? DepartmentName { get; set; }
        public string? DoctorName { get; set; }
        public int? RegID { get; set; }
        public int? DepartmentId { get; set; }
        public int? ConsultantDocId { get; set; }
        public DateTime VisitDate { get; set; }
    }
}
