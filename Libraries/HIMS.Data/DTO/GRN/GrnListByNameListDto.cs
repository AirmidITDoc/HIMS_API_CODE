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
        public String? GRNReturnNo { get; set; }
        public string? GRNReturnDate { get; set; }
        public string? GRNReturnTime { get; set; }
        public long? StoreId { get; set; }
        public string? StoreName { get; set; }
        public long? SupplierId { get; set; }
        public String? SupplierName { get; set; }
        //public double? Cash_Credit { get; set; }
        //public double? TotalVatAmount { get; set; }
        //public double? NetAmount { get; set; }
        public String? Remark { get; set; }

        public bool? IsVerified { get; set; }
        public bool? IsClosed { get; set; }
        public long? AddedBy { get; set; }
        public String? UserName { get; set; }
    }
}
