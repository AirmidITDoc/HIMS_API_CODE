using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Nursing
{
    public class NursingPainAssessmentListDto
    {
        public long PainAssessmentId { get; set; }
        public DateTime? PainAssessmentDate { get; set; }
        public DateTime? PainAssessmentTime { get; set; }
        public long? AdmissionId { get; set; }
        public int? PainAssessementValue { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
