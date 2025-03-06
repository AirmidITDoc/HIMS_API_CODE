using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public  class PathResultEntryListDto
    {
        public string? RegNo { get; set; }
        public string? PatientName { get; set; }
        public string? OP_IP_No { get; set; }
        public DateTime? VADate { get; set; }
        public string? VATime { get; set; }
        public string? DoctorName { get; set; }
        public long? Visit_Adm_ID { get; set; }
        public string? PathTestID { get; set; }
        public string? ServiceName { get; set; }
        public long PathReportId { get; set; }
        public long ServiceId { get; set; }
        public string? DOA { get; set; }
        public string? DOT { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsPrinted { get; set; }
        public long? OpdIpdType { get; set; }
        public long? OpdIpdId { get; set; }
        public bool? PatientType { get; set; }
        public string? PBillNo { get; set; }
        public long AgeYear { get; set; }
        public string? GenderName { get; set; }
        public string? IsTemplateTest { get; set; }
        public string? CategoryName { get; set; }
        public long ChargeId { get; set; }
        public long? Adm_Visit_docId { get; set; }
        public string? SampleNo { get; set; }
        public string? SampleCollectionTime { get; set; }
        public string? IsSampleCollection { get; set; }
        public bool? IsVerifySign { get; set; }
        public long? PathTestServiceId { get; set; }
        public long? GenderId { get; set; }
    }
}
