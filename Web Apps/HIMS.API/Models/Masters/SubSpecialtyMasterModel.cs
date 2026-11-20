using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class SubSpecialtyMasterModel
    {
        public long SubSpecialtyId { get; set; }
        public long SpecialtyId { get; set; }
        public string SubSpecialtyName { get; set; }
       
    }
    public class SubSpecialtyMasterModelValidator : CommonDynamicValidator<SubSpecialtyMasterModel>
    {
        public SubSpecialtyMasterModelValidator()
        {
           
        }
    }
}