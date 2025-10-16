using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public class DoctorPayListDto
    {
        public long TranId { get; set; }
        public DateTime? TranDate { get; set; }
        public DateTime? TranTime { get; set; }
        public long? PbillNo { get; set; }
        public long? CompanyId { get; set; }
        public DateTime? BillDate { get; set; }
        public string? ServiceName { get; set; }
        public decimal? Price { get; set; }
        public float? Qty { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? DocAmount { get; set; }
        public long? IsBillShrHold { get; set; }
        public long? DocId { get; set; }
        public string? PatientName { get; set; }
        public string? CompanyName { get; set; }
        public string? PatientType { get; set; }


    }
}
