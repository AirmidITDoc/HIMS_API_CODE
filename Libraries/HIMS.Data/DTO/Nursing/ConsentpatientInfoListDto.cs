using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Nursing
{
    public class ConsentpatientInfoListDto
    {
        public long ConsentId { get; set; }
        public DateTime? ConsentDate { get; set; }
        public DateTime? ConsentTime { get; set; }
        public long? ConsentTempId { get; set; }
        public string? ConsentName { get; set; }
        public string? ConsentText { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public long? OPIPType { get; set; }
        public string? PatientName { get; set; }
        public string? RegNo { get; set; }
        public string? AgeYear { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public string? MobileNo { get; set; }
        public string? DepartmentName { get; set; }
        public long? ConsentDeptId { get; set; }
        public string? AddedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public string? OPDNo { get; set; }
        public string? TariffName { get; set; }
        public string? CompanyName { get; set; }
        public string? DoctorName { get; set; }
        public long? OPIPID { get; set; }


    }
}
