using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.IPPatient
{
    public class ICDUpdateModel
    {
        public long Hid { get; set; }
        public DateTime? ReqDate { get; set; }
        public string? ReqTime { get; set; }
        public byte? OpIpType { get; set; }
        public long? OpIpId { get; set; }
        public long? AddedBy { get; set; }

        public long? UpdatedBy { get; set; }

    }
    public class ICDUpdateModelValidator : AbstractValidator<ICDUpdateModel>
    {
        public ICDUpdateModelValidator()
        {
         //   RuleFor(x => x.Hid).NotNull().NotEmpty().WithMessage("Hid  is required");
        

        }
    }

    public class TPatIcdcdeDModel
    {
        public long? Hid { get; set; }
        public string? IcdCode { get; set; }
        public string? IcdCodeDesc { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public string? IcdcdeMainName { get; set; }
        public long? MainIcdcdeId { get; set; }
    }
    public class TPatIcdcdeDModelValidator : AbstractValidator<TPatIcdcdeDModel>
    {
        public TPatIcdcdeDModelValidator()
        {
            //RuleFor(x => x.Did).NotNull().NotEmpty().WithMessage("Icdversion  is required");


        }
    }
    public class ICDupdateModel
    {
        public ICDUpdateModel TPatIcdcdeH { get; set; }
        public List<TPatIcdcdeDModel> TPatIcdcdeD { get; set; }

    }
}
