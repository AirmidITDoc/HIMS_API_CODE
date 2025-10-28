namespace HIMS.Data.DTO.IPPatient
{
    public class CanteenRequestHeaderListDto
    {
        public string PatientName { get; set; }
        public string RegNo { get; set; }
        public DateTime AdmissionTime { get; set; }
        public string CompanyName { get; set; }
        public long OP_IP_ID { get; set; }
        public long OPD_IPD_Type { get; set; }
        public long ReqId { get; set; }
        public string AddedUserName { get; set; }
        public string Vst_Adm_Date { get; set; }
        public string Date { get; set; }
        public bool IsBillGenerated { get; set; }
        public string WardName { get; set; }
        public string BedName { get; set; }
        public string IPDNo { get; set; }
        public string PatientType { get; set; }
        public bool? IsCancelled { get; set; }
        public long ReqDetId { get; set; }
        public long CompanyId { get; set; }



    }
}
