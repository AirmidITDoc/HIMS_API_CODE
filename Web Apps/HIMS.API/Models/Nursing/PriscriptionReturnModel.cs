using FluentValidation;

namespace HIMS.API.Models.Nursing
{
    public class PriscriptionReturnModel
    {
        public long PresReId { get; set; }
        public string? PresNo { get; set; }
        public DateTime? PresDate { get; set; }
        public string? PresTime { get; set; }
        public long? ToStoreId { get; set; }
        public long? OpIpId { get; set; }
        public byte? OpIpType { get; set; }
        public long? Addedby { get; set; }
        public long? Isdeleted { get; set; }
        public bool? Isclosed { get; set; }
        public List<IpprescriptionReturnDModel> TIpprescriptionReturnDs { get; set; }

    }
    public class PriscriptionReturnModelValidator : AbstractValidator<PriscriptionReturnModel>
    {
        public PriscriptionReturnModelValidator()
        {
            RuleFor(x => x.PresDate).NotNull().NotEmpty().WithMessage("PresDate  is required");
            RuleFor(x => x.PresTime).NotNull().NotEmpty().WithMessage("PresTime  is required");

        }
    }
    public class IpprescriptionReturnDModel
    {
        public long PresDetailsId { get; set; }
        public long? PresReId { get; set; }
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public double? Qty { get; set; }
        public bool? IsClosed { get; set; }
    }
    public class IpprescriptionReturnDModelValidator : AbstractValidator<IpprescriptionReturnDModel>
    {
        public IpprescriptionReturnDModelValidator()
        {
            RuleFor(x => x.PresReId).NotNull().NotEmpty().WithMessage("PresReId  is required");
            RuleFor(x => x.ItemId).NotNull().NotEmpty().WithMessage("ItemId  is required");

        }
    }
}
