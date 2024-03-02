using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MSupplierMaster
    {
        public long SupplierId { get; set; }
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
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public string? Gstno { get; set; }
        public string? PanNo { get; set; }
    }
}
