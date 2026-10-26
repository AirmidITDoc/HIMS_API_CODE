using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class DischargeSummaryProvisionalDiagnosisListDto
    {
        public long? OPIPID { get; set; }
        public long? AdmissionId { get; set; }
        public string? Provisional { get; set; }
    }
}
