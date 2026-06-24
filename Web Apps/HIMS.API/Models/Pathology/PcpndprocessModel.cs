using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.Pathology
{
    public class PcpndprocessModel
    {
        public long PcpndtprocessId { get; set; }
        public DateTime? ProcessDate { get; set; }
        public long? Opipid { get; set; }
        public byte? Opiptype { get; set; }
        public string? RelativeName { get; set; }
        public string? ChildrenCount { get; set; }
        public string? DaughtersDetails { get; set; }
        public string? SonsDetails { get; set; }
        public string? Mperiod { get; set; }
        public long? RefDocId { get; set; }
        public long? ConsultantDocId { get; set; }
        public string? NonInvasive { get; set; }
        public string? Indication { get; set; }
        public string? Prenatal { get; set; }
        public DateTime? ResultDate { get; set; }
        public bool? Ultrasound { get; set; }
        public bool? Obs { get; set; }
        public bool? Pelvic { get; set; }
        public long? InvasiveDoctorId { get; set; }
        public long? ComplicationsId { get; set; }
        public bool? Clinical { get; set; }
        public bool? BioChemical { get; set; }
        public bool? Cytogenetic { get; set; }
        public bool? OtherRadiological { get; set; }
        public bool? Chromosomaldisorder { get; set; }
        public bool? Metabolicdisorder { get; set; }
        public bool? Congenitalanomaly { get; set; }
        public bool? MentalDisability { get; set; }
        public bool? Haemoglobinopathy { get; set; }
        public bool? SexLinkeddisorder { get; set; }
        public bool? Singlegenedisorder { get; set; }
        public long? AnyOther1 { get; set; }
        public bool? Mage { get; set; }
        public bool? GeneticDisease { get; set; }
        public long? AnyOtherIndication { get; set; }
        public bool? Chromosomal { get; set; }
        public bool? Molecular { get; set; }
        public bool? PreImplantation { get; set; }
        public long? AnyOtherTest { get; set; }
        public string? TestResult { get; set; }
        public DateTime? ProcedureDate { get; set; }
        public string? ResultConveyedto { get; set; }
        public string? IndicationofMtp { get; set; }
        public bool? Amniocentesis { get; set; }
        public bool? ChorionicVilliaspiration { get; set; }
        public bool? FetalBiopsy { get; set; }
        public bool? Cordocentesis { get; set; }
        public long? AnyOther2 { get; set; }
        public DateTime? ConsentDate { get; set; }
        public long? DeclarationDoctorid { get; set; }
        public List<PcpndprocessDetailModel> TPcpndprocessDetails { get; set; }

    }
    public class PcpndprocessModelValidator : AbstractValidator<PcpndprocessModel>
    {
        public PcpndprocessModelValidator()
        {
            RuleFor(x => x.ProcessDate).NotNull().NotEmpty().WithMessage("ProcessDate is required");
            RuleFor(x => x.ResultDate).NotNull().NotEmpty().WithMessage("ResultDate is required");
            RuleFor(x => x.ProcedureDate).NotNull().NotEmpty().WithMessage("ProcedureDate is required");
            RuleFor(x => x.ConsentDate).NotNull().NotEmpty().WithMessage("ConsentDate is required");

        }
    }
    public class PcpndprocessDetailModel
    {
        public long PcpndprocessDetId { get; set; }
        public long? PcpndtprocessId { get; set; }
        public string? IndicationDesc { get; set; }
        public bool? IndicationValues { get; set; }
    }
    public class PcpndprocessDetailModelValidator : AbstractValidator<PcpndprocessDetailModel>
    {
        public PcpndprocessDetailModelValidator()
        {
            RuleFor(x => x.IndicationDesc).NotNull().NotEmpty().WithMessage("IndicationDesc is required");
            RuleFor(x => x.IndicationValues).NotNull().NotEmpty().WithMessage("IndicationValues is required");
           

        }
    }
}
