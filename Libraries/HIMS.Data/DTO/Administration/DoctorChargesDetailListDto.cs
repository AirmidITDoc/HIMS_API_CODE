using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public  class DoctorChargesDetailListDto
    {
        public long ServiceId { get; set; }
        public long DocChargeId { get; set; }
        public long DoctorId { get; set; }
        public long TariffId { get; set; }
        public long ClassId { get; set; }
        public decimal? Price { get; set; }
        public int? Days { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string TariffName { get; set; }
        public string ClassName { get; set; }
        public string ServiceName { get; set; }

    }
}
