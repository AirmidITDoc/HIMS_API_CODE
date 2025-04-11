using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class ParameterMasterModel
    {
        public long ParameterId { get; set; }
        public string? ParameterShortName { get; set; }
        public string? ParameterName { get; set; }
        public string? PrintParameterName { get; set; }
        public long? UnitId { get; set; }
        public int? IsNumeric { get; set; }
        public bool? IsPrintDisSummary { get; set; }
        public string? MethodName { get; set; }
        public string? Formula { get; set; }
        public string? IsBoldFlag { get; set; }

        public List<MParameterDescriptiveMasterModel>? MParameterDescriptiveMasters { get; set; }
        public List<MPathParaRangeWithAgeMasterModel>? MPathParaRangeWithAgeMasters { get; set; }
    }
    public class ParameterMasterModelValidator : AbstractValidator<ParameterMasterModel>
    {
        public ParameterMasterModelValidator()
        {
            RuleFor(x => x.ParameterShortName).NotNull().NotEmpty().WithMessage("Patient Type is required");
            RuleFor(x => x.ParameterName).NotNull().NotEmpty().WithMessage("ParameterName is required");
            RuleFor(x => x.PrintParameterName).NotNull().NotEmpty().WithMessage("PrintParameterName is required");
            RuleFor(x => x.UnitId).NotNull().NotEmpty().WithMessage("UnitId is required");
           
        }
    }
    public class MParameterDescriptiveMasterModel
    {
        public long DescriptiveId { get; set; }
        public long? ParameterId { get; set; }
        public string? ParameterValues { get; set; }
        public bool? IsDefaultValue { get; set; }
        public string? DefaultValue { get; set; }
    }
    public class MParameterDescriptiveMasterModelValidator : AbstractValidator<MParameterDescriptiveMasterModel>
    {
        public MParameterDescriptiveMasterModelValidator()
        {
            RuleFor(x => x.ParameterValues).NotNull().NotEmpty().WithMessage("ParameterValues  is required");
            RuleFor(x => x.IsDefaultValue).NotNull().NotEmpty().WithMessage("IsDefaultValue  is required");

        }
    }
    public class MPathParaRangeWithAgeMasterModel
    {
        public long PathparaRangeId { get; set; }
        public long? ParaId { get; set; }
        public long? SexId { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public string? AgeType { get; set; }
        public string? MinValue { get; set; }
        public string? MaxValue { get; set; }
        public bool? IsDeleted { get; set; }
        public long? Addedby { get; set; }
        public long? Updatedby { get; set; }
    }
    public class MPathParaRangeWithAgeMasterModelValidator : AbstractValidator<MPathParaRangeWithAgeMasterModel>
    {
        public MPathParaRangeWithAgeMasterModelValidator()
        {
            RuleFor(x => x.SexId).NotNull().NotEmpty().WithMessage("SexId  is required");
           
        }
    }
    public class CancelParameter
    {
        public long ParameterId { get; set; }
    }

    public class UpdateParameterFormulaModel
    {
        public long ParameterId { get; set; }
        public string? Formula { get; set; }
    }
}
