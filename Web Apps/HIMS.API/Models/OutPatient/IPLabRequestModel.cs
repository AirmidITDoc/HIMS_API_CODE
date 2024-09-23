namespace HIMS.API.Models.OutPatient
{
    public class IPLabRequestModel
    {
        public long RequestId { get; set; }
      
        public long? OpIpId { get; set; }
    
        public long? IsAddedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public DateTime? IsCancelledTime { get; set; }
        public byte? IsType { get; set; }
        public bool? IsOnFileTest { get; set; }
    }
}
