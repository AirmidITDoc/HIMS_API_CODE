using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Pathology
{
    public class RadiologyApproveListDto
    {

        public long ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public string? IsRadOutSource { get; set; }
        public long RadReportId { get; set; }
        public long opdipdtype { get; set; }
        public long? opdipdid { get; set; }
        public string? RadDate { get; set; }
        public string? RadTime { get; set; }
        public string? UserName { get; set; }
        public long? RadTestID { get; set; }
        public string? DoctorName { get; set; }
        public string? PatientName { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsPrinted { get; set; }
        public string? SuggestionNotes { get; set; }
        public bool? IsVerifySign { get; set; }
        public string? IsVerifyedDate { get; set; }
        public long? IsTemplateTest { get; set; }
        public string? CategoryName { get; set; }
        public long? ChargesId { get; set; }
        public long? BillNo { get; set; }
    }
}
