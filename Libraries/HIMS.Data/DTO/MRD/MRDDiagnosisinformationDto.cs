using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace HIMS.Data.DTO.MRD
{
    public class MRDDiagnosisinformationDto
    {
        public long IpdiagDetId { get; set; }
        public long IpdiagId { get; set; }
        public long AdmId { get; set; }
        public string? Diagnosis { get; set; } 
        public string? Icdcode { get; set; } 
        public string? Diagnosisinformation { get; set; }
        public string? FlagCode { get; set; } 
        public int CreatedBy { get; set; }
        public string? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public string? ModifiedDate { get; set; }
    }
}