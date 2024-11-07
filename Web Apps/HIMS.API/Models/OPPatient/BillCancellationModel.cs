using FluentValidation;
using HIMS.Data.Models;

namespace HIMS.API.Models.OPPatient
{
    public class OpBillCancellationModel
    {
        public long BillNo { get; set; }    

       
    }
    public class OpBillCancellationModelValidator : AbstractValidator<OpBillCancellationModel>
    {
        public OpBillCancellationModelValidator()
        {
            RuleFor(x => x.BillNo).NotNull().NotEmpty().WithMessage("BillNo is required");
          
        }
    }

}
