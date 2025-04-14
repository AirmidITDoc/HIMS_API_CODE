using FluentValidation;
using HIMS.Data.Models;

namespace HIMS.API.Models.OutPatient
{


    public class BilllsModel
    {
        public long? BillNo { get; set; }
        public DateTime? BillDate { get; set; }
        public string? BillTime { get; set; }
    }
    public class BilllsModelValidator : AbstractValidator<BilllsModel>
    {
        public BilllsModelValidator()
        {
            RuleFor(x => x.BillTime).NotNull().NotEmpty().WithMessage("BillTime is required");
        }
    }

}