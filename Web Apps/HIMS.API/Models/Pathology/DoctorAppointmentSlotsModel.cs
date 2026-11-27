namespace HIMS.API.Models.Pathology
{
    public class DoctorAppointmentSlotsModel
    {
        public int DoctorId { get; set; }
        public int UnitId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int SlotDuration { get; set; } 
        public string SessionType { get; set; }
    }

    public class DoctorAppointmentSlotsCancelModel
    {
        public long SlotId { get; set; }
    }
}
