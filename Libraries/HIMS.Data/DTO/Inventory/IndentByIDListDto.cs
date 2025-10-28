namespace HIMS.Data.DTO.Inventory
{
    public class IndentByIDListDto
    {

        public long? IndentNo { get; set; }
        public string? IndentDate { get; set; }

        public string? IndentTime { get; set; }

        public long? FromStoreId { get; set; }
        public String? FromStoreName { get; set; }
        public long? ToStoreId { get; set; }
        public String? ToStoreName { get; set; }

        public long? Addedby { get; set; }

        public bool? Isdeleted { get; set; }

        public bool? Isverify { get; set; }

        public string? DIndentDate { get; set; }

        public bool? IsInchargeVerify { get; set; }




    }
}
