using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public  class MIcdDiagnosisMasterDto
    {
        public int Icdid { get; set; }
        public string Icdversion { get; set; } = null!;
        public string Icdcode { get; set; } = null!;
        public string DiagnosisName { get; set; } = null!;
        public string? ShortName { get; set; }
    }
}
