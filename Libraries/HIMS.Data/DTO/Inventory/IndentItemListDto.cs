namespace HIMS.Data.DTO.Inventory
{
    public class IndentItemListDto
    {

        public long? IndentId { get; set; }
        public long? IndentDetailsId { get; set; }
        public long? ItemId { get; set; }


        public string? ItemName { get; set; }
        public double? Qty { get; set; }
        public long? IndQty { get; set; }
        public long? IssQty { get; set; }

        public string? Comments { get; set; }

        public double? Bal { get; set; }

        public double? BalanceQty { get; set; }
        public long? FromStoreId { get; set; }

        public long? ToStoreId { get; set; }

        public string? FromStoreName { get; set; }

        public string? ToStoreName { get; set; }



    }
}
