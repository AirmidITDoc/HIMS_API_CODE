using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class ScheduleModel
    {
        public int Id { get; set; }
        public string ScheduleName { get; set; }
        public SchedulerTypes ScheduleType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Days { get; set; }
        public string Dates { get; set; }
        public string Hours { get; set; }
        public string Custom { get; set; }
        public string Query { get; set; }
    }
    public enum SchedulerTypes
    {
        Daily = 1, Weekly = 2, Monthly = 3, Custom = 4
    }
    public class ScheduleModelValidator : AbstractValidator<ScheduleModel>
    {
        public ScheduleModelValidator()
        {
            RuleFor(x => x.ScheduleName).NotNull().NotEmpty().WithMessage("Schedule Name is required");
            RuleFor(x => x.ScheduleType).NotNull().NotEmpty().WithMessage("Schedule Type is required");
            RuleFor(x => x.Hours).NotNull().NotEmpty().WithMessage("Hours is required");
            RuleFor(x => x.StartDate).NotNull().NotEmpty().WithMessage("Start Date is required");
            RuleFor(x => x.EndDate).NotNull().NotEmpty().WithMessage("End Date is required");
            RuleFor(x => x.Query).NotNull().NotEmpty().WithMessage("Query is required");
        }
    }

    public class FavouriteDtoModel
    {
        public int UserId { get; set; }
        public int MenuId { get; set; }
    }
    public class FavouriteDtoModelValidator : AbstractValidator<FavouriteDtoModel>
    {
        public FavouriteDtoModelValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty().WithMessage("User Id is required");
            RuleFor(x => x.MenuId).NotNull().NotEmpty().WithMessage("Menu Id is required");
        }
    }
}
