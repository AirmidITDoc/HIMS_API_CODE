using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class SpecialtyMasterModel
    {
        public long SpecialtyId { get; set; }
        public string SpecialtyName { get; set; }
        
    }
    public class SpecialtyMasterModelValidator : CommonDynamicValidator<SpecialtyMasterModel>
    {
        public SpecialtyMasterModelValidator()
        {
          

        }
    }
}