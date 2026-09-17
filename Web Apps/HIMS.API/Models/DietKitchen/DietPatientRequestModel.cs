using FluentValidation;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.DietKitchen
{
    public class DietPatientRequestModel
    {
        public long DietReqId { get; set; }
        public DateTime? Date { get; set; }
        public string? Time { get; set; }
        public long? UnitId { get; set; }
        //public string? DietReqNo { get; set; }
        public long? DietMenuId { get; set; }
        public List<DietPatientRequestDetailModel> TDietPatReqDetails { get; set; }

    }
    public class DietPatientRequestModelValidator : AbstractValidator<DietPatientRequestModel>
    {
        public DietPatientRequestModelValidator()
        {
            RuleFor(x => x.Date).NotNull().NotEmpty().WithMessage("Date  is required");
            RuleFor(x => x.Time).NotNull().NotEmpty().WithMessage("Time  is required");
            RuleFor(x => x.UnitId).NotNull().NotEmpty().WithMessage("UnitId  is required");
            RuleFor(x => x.DietMenuId).NotNull().NotEmpty().WithMessage("DietMenuId  is required");


        }
    }
    public class DietPatientRequestDetailModel

    {
        public long DietReqDetId { get; set; }
        public long DietReqId { get; set; }
        public DateTime OrderDate { get; set; }
        public string? OrderTime { get; set; }
        public long Opipid { get; set; }
        public long Opiptype { get; set; }
        public long DietMenuId { get; set; }
        public long MealTypeId { get; set; }
        public long DietTypeId { get; set; }
        public long? DietRestrictionId { get; set; }
        public long? AllergyId { get; set; }
        public long? NutritionistId { get; set; }
        public bool? IsPriority { get; set; }
        public string? Comments { get; set; }
        public long Status { get; set; }
        public bool? IsAccept { get; set; }
        public long? IsAcceptedBy { get; set; }
        public DateTime? IsAcceptedDateTime { get; set; }
        public bool? IsDelived { get; set; }
        public long? IsDelivedBy { get; set; }
        public DateTime? IsDelivedDateTime { get; set; }

    }
    public class DietPatientRequestDetailModelValidator : AbstractValidator<DietPatientRequestDetailModel>
    {
        public DietPatientRequestDetailModelValidator()
        {
            RuleFor(x => x.OrderDate).NotNull().NotEmpty().WithMessage("OrderDate  is required");
            RuleFor(x => x.OrderTime).NotNull().NotEmpty().WithMessage("OrderTime  is required");
            RuleFor(x => x.Opipid).NotNull().NotEmpty().WithMessage("Opipid  is required");
            RuleFor(x => x.Opiptype).NotNull().NotEmpty().WithMessage("Opiptype  is required");
            RuleFor(x => x.DietMenuId).NotNull().NotEmpty().WithMessage("DietMenuId  is required");
            RuleFor(x => x.MealTypeId).NotNull().NotEmpty().WithMessage("MealTypeId  is required");
            RuleFor(x => x.IsAcceptedBy).NotNull().NotEmpty().WithMessage("IsAcceptedBy  is required");
            RuleFor(x => x.IsDelivedBy).NotNull().NotEmpty().WithMessage("IsDelivedBy  is required");
            RuleFor(x => x.IsDelivedDateTime).NotNull().NotEmpty().WithMessage("IsDelivedDateTime  is required");

        }
        public class DietPatientRequestCancel
        {
            public long DietReqId { get; set; }
            public long? IsCancelledBy { get; set; }
            public string? CancelledReason { get; set; }

        }
        public class DietPatientRequestDetailsCancel
        {
            public long DietReqDetId { get; set; }
            public long? IsCancelledBy { get; set; }
            public string? CancelledReason { get; set; }

        }
        public class DietPatientRequestDetailsAccept
        {
            public long DietReqDetId { get; set; }
            public bool? IsAccept { get; set; }
            public long? IsAcceptedBy { get; set; }
            public DateTime? IsAcceptedDateTime { get; set; }
        }
    }
}

    

