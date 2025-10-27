namespace HIMS.Data.DTO.IPPatient
{
    public class LabRequestDetailsListDto
    {
        public long RequestId { get; set; }
        public long ReqDetId { get; set; }
        public string? ServiceName { get; set; }
        public long ServiceId { get; set; }
        public long OPIPID { get; set; }
        public long OPIPType { get; set; }
        public bool IsStatus { get; set; }
        public string? ReqDate { get; set; }
        public string? ReqTime { get; set; }
        public string? AddedByName { get; set; }
        public string? BillingUser { get; set; }
        public string? AddedByDate { get; set; }
        public long IsPathology { get; set; }
        public long IsRadiology { get; set; }
        public long CharId { get; set; }
        public long IsTestCompleted { get; set; }
        public long PathReportID { get; set; }
        public long IsTemplateTest { get; set; }
        public string? PBillNo { get; set; }
        public long ChargeId { get; set; }
        public bool IsTestCompted { get; set; }


    }
}
