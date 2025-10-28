namespace HIMS.Data.DTO.OPPatient
{
    public class OPRequestListDto
    {
        public long RequestTranId { get; set; }
        public long OP_IP_ID { get; set; }
        public long? ServiceId { get; set; }
        public string? ServiceName { get; set; }

    }
}
