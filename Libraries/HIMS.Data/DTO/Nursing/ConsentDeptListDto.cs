namespace HIMS.Data.DTO.Nursing
{
    public class ConsentDeptListDto
    {
        public long ConsentId { get; set; }
        public string ConsentName { get; set; }
        public string ConsentDesc { get; set; }
    }
    public class TransactionConsentMasterListDto
    {
        public string PatientName { get; set; }
        public string RegNo { get; set; }
        public string GenderName { get; set; }
        public DateTime AdmissionDate { get; set; }
        public string AdmissionTime { get; set; }
        public string IPDNo { get; set; }
        public long ConsentId { get; set; }
        public long RefId { get; set; }
        public string OPIPID { get; set; }
        public string OPIPType { get; set; }
        public string ConsentText { get; set; }
        public string UserName { get; set; }
        public string DoctorName { get; set; }
        public string DepartmentName { get; set; }
        public string AgeYear { get; set; }
        public string AgeDay { get; set; }
        public string AadharCardNo { get; set; }
        public string PanCardNo { get; set; }
        public string MobileNo { get; set; }
        public string TransactionLabel { get; set; }
        public DateTime ConsentDate { get; set; }
        public string ConsentTime { get; set; }
        public bool? IsActive { get; set; }
        public string? ConsentName { get; set; }


    }

}
