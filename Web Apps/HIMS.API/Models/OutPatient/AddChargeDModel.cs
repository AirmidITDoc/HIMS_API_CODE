using FluentValidation;
using HIMS.Data.Models;
using HIMS.API.Models.IPPatient;
using Microsoft.AspNetCore.Mvc;
namespace HIMS.API.Models.OutPatient
{
    public class AddChargesDeleteModel
    {
        public long? ChargesId { get; set; }
        public long? UserId { get; set; }
    }
    public class AddChargesDeleteModelValidator : AbstractValidator<AddChargesDeleteModel>
    {
        public AddChargesDeleteModelValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty().WithMessage("UserId is required");

        }
    }
    public class AddChargeDModel
    {

        public AddChargesDeleteModel DeleteCharges { get; set; }


    }

}