using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OTManagement
{
    public  class InOperationAttendingDetailsListDto
    {
        public long? OtinOperationId { get; set; }
        public long? DoctorTypeId { get; set; }
        public long? DoctorId { get; set; }
        public int? SeqNo { get; set; }
        public string? DoctorType { get; set; }
        public string? DoctorName { get; set; }
       
    }
    public class InOperationSurgeryDetailsDto
    {
        public long? OtinOperationId { get; set; }
        public long? SurgeryCategoryId { get; set; }
        public long? SurgeryId { get; set; }
        public string? SurgeryPart { get; set; }
        public string? SurgeryCategoryName { get; set; }
        public string? SurgeryName { get; set; }
        public DateTime? SurgeryFromTime { get; set; }
        public DateTime? SurgeryEndTime { get; set; }
        public double? SurgeryDuration { get; set; }
        public string? IsPrimary { get; set; }
        public long? SurgeonId { get; set; }
        public long? AnesthetistId { get; set; }
        public string? SurgeonName { get; set; }
        public string? AnestheticsName { get; set; }
      
    }
}
