using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public  class OTBookingRequestListDto
    {
        public long OTRequestId { get; set; }
        public DateTime? OtbookingDate { get; set; }
        public DateTime? OtbookingTime { get; set; }
        public byte? OpIpType { get; set; }
        public long VisitId { get; set; }
        public string? OPDNo { get; set; }
        public string? SurgeryType { get; set; }
        public string? DepartmentName { get; set; }
        public string? SiteDescriptionName { get; set; }
        public string? SurgeryName { get; set; }
        public long? AddedBy { get; set; }
        public string? UserName { get; set; }
        public string? SurgeryCategoryName { get; set; }
        public string? PatientName { get; set; }
        public bool? DoctorName { get; set; }
        public long? TariffName { get; set; }
        public string? CompanyName { get; set; }
        public string? RegNo { get; set; }
        public string? AgeYear { get; set; }
        public string? MobileNo { get; set; }
        public long? CategoryId { get; set; }
        public long? DepartmentId { get; set; }
        public long? SiteDescId { get; set; }
        public long? SurgeonId { get; set; }
        public long? SurgeryId { get; set; }
        public long? OpIpId { get; set; }
        public bool? IsCancelled { get; set; }

    }
}
