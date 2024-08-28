using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MSupplierMaster
    {
        public MSupplierMaster()
        {
            MAssignSupplierToStores = new HashSet<MAssignSupplierToStore>();
        }

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
        public bool? IsActive { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public string? Gstno { get; set; }
        public string? PanNo { get; set; }
        public string? PinCode { get; set; }
        public long? Taluka { get; set; }
        public string? LicNo { get; set; }
        public DateTime? ExpDate { get; set; }
        public string? DlNo { get; set; }
        public long? BankId { get; set; }
        public string? Bankname { get; set; }
        public string? Branch { get; set; }
        public long? BankNo { get; set; }
        public string? Ifsccode { get; set; }
        public long? VenderTypeId { get; set; }
        public decimal? OpeningBalance { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? SupplierTime { get; set; }

        public virtual ICollection<MAssignSupplierToStore> MAssignSupplierToStores { get; set; }
    }
}
