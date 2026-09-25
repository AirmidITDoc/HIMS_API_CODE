using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.DietKitchen
{
    public class DietmenuDetailmasterListDto
    {
        public long DietMenuId { get; set; }
        public string? DietMenuCode { get; set; } 
        public string? DietMenuName { get; set; }
        public string? FoodName { get; set; }
        public string? Name { get; set; }
        public string? ConstantType { get; set; }
        public string? DietMeValuenuName { get; set; }
        public int? Quantity { get; set; }
        public long? UnitId { get; set; }
        public int? SequenceNo { get; set; }
        public long FoodItemId { get; set; }
        public long MenuDetId { get; set; }




    }
}

