using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Models;

namespace HIMS.Services.DietkitchenMaster
{

    public partial interface IMFoodCategoryMasterService
    { 
        Task InsertAsync(MFoodCategoryMaster ObjMFoodItemMaster, int currentUserId, string currentUserName);
        Task UpdateAsync(MFoodCategoryMaster ObjMFoodItemMaster, int currentUserId, string currentUserName, string[] strings);
    }
}
