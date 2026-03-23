using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Pathology
{
    public  class LabAppointmentListDto
    {
        
            public long LabAppId { get; set; }
            public DateTime? AppDate { get; set; }
            public DateTime? AppTime { get; set; }
            public long? PrefixId { get; set; }
            public long? GenderId { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }
            public DateTime? DateofBirth { get; set; }
            public string MobileNo { get; set; }
            public long? CityId { get; set; }
            public long? StateId { get; set; }
            public long? CountryId { get; set; }
            public string? Address { get; set; }
            public long? DoctorId { get; set; }
            public long? CategoryId { get; set; }
            public DateTime? LabAppDate { get; set; }
            public DateTime? LabAppTime { get; set; }
            public long? AddedBy { get; set; }
            public bool? IsCancelled { get; set; }
            public long? IsCancelledBy { get; set; }
            public long? LabPatRegId { get; set; }
            public DateTime? StartTime { get; set; }
            public DateTime? EndTime { get; set; }
            public bool? IsActive { get; set; }
            public int? CreatedBy { get; set; }
            public DateTime? CreatedDate { get; set; }
            public string? SeqNo { get; set; }
            public string? CategoryName { get; set; }
            public string PrefixName { get; set; }
            public bool AppointmentCompleted { get; set; }

    }
    public class LabAppDetListDto
    {
        public long? UnitId { get; set; }
        public long? TestId { get; set; }
        public decimal? Price { get; set; }
        public int? Qty { get; set; }
        public decimal? TotalAmount { get; set; }
        public double? DiscPer { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public long? LabAppId { get; set; }
        public string? ServiceName { get; set; }
        public bool? IsEditable { get; set; }
        public long? IsPathology { get; set; }
        public bool? IsPathOutSource { get; set; }
        public long? IsRadiology { get; set; }
        public bool? IsRadOutSource { get; set; }
        public long? IsPackage { get; set; }
    }
}



