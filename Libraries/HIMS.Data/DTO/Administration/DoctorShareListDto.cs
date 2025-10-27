using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public class DoctorShareListDto
    {
        public string? PatientName { get; set; }
        public decimal? TotalAmt { get; set; }
        public double? ConcessionAmt { get; set; }
        public decimal? NetPayableAmt { get; set; }
        public string? PbillNo { get; set; }
        public string? AdmittedDoctorName { get; set; }
        public string? PatientType { get; set; }
        public string? CompanyName { get; set; }

        public long BillNo { get; set; }
        public bool? IsBillShrHold { get; set; }
        public byte? OpdIpdType { get; set; }
        public int? DoctorShareAmount { get; set; }
        public int? HospitalAmount { get; set; }
        public string? lbl { get; set; }
        public long? DoctorId { get; set; }

    }
}
