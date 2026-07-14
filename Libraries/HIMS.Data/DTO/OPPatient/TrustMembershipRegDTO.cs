using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OPPatient
{
    public  class TrustMembershipRegDTO
    {

        public long MembershipId { get; set; }
        public string? MembershipNo { get; set; }
        public DateTime? MembershipDate { get; set; }
        public string? MembershipTime { get; set; }
        public long HprefixId { get; set; }
        public long HgenderId { get; set; }
        public string? PatientName { get; set; }
        public DateTime HusbandDob { get; set; }
        public long? HusbandAgeY { get; set; }
        public long? HusbandAgeM { get; set; }
        public long? HusbandAgeD { get; set; }
        public string? HusbandEmail { get; set; }
        public string? HusbandMobile { get; set; }
        public string? HusbandAadhaar { get; set; }
        public string? HusbandPan { get; set; }
        public string? HusbandEducation { get; set; }
        public string? HusbandBloodGroupId { get; set; }
        public string? HusbandHobbies { get; set; }
        public string? HusbandMedications { get; set; }
        public DateTime? HusbandFullBodyCheckupDate { get; set; }
        public string? HusbandPhoto { get; set; }
        public long WprefixId { get; set; }
        public long WgenderId { get; set; }
        public string? WifeFirstName { get; set; }
        public string? WifeMiddleName { get; set; }
        public string? WifeLastName { get; set; }
        public string? WifeName { get; set; }
        public string? WifeParentalDetails { get; set; }
        public DateTime WifeDob { get; set; }
        public long? WifeAgeY { get; set; }
        public long? WifeAgeM { get; set; }
        public long? WifeAgeD { get; set; }
        public string? WifeMobile { get; set; }
        public string? WifeEmail { get; set; }
        public string? WifeBloodGroupId { get; set; }
        public string? WifeAadhaar { get; set; }
        public string? WifePAN { get; set; }
        public long? WifeOccupationId { get; set; }
        public long? HusbandOccupationId { get; set; }
        public string? WifeEducation { get; set; }
        public string? WifeHobbies { get; set; }
        public string? WifeParentsNativePlace { get; set; }
        public string? WifeMedications { get; set; }
        public DateTime? WifeFullBodyCheckupDate { get; set; }
        public string? CityName { get; set; }
        public string? ResidenceAddress { get; set; }
        public string? ResidenceType { get; set; }
        public string? NativePlace { get; set; }
        public bool? AyushmanEnrolled { get; set; }
        public bool? MaleFemaleEnrolled { get; set; }
        public string? AyushmanSpouseDetails { get; set; }
        public bool? HasMediclaim { get; set; }
        public long? MediclaimCompany { get; set; }
        public string? MediclaimPolicyNumber { get; set; }
        public decimal? MediclaimIssuanceAmt { get; set; }
        public DateTime? MediclaimStartDate { get; set; }
        public DateTime? MediclaimEndDate { get; set; }
        public string? FamilyDoctorName { get; set; }
        public string? FamilyDoctorContact { get; set; }
        public long? MonthlyIncomeRange { get; set; }
        public string? HusbandPreviousMemberId { get; set; }
        public string? WifePreviousMemberId { get; set; }
        public DateTime? DeclarationDate { get; set; }
        public bool? FeeReceived { get; set; }
        public decimal? FeeAmount { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public string? HOcccupation { get; set; }
        public string? Value { get; set; }
        public string? HIncome { get; set; }
        public string? WifeOccupation { get; set; }
        public string? CompanyName { get; set; }
        public string? HusbandFirstName { get; set; }
        public string? HusbandMiddleName { get; set; }
        public string? HusbandLastName { get; set; }
        public long? CityId { get; set; }
        public DateTime? MembershipvalidDate { get; set; }





    }
    public class TrustMembershipRegistrationDTO
    {

        public long MembershipId { get; set; }
        public string MembershipNo { get; set; } = null!;
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

