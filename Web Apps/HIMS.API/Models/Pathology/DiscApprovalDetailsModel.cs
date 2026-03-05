namespace HIMS.API.Models.Pathology
{
    public class DiscApprovalDetailsModel
    {
        public long DiscSeqId { get; set; }
        public long Opipid { get; set; }
        public byte Opiptype { get; set; }
        //public string? DiscApprovalNo { get; set; }
        public long BillNo { get; set; }
        public decimal RequestAmount { get; set; }
        public decimal ApprovedAmount { get; set; }
        public long? AppovedBy { get; set; }
        public DateTime? ApprovedDateTime { get; set; }
        public string? Comments { get; set; }
        public bool? IsActive { get; set; }
    }
    public  class MConstantModel
    {
        public long ConstantId { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
        public string? ConstantType { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
