using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public  class MIcdDiagnosisMasterDto
    {
        public string Icdcode { get; set; } = null!;
        public string DiagnosisName { get; set; } = null!;
        public string? ICDCodeWithDiagnosis { get; set; }
    }
}
