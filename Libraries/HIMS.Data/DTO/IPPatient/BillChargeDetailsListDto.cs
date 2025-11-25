using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class BillChargeDetailsListDto
    {

        public long? ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public double? Price { get; set; }
        public double? Qty { get; set; }
        public double? TotalAmt { get; set; }
        public double? ConcessionPercentage { get; set; }
        public decimal? ConcessionAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public long? DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public byte? OpdIpdType { get; set; }
        public long? OpdIpdId { get; set; }
        public long? IsPathology { get; set; }
        public long? IsRadiology { get; set; }
        public long? IsPackage { get; set; }
        public long? BillNo { get; set; }
        public bool? IsInclusionExclusion { get; set; }

    }
}
