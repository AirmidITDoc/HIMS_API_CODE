using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class ServiceClassdetailListDto
    {
        public long ServiceDetailId { get; set; }
        public long ServiceId { get; set; }
        public long TariffId { get; set; }
        public long ClassId { get; set; }
        public decimal ClassRate { get; set; }
        public string ClassName { get; set; }
        public string TariffName { get; set; }

    }
}
