﻿using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class ClassMasterModel
    {
        public long ClassId { get; set; }
        public string? ClassName { get; set; }
    }
    public class ClassMasterModelValidator : AbstractValidator<ClassMasterModel>
    {
        public ClassMasterModelValidator()
        {
            RuleFor(x => x.ClassName).NotNull().NotEmpty().WithMessage("Class  is required");
        }
    }

}