using FluentValidation;

namespace HIMS.API.Models.Administration
{
    public class MDoctorPerMasterModel
    {
        public long DoctorShareId { get; set; }
        public long? DoctorId { get; set; }
        public long? ServiceId { get; set; }
        public byte? DocShrType { get; set; }
        public string? DocShrTypeS { get; set; }
        public decimal? ServicePercentage { get; set; }
        public decimal? ServiceAmount { get; set; }
        public long? ClassId { get; set; }
        public long? ShrTypeSerOrGrp { get; set; }
        public byte? OpIpType { get; set; }
    }
    public class MDoctorPerMasterModelValidator : AbstractValidator<MDoctorPerMasterModel>
    {
        public MDoctorPerMasterModelValidator()
        {
            RuleFor(x => x.DoctorId).NotNull().NotEmpty().WithMessage("DoctorId is required");
            RuleFor(x => x.ServiceId).NotNull().NotEmpty().WithMessage("ServiceId is required");



        }
    }


}