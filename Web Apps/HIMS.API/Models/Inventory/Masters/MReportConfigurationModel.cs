using System;
using System.Collections.Generic;
using FluentValidation;
using HIMS.API.Models.Inventory.Masters;

namespace HIMS.Data.Models
{
    public partial class MReportConfigurationModel
    {
        public long ReportId { get; set; }
        public string? ReportSection { get; set; }
        public string? ReportName { get; set; }
    }
    public class MReportConfigurationModelValidator : AbstractValidator<MReportConfigurationModel>
    {
        public MReportConfigurationModelValidator()
        {
            RuleFor(x => x.ReportSection).NotNull().NotEmpty().WithMessage("ReportSection is required");
            RuleFor(x => x.ReportName).NotNull().NotEmpty().WithMessage("ReportName is required");

        }
    }
}
