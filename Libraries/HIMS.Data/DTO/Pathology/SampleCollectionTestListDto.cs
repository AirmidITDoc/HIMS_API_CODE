namespace HIMS.Data.DTO.Pathology
{
    public class SampleCollectionTestListDto
    {
        public string OP_IP_No { get; set; }
        public string VADate { get; set; }
        public string VATime { get; set; }
        public long Visit_Adm_ID { get; set; }
        public long PathTestID { get; set; }
        public string ServiceName { get; set; }
        public long PathReportID { get; set; }
        public long ServiceId { get; set; }
        public string DOA { get; set; }
        public string DOT { get; set; }
        public string IsCompleted { get; set; }
        public string IsPrinted { get; set; }
        public long OPD_IPD_ID { get; set; }
        public bool OPD_IPD_Type { get; set; }
        public string? PatientType { get; set; }
        public string? SampleNo { get; set; }
        public string? SampleCollectionTime { get; set; }
        public string? IsSampleCollection { get; set; }
        public long AdmissionID { get; set; }
        public long IsApprovedByCamp { get; set; }
        public long PatientTypeId { get; set; }

    }
}
