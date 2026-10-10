using FluentValidation;
using System;

namespace HIMS.API.Models.Inventory
{
    public class ImmunizatoinInfoModel
    {
        public long ImmunizatoinId { get; set; }
        public long? Opipid { get; set; }
        public byte? Opiptype { get; set; }
        public long? RegId { get; set; }
        public long? VaccineId { get; set; }
        public string? LotNumber { get; set; }
        public DateTime? LotExpDate { get; set; }
        public DateTime? OccuranceDate { get; set; }
        public string? PrimarySource { get; set; }
        public DateTime? RecommandationDate { get; set; }
        public string? ForecastStatus { get; set; }
        public string? Series { get; set; }
        public string? Description { get; set; }
        public long? DoseNumber { get; set; }
        public string? SeriesDoses { get; set; }
        public DateTime? RecommendedDate { get; set; }
        public string? Comment { get; set; }
        public string? SupportingImmunization { get; set; }
        public string? Status { get; set; }
    }

    public class ImmunizatoinInfoModelValidator : AbstractValidator<ImmunizatoinInfoModel>
    {
        public ImmunizatoinInfoModelValidator()
        {
            RuleFor(x => x.RegId).NotNull().NotEmpty().WithMessage("RegId is required");
            RuleFor(x => x.VaccineId).NotNull().NotEmpty().WithMessage("VaccineId is required");
        }
    }
}
