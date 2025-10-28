namespace HIMS.Data.DTO.Inventory
{
    public class MateralreceivedbyDeptLstDto
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
        public DateTime? AcceptedDatetime { get; set; }

        public string? TotalQtyIssued { get; set; }
        public string? AcceptedByDepartment { get; set; }
        public string? RejetcedByDepartment { get; set; }
        public string? PendingByDepartment { get; set; }
    }
}
