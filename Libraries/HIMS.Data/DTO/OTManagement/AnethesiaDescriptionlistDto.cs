using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OTManagement
{
    public class AnethesiaDescriptionlistDto
    {
        public long OTAnesthesiaPreOPDiagnosisId { get; set; }
        public string? DescriptionName { get; set; }
        public string? DescriptionType { get; set; }

    }
}
