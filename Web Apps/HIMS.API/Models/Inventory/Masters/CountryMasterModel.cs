﻿using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class CountryMasterModel
    {
        public long CountryId { get; set; }
        public string? CountryName { get; set; }
       
    }
    public class CountryMasterModelValidator : AbstractValidator<CountryMasterModel>
    {
        public CountryMasterModelValidator()
        {
            RuleFor(x => x.CountryName).NotNull().NotEmpty().WithMessage("Country is required");
        }
    }

}
