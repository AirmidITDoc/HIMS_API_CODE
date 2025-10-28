namespace HIMS.Data.DTO.Inventory
{
    public class IssuetodeptListDto
    {
        public long? ToStoreId { get; set; }
        public long? StoreId { get; set; }
        public string? FromStoreName { get; set; }
        public long? IssueId { get; set; }
        public string? IssueNo { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? IssueTime { get; set; }
        public double? TotalAmount { get; set; }
        public double? TotalVatAmount { get; set; }
        public double? NetAmount { get; set; }
        public string? Remark { get; set; }
        public string? Receivedby { get; set; }
        public long? Addedby { get; set; }
        public bool? IsVerified { get; set; }
        public bool? IsClosed { get; set; }
        public string? ToStoreName { get; set; }
        public string? IDate { get; set; }
        public long? IndentId { get; set; }
        public bool? IsAccepted { get; set; }

        public String? UserName { get; set; }
    }
}
