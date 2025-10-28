namespace HIMS.Data.DTO.Administration
{
    public class GetLabInformationListDto
    {
        public string? PathDate { get; set; }
        public string? ServiceName { get; set; }
        public long PathReportID { get; set; }
        public string? PBillNo { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsPrinted { get; set; }
        public string? PatientType { get; set; }

    }

}
