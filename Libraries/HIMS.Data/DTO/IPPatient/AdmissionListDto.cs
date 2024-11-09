using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class AdmissionListDto
    {
        public long AdmissionId { get; set; }
        public long? RegId { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public DateTime? AdmissionTime { get; set; }
        public long? PatientTypeId { get; set; }
        public long? DocNameId { get; set; }
        public string? Ipdno { get; set; }
        public byte? AdmissionType { get; set; }
    }
}
