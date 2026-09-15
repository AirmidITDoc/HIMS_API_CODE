using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HIMS.Data.Models
{
    [Table("M_FoodItemMaster")]
    public class MFoodItemMaster
    {
        [Key]
        public long FoodItemId { get; set; }
        public string? FoodCode { get; set; }
        public string? FoodName { get; set; }
        public long? FoodCategoryId { get; set; }  
        public string? LocalName { get; set; }
        public long? Unit { get; set; }           
        public bool? IsVegetarian { get; set; }
        public bool? Active { get; set; }
        public long? CreatedBy { get; set; }      
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }     
        public DateTime? ModifiedDate { get; set; }
    }
}