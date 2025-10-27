namespace HIMS.Data.DTO.Inventory
{
    public class IndentListbyIdDto
    {
        public long IndentId { get; set; }
        public string? IndentNo { get; set; }
        public string? IndentDate { get; set; }
        public string? IndentTime { get; set; }
        public long? FromStoreId { get; set; }
        public long? ToStoreId { get; set; }
        public string? Addedby { get; set; }
        public string? Comments { get; set; }
        public bool? Isdeleted { get; set; }
        public bool? Isverify { get; set; }
        public bool? Isclosed { get; set; }
        public string? DIndentDate { get; set; }
        public string? DIndentTime { get; set; }
        public long? IsInchargeVerifyId { get; set; }
        public bool? Priority { get; set; }

        public string? ToStoreName { get; set; }
        public string? FromStoreName { get; set; }
    }
}
