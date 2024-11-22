using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public class SupplierListDto
    {
        public string? SupplierName { get; set; }
        public string? ContactPerson { get; set; }
        public string? Address { get; set; }
        public long? CityId { get; set; }
        public long? StateId { get; set; }
        public long? CountryId { get; set; }
        public string? CreditPeriod { get; set; }
        public string? Mobile { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public long? ModeOfPayment { get; set; }
        public long? TermOfPayment { get; set; }
        public long? TaxNature { get; set; }
        public long? CurrencyId { get; set; }
        public long? Octroi { get; set; }
        public long? Freight { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public string? CityName { get; set; }
        public string? StateName { get; set; }
        public string? CountryName { get; set; }
        public long? StoreId { get; set; }
        public string? GSTNo { get; set; }
        public string? PanNo { get; set; }
    }
}
