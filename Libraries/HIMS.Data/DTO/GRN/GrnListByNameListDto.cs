
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.GRN
{
    public class GrnListByNameListDto
    {
        public long GRNReturnId { get; set; }
        public string? GRNReturnNo { get; set; }
        public string? GRNReturnDate { get; set; }
        public string? GRNReturnTime { get; set; }
        public long? StoreId { get; set; }
        public string? StoreName { get; set; }
        public long? SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public bool? Cash_Credit { get; set; }
        public decimal? TotalVatAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public string? Remark { get; set; }
        public bool? IsVerified { get; set; }
        public bool? IsClosed { get; set; }
        public long? AddedBy { get; set; }
        public string? UserName { get; set; }
        public string? GrnType { get; set; }
        public bool? IsGrnTypeFlag { get; set; }
        public decimal? TotalAmount { get; set; }
        public long Grnid { get; set; }

    }
}
