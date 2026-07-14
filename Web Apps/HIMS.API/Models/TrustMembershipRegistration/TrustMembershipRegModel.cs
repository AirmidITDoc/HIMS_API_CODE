using FluentValidation;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.TrustMembershipRegistration
{
    public class TrustMembershipRegModel
    {
        public long MembershipId { get; set; }
        public string MembershipNo { get; set; } = null!;
        public DateTime MembershipDate { get; set; }
        public string? MembershipTime { get; set; }
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
        public string? WifeBloodGroupId { get; set; }
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
        public DateTime? MembershipvalidDate { get; set; }

        public List<MembershipChildModel>? TMembershipChildren { get; set; }
        public List<MembershipEmrgencyModel>? TMembershipEmrgencies { get; set; }
        public List<MembershipRelativeModel>? TMembershipRelatives { get; set; }
    }
    public class TrustMembershipRegModelValidator : AbstractValidator<TrustMembershipRegModel>
    {
        public TrustMembershipRegModelValidator()
        {
            RuleFor(x => x.MembershipNo).NotNull().NotEmpty().WithMessage("MembershipNo is required");
            RuleFor(x => x.MembershipDate).NotNull().NotEmpty().WithMessage("MembershipDate is required");
            RuleFor(x => x.MembershipTime).NotNull().NotEmpty().WithMessage("MembershipTime  is required");
            //RuleFor(x => x.HusbandDob).NotNull().NotEmpty().WithMessage(" HusbandDob required");
            //RuleFor(x => x.HusbandFullBodyCheckupDate).NotNull().NotEmpty().WithMessage("HusbandFullBodyCheckupDate is required");
            //RuleFor(x => x.WifeFullBodyCheckupDate).NotNull().NotEmpty().WithMessage("WifeFullBodyCheckupDate is required");
            RuleFor(x => x.CityId).NotNull().NotEmpty().WithMessage("CityId is required");
            RuleFor(x => x.CityName).NotNull().NotEmpty().WithMessage("CityName is required");
            RuleFor(x => x.NativePlace).NotNull().NotEmpty().WithMessage("NativePlace is required");
            RuleFor(x => x.DeclarationDate).NotNull().NotEmpty().WithMessage("DeclarationDate is required");
            RuleFor(x => x.ReceiptDate).NotNull().NotEmpty().WithMessage("ReceiptDate is required");

        }
    }
    public class MembershipChildModel
    {
        public long ChildId { get; set; }
        public long MembershipId { get; set; }
        public long PrefixId { get; set; }
        public string ChildName { get; set; } = null!;
        public string? ChildMobile { get; set; }
        public string? ChildAddress { get; set; }
        public string? PrefixName { get; set; }


    }
    public class MembershipChildModelValidator : AbstractValidator<MembershipChildModel>
    {
        public MembershipChildModelValidator()
        {
            RuleFor(x => x.ChildId).NotNull().NotEmpty().WithMessage("ChildId  is required");
            RuleFor(x => x.MembershipId).NotNull().NotEmpty().WithMessage(" MembershipId required");
            RuleFor(x => x.PrefixId).NotNull().NotEmpty().WithMessage(" PrefixId required");

        }
    }
    public class MembershipEmrgencyModel
    {
        public long EmrgencyId { get; set; }
        public long? MembershipId { get; set; }
        public long? PrefixId { get; set; }
        public string? EmrgencyName { get; set; }
        public string? EmrgencyMobile { get; set; }
        public string? EmrgencyAddress { get; set; }
        public string? PrefixName { get; set; }

    }
    public class MembershipEmrgencyModelValidator : AbstractValidator<MembershipEmrgencyModel>
    {
        public MembershipEmrgencyModelValidator()
        {
            RuleFor(x => x.EmrgencyId).NotNull().NotEmpty().WithMessage("EmrgencyId is required");
            RuleFor(x => x.MembershipId).NotNull().NotEmpty().WithMessage("MembershipId  is required");
            RuleFor(x => x.PrefixId).NotNull().NotEmpty().WithMessage(" PrefixId required");
            

        }
    }

    public class MembershipRelativeModel
    {
        public long RelativeId { get; set; }
        public long MembershipId { get; set; }
        public long PrefixId { get; set; }
        public long RelationId { get; set; }
        public string? RelativeName { get; set; }
        public string? RelativeMobile { get; set; }
        public string? RelativeAddress { get; set; }
        public string? PrefixName { get; set; }

    }
    public class MembershipRelativeModelValidator : AbstractValidator<MembershipRelativeModel>
    {
        public MembershipRelativeModelValidator()
        {
            RuleFor(x => x.RelativeId).NotNull().NotEmpty().WithMessage("RelativeId is required");
            RuleFor(x => x.MembershipId).NotNull().NotEmpty().WithMessage("MembershipId  is required");
            RuleFor(x => x.PrefixId).NotNull().NotEmpty().WithMessage(" PrefixId required");
            //RuleFor(x => x.RelationId).NotNull().NotEmpty().WithMessage(" RelationId required");

        }
    }
}