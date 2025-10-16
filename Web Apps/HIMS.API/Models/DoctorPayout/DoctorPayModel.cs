using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.DoctorPayout
{
    public class DoctorPayModel
    {
        public DateTime? TranDate { get; set; }
        public string? TranTime { get; set; }
        public long? PbillNo { get; set; }
        public long? CompanyId { get; set; }
        public DateTime? BillDate { get; set; }
        public string? ServiceName { get; set; }
        public decimal? Price { get; set; }
        public float? Qty { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? DocAmount { get; set; }
        public long? IsProcess { get; set; }
        public long? DocId { get; set; }
        public string? PatientName { get; set; }
    }
    public class DoctorPayModelValidator : AbstractValidator<DoctorPayModel>
    {
        public DoctorPayModelValidator()
        {
            RuleFor(x => x.TranDate).NotNull().NotEmpty().WithMessage("TranDate  is required");
            RuleFor(x => x.TranDate).NotNull().NotEmpty().WithMessage("TranDate  is required");
            RuleFor(x => x.PbillNo).NotNull().NotEmpty().WithMessage("TranDate  is required");
            RuleFor(x => x.CompanyId).NotNull().NotEmpty().WithMessage("CompanyId  is required");
            RuleFor(x => x.BillDate).NotNull().NotEmpty().WithMessage("BillDate  is required");
            RuleFor(x => x.ServiceName).NotNull().NotEmpty().WithMessage("ServiceName  is required");


        }
    }
}
