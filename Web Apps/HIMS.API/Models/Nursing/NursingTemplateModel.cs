﻿using FluentValidation;
using HIMS.API.Models.OPPatient;

namespace HIMS.API.Models.Nursing
{
    public class NursingTemplateModel
    {
        public long NursingId { get; set; }
        public string? NursTempName { get; set; }
        public string? TemplateDesc { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }

    }
    public class NursingTemplateModelValidator : AbstractValidator<NursingTemplateModel>
    {
        public NursingTemplateModelValidator()
        {
            RuleFor(x => x.TemplateDesc).NotNull().NotEmpty().WithMessage("TemplateDesc is required");
            RuleFor(x => x.NursTempName).NotNull().NotEmpty().WithMessage("NursTempName is required");
          
        }
    }
}
