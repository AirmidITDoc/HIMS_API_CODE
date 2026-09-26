using DocumentFormat.OpenXml.Wordprocessing;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.IPPatient;

namespace HIMS.API.Models.DietKitchen
{
    public class DietmenumasterModel
    {
        public long DietMenuId { get; set; }
        //public string DietMenuCode { get; set; } = null!;
        public string? DietMenuName { get; set; }
        public long MealTypeId { get; set; }
        public long DietTypeId { get; set; }
        public string? Texture { get; set; }
        public string? Calories { get; set; }
        public string? Protein { get; set; }
        public bool? Active { get; set; }
        public long? ModifiedBy { get; set; }

    }
    public class DietmenumasterDetailsModel

    {
   
        public long DietMenuId { get; set; }
        public long FoodItemId { get; set; }
        public int? Quantity { get; set; }
        public long? UnitId { get; set; }
        public int? SequenceNo { get; set; }
        public bool? Active { get; set; }
        public long? CreatedBy { get; set; }


    }
    public class DietmenumasterModels
    {
        public DietmenumasterModel Dietmenumaster { get; set; }
        public List<DietmenumasterDetailsModel> MDietMenuDetailMasters { get; set; }

    }
}