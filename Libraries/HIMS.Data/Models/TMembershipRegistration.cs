using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TMembershipRegistration
    {
        public TMembershipRegistration()
        {
            TMembershipChildren = new HashSet<TMembershipChild>();
            TMembershipEmrgencies = new HashSet<TMembershipEmrgency>();
            TMembershipRelatives = new HashSet<TMembershipRelative>();
        }

        public long MembershipId { get; set; }
        public string MembershipNo { get; set; } = null!;
        public DateTime MembershipDate { get; set; }
        public DateTime MembershipTime { get; set; }
        public long HprefixId { get; set; }
        public long HgenderId { get; set; }
        public string HusbandFirstName { get; set; } = null!;
        public string? HusbandMiddleName { get; set; }
        public string? HusbandLastName { get; set; }
        public DateTime HusbandDob { get; set; }
        public long? HusbandAgeY { get; set; }
        public long? HusbandAgeM { get; set; }
        public long? HusbandAgeD { get; set; }
        public string? HusbandMobile { get; set; }
        public string? HusbandEmail { get; set; }
        public string? HusbandAadhaar { get; set; }
        public string? HusbandPan { get; set; }
        public long? HusbandBloodGroupId { get; set; }
        public string? HusbandEducation { get; set; }
        public long? HusbandOccupationId { get; set; }
        public string? HusbandHobbies { get; set; }
        public string? HusbandMedications { get; set; }
        public DateTime? HusbandFullBodyCheckupDate { get; set; }
        public string? HusbandPhoto { get; set; }
        public long WprefixId { get; set; }
        public long WgenderId { get; set; }
        public string WifeFirstName { get; set; } = null!;
        public string? WifeMiddleName { get; set; }
        public string? WifeLastName { get; set; }
        public string? WifeParentalDetails { get; set; }
        public DateTime WifeDob { get; set; }
        public long? WifeAgeY { get; set; }
        public long? WifeAgeM { get; set; }
        public long? WifeAgeD { get; set; }
        public string? WifeMobile { get; set; }
        public string? WifeEmail { get; set; }
        public long? WifeBloodGroupId { get; set; }
        public string? WifeAadhaar { get; set; }
        public string? WifePan { get; set; }
        public long? WifeOccupationId { get; set; }
        public string? WifeEducation { get; set; }
        public string? WifeHobbies { get; set; }
        public string? WifeParentsNativePlace { get; set; }
        public string? WifeMedications { get; set; }
        public DateTime? WifeFullBodyCheckupDate { get; set; }
        public string? WifePhoto { get; set; }
        public long? CityId { get; set; }
        public string? CityName { get; set; }
        public string? ResidenceAddress { get; set; }
        public bool? ResidenceType { get; set; }
        public string? NativePlace { get; set; }
        public bool? AyushmanEnrolled { get; set; }
        public bool? MaleFemaleEnrolled { get; set; }
        public string? AyushmanSpouseDetails { get; set; }
        public bool? HasMediclaim { get; set; }
        public string? MediclaimCompany { get; set; }
        public string? MediclaimPolicyNumber { get; set; }
        public decimal? MediclaimIssuanceAmt { get; set; }
        public DateTime? MediclaimStartDate { get; set; }
        public DateTime? MediclaimEndDate { get; set; }
        public string? FamilyDoctorName { get; set; }
        public string? FamilyDoctorContact { get; set; }
        public string? MonthlyIncomeRange { get; set; }
        public string? HusbandPreviousMemberId { get; set; }
        public string? WifePreviousMemberId { get; set; }
        public DateTime? DeclarationDate { get; set; }
        public bool? FeeReceived { get; set; }
        public decimal? FeeAmount { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<TMembershipChild> TMembershipChildren { get; set; }
        public virtual ICollection<TMembershipEmrgency> TMembershipEmrgencies { get; set; }
        public virtual ICollection<TMembershipRelative> TMembershipRelatives { get; set; }
    }
}
