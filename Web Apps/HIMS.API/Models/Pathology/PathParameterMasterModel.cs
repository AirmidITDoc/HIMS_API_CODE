﻿using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.Pathology
{
    public class PathParameterMasterModel
    {
        public long ParameterId { get; set; }
        public string? ParameterShortName { get; set; }
        public string? ParameterName { get; set; }
        public string? PrintParameterName { get; set; }
        public long? UnitId { get; set; }
        public long? IsNumeric { get; set; }
        public bool? IsPrintDisSummary { get; set; }

    }
    public class PatientTypeModelValidator : AbstractValidator<PatientTypeModel>
    {
        public PatientTypeModelValidator()
        {
            RuleFor(x => x.PatientType).NotNull().NotEmpty().WithMessage("Patient Type is required");
        }
    }
}