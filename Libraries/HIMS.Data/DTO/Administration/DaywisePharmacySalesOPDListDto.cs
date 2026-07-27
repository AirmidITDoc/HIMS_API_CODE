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
    public class IPSalesAndSalesReturnPaymentDateWiseDto
    {
        public DateTime Date { get; set; }
        public string? PaymentDate { get; set; }
        public string OPIPType { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public string RegNo { get; set; } = string.Empty;
        public string IPDNo { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public string SalesNo { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal NetAmount { get; set; }
    }
}
