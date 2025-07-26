using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public  class LoginConfigUserWiseListDto
    {
       
        public long? LoginId { get; set; }
        public long? AccessValueId { get; set; }
        public bool? AccessValue { get; set; }
        public string? AccessInputValue { get; set; }
        public string? AccessValueName { get; set; }
       

    }
    public class LoginStoreUserWiseListDto
    {
        public long? LoginId { get; set; }
        public long? StoreId { get; set; }


    }
    public class LoginAccessConfigListDto
    {
        public long? LoginConfigId { get; set; }
        public string? Name { get; set; }
        public long? AccessCategoryId { get; set; }
        public string? AccessValueId { get; set; }
        public bool? IsInputField { get; set; }



    }
    public class LoginUnitUserWiseListDto
    {
        public long? LoginId { get; set; }
        public long? UnitId { get; set; }



    }
}
