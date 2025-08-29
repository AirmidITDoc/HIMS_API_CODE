using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Nursing
{
    public class IPPathologyListDto
    {

        public string RegNo { get; set; }
        public string PatientName { get; set; }
        public string OPIPNo { get; set; }
        public DateTime VADate { get; set; }
        public DateTime VATime { get; set; }
        public string DoctorName { get; set; }
        public string VisitAdmID { get; set; }
        public long PathTestID { get; set; }
        public string ServiceName { get; set; }
        public long PathReportID { get; set; }
        public long? ServiceId { get; set; }
        public string DOA { get; set; }
        public string? DOT { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsPrinted { get; set; }
        public string? OPD_IPD_ID { get; set; }
        public string? OPD_IPD_Type { get; set; }
        public string? PatientType { get; set; }
        public string? PBillNo { get; set; }
        public string? AgeYear { get; set; }
        public string? GenderName { get; set; }
        public string? IsTemplateTest { get; set; }
        public string? CategoryName { get; set; }
        public long? ChargeId { get; set; }
        public long? AdmVisitdocId { get; set; }

    }
}
