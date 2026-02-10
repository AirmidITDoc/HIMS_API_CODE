using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MPathTestMaster
    {
        public MPathTestMaster()
        {
            MPathTemplateDetail1s = new HashSet<MPathTemplateDetail1>();
            MPathTemplateDetails = new HashSet<MPathTemplateDetail>();
            MPathTestDetailMasters = new HashSet<MPathTestDetailMaster>();
        }

        public long TestId { get; set; }
        public string? TestName { get; set; }
        public string? PrintTestName { get; set; }
        public long? CategoryId { get; set; }
        public bool? IsSubTest { get; set; }
        public string? TechniqueName { get; set; }
        public string? MachineName { get; set; }
        public string? SuggestionNote { get; set; }
        public string? FootNote { get; set; }
        public bool? IsActive { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public long? ServiceId { get; set; }
        public long? IsTemplateTest { get; set; }
        public bool? IsCategoryPrint { get; set; }
        public bool? IsPrintTestName { get; set; }
        public DateTime? TestTime { get; set; }
        public DateTime? TestDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? SpecimenTypeId { get; set; }
        public long? SpecimenColor { get; set; }
        public long? SpecimenQty { get; set; }
        public long? SpecimenConditionId { get; set; }
        public long? ContainerTypeId { get; set; }
        public long? CollectionMethod { get; set; }
        public long? NoofContainer { get; set; }
        public long? PreservationUsed { get; set; }
        public string? BarcodeLabel { get; set; }
        public string? ConsentDetail { get; set; }
        public bool? IsConsentRequired { get; set; }
        public bool? IsFastingRequired { get; set; }
        public bool? IsApprovedRequired { get; set; }
        public string? TestInformationTemplate { get; set; }
        public long? Tatday { get; set; }
        public long? Tathour { get; set; }
        public long? Tatmin { get; set; }

        public virtual ICollection<MPathTemplateDetail1> MPathTemplateDetail1s { get; set; }
        public virtual ICollection<MPathTemplateDetail> MPathTemplateDetails { get; set; }
        public virtual ICollection<MPathTestDetailMaster> MPathTestDetailMasters { get; set; }
    }
}
