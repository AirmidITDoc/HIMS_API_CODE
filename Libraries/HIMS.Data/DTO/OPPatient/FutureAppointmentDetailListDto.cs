namespace HIMS.Data.DTO.OPPatient
{
    public class FutureAppointmentDetailListDto
    {
        public long PhoneAppId { get; set; }
        public string AppDate { get; set; }
        public string AppTime { get; set; }
        public string SeqNo { get; set; }
        public string PatientName { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string PhAppDate { get; set; }
        public string PhAppTime { get; set; }
        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public long DoctorId { get; set; }
        public string DoctorName { get; set; }


    }
}
