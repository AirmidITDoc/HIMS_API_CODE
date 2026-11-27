using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Pathology
{
    public class DoctorAppointmentSlotsListDto
    {
        public long SlotId { get; set; }
        public int DoctorId { get; set; }
        public int UnitId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string SlotStatus { get; set; } = null!;
        public string? SessionType { get; set; }
        public long? AppointmentId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public string DoctorName { get; set; }
        public string? CreatedByName { get; set; }
        public string? ModifyByName { get; set; }
        public string? CancelledByName { get; set; }
    }
}
