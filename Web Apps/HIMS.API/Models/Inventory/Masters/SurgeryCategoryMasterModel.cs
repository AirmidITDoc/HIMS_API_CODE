using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class SurgeryCategoryMasterModel
    {
        public long SurgeryCategoryId { get; set; }
        public string? SurgeryCategoryName { get; set; }
    }
}