using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OPPatient
{
    public  class TrustMembershipRegDTO
    {

        //public long MembershipId { get; set; }
        //public string? MembershipNo { get; set; }
        //public string? FemaleMembershipNo { get; set; }
        //public DateTime? MembershipDate { get; set; }
        //public string? MembershipTime { get; set; }
        //public long HprefixId { get; set; }
        //public long HgenderId { get; set; }
        //public string? PatientName { get; set; }
        //public DateTime HusbandDob { get; set; }
        //public long? HusbandAgeY { get; set; }
        //public long? HusbandAgeM { get; set; }
        //public long? HusbandAgeD { get; set; }
        //public string? HusbandEmail { get; set; }
        //public string? HusbandMobile { get; set; }
        //public string? HusbandAadhaar { get; set; }
        //public string? HusbandPan { get; set; }
        //public string? HusbandEducation { get; set; }
        //public string? HusbandBloodGroupId { get; set; }
        //public string? HusbandHobbies { get; set; }
        //public string? HusbandMedications { get; set; }
        //public DateTime? HusbandFullBodyCheckupDate { get; set; }
        //public string? HusbandPhoto { get; set; }
        //public long WprefixId { get; set; }
        //public long WgenderId { get; set; }
        //public string? WifeFirstName { get; set; }
        //public string? WifeMiddleName { get; set; }
        //public string? WifeLastName { get; set; }
        //public string? WifeName { get; set; }
        //public string? WifeParentalDetails { get; set; }
        //public DateTime WifeDob { get; set; }
        //public long? WifeAgeY { get; set; }
        //public long? WifeAgeM { get; set; }
        //public long? WifeAgeD { get; set; }
        //public string? WifeMobile { get; set; }
        //public string? WifeEmail { get; set; }
        //public string? WifeBloodGroupId { get; set; }
        //public string? WifeAadhaar { get; set; }
        //public string? WifePAN { get; set; }
        //public long? WifeOccupationId { get; set; }
        //public long? HusbandOccupationId { get; set; }
        //public string? WifeEducation { get; set; }
        //public string? WifeHobbies { get; set; }
        //public string? WifeParentsNativePlace { get; set; }
        //public string? WifeMedications { get; set; }
        //public DateTime? WifeFullBodyCheckupDate { get; set; }
        //public string? CityName { get; set; }
        //public string? ResidenceAddress { get; set; }
        //public string? ResidenceType { get; set; }
        //public string? NativePlace { get; set; }
        //public bool? AyushmanEnrolled { get; set; }
        //public bool? MaleFemaleEnrolled { get; set; }
        //public string? AyushmanSpouseDetails { get; set; }
        //public bool? HasMediclaim { get; set; }
        //public long? MediclaimCompany { get; set; }
        //public string? MediclaimPolicyNumber { get; set; }
        //public double? MediclaimIssuanceAmt { get; set; }
        //public DateTime? MediclaimStartDate { get; set; }
        //public DateTime? MediclaimEndDate { get; set; }
        //public string? FamilyDoctorName { get; set; }
        //public string? FamilyDoctorContact { get; set; }
        //public long? MonthlyIncomeRange { get; set; }
        //public string? HusbandPreviousMemberId { get; set; }
        //public string? WifePreviousMemberId { get; set; }
        //public DateTime? DeclarationDate { get; set; }
        //public bool? FeeReceived { get; set; }
        //public decimal? FeeAmount { get; set; }
        //public DateTime? ReceiptDate { get; set; }
        //public string? HOcccupation { get; set; }
        //public string? Value { get; set; }
        //public string? HIncome { get; set; }
        //public string? WifeOccupation { get; set; }
        //public string? CompanyName { get; set; }
        //public string? HusbandFirstName { get; set; }
        //public string? HusbandMiddleName { get; set; }
        //public string? HusbandLastName { get; set; }
        //public long? CityId { get; set; }
        //public DateTime? MembershipvalidDate { get; set; }
        //public DateTime? HdeathDate { get; set; }
        //public DateTime? WdeathDate { get; set; }
        //public string? HaayushmanId { get; set; }
        //public string? WaayushmanId { get; set; }
        //public DateTime? WmembershipvalidDate { get; set; }
        //public DateTime? WreceiptDate { get; set; }
        //public bool? WfeeReceived { get; set; }
        //public long? WfeeAmount { get; set; }
        //public string? Wmediclaimpolicynumber { get; set; }
        //public DateTime? WmediclaimEndDate { get; set; }
        //public DateTime? WmediclaimStartDate { get; set; }
        //public long? WmonthlyIncomeRange { get; set; }
        //public decimal? WmediclaimIssuanceAmt { get; set; }
        //public bool? Whasmediclaim { get; set; }
        //public long? Wmediclaimcompany { get; set; }
        //public string? WfamilyDoctorName { get; set; }
        //public string? WfamilyDoctorContact { get; set; }
        //public string? WresidenceAddress { get; set; }
        //public bool? Wresidencetype { get; set; }

        public long MembershipId { get; set; }
        public string? MembershipNo { get; set; }
        public string? MemberType { get; set; }
        public long? PrefixId { get; set; }
        public long? GenderId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? PatientName { get; set; }
        public DateTime? DOB { get; set; }
        public long? AgeY { get; set; }
        public long? AgeM { get; set; }
        public long? AgeD { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? Aadhaar { get; set; }
        public string? PAN { get; set; }
        public string? BloodGroupId { get; set; }
        public string? Education { get; set; }
        public long? OccupationId { get; set; }
        public string? OccupationName { get; set; }
        public string? Hobbies { get; set; }
        public string? Medications { get; set; }
        public DateTime? FullBodyCheckupDate { get; set; }
        public string? Photo { get; set; }
        public DateTime? DeathDate { get; set; }

        public string? AayushmanId { get; set; }
        public string? PreviousMemberId { get; set; }
        public bool? FeeReceived { get; set; }
        public decimal? FeeAmount { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public bool? HasMediclaim { get; set; }
        public string? MediclaimCompany { get; set; }
        public string? CompanyName { get; set; }
        public string? MediclaimPolicyNumber { get; set; }
        public double? MediclaimIssuanceAmt { get; set; }
        public DateTime? MediclaimStartDate { get; set; }
        public DateTime? MediclaimEndDate { get; set; }
        public string? MonthlyIncomeRange { get; set; }
        public string? IncomeRange { get; set; }
        public string? FamilyDoctorName { get; set; }
        public string? FamilyDoctorContact { get; set; }
        public long? CityId { get; set; }
        public string? CityName { get; set; }
        public string? ResidenceAddress { get; set; }
        public bool? ResidenceType { get; set; }
        public string? NativePlace { get; set; }
        public bool? AyushmanEnrolled { get; set; }
        public bool? MaleFemaleEnrolled { get; set; }
        public string? AyushmanSpouseDetails { get; set; }
        public DateTime? DeclarationDate { get; set; }
        public long? consultDocId { get; set; }
        public long? familyDocId { get; set; }
        public string? MobileNo2 { get; set; }
        public long? hconsultDoctorId { get; set; }
        public long? familyDoctorId { get; set; }
        public long? wconsultDoctorId { get; set; }
        public long? wfamilyDoctorId { get; set; }
        public string? HusbandMobileNo { get; set; }
        public string? WifeMobileNo { get; set; }
        public long SortOrder { get; set; }
        public DateTime? RegDate { get; set; }
        public DateTime? RegTime { get; set; }




    }
    public class TrustMembershipRegistrationDTO
    {

        public long MembershipId { get; set; }
        public string? MembershipNo { get; set; }
        public string? FemaleMembershipNo { get; set; }

        //public DateTime MembershipDate { get; set; }
        //public DateTime MembershipTime { get; set; }
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
        public string? HusbandBloodGroupId { get; set; }
        public string? HusbandEducation { get; set; }
        public long? HusbandOccupationId { get; set; }
        public string? HusbandHobbies { get; set; }
        public string? HusbandMedications { get; set; }
        public DateTime? HusbandFullBodyCheckupDate { get; set; }
        public string? HusbandPhoto { get; set; }
        public long? CityId { get; set; }
        public string? CityName { get; set; }
       




    }
}

