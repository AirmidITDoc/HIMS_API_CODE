namespace HIMS.Data.DTO.Administration
{
    public class OPRequestListFromEMRDto
    {
        public long RequestTranId { get; set; }
        public long OPIPID { get; set; }
        public string GroupName { get; set; }
        public long ServiceId { get; set; }
        public string ServiceName { get; set; }
        public long CreatedBy { get; set; }
        public string UserName { get; set; }
        public decimal ClassRate { get; set; }
        public long? IsPathology { get; set; }
        public long? IsRadiology { get; set; }
        public long? IsPackage { get; set; }
        public string ClassName { get; set; }


    }
}
