using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public  class DaywisePharmacySalesOPDListDto
    {
        public string? SaleDate { get; set; }
        public string? DateYYYYMMDD { get; set; }
        public string? Type { get; set; }
        public string? TranType { get; set; }
        public decimal? Taxable { get; set; }
        public float? Cgstper { get; set; }
        public decimal? Cgstamt { get; set; }
        public float? Sgstper { get; set; }
        public decimal? Sgstamt { get; set; }
        public decimal? LineTotal { get; set; }
        public decimal? TotalBillAmount { get; set; }
    }
}
