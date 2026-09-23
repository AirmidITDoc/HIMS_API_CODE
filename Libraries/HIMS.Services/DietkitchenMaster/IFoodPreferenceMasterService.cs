using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Models;

namespace HIMS.Services.DietkitchenMaster
{
    public partial interface IFoodPreferenceMasterService 
    {
        Task InsertAsync(MFoodPreferenceMaster ObjMFoodPreferenceMaster, int UserId, string Username);
        Task UpdateAsync(MFoodPreferenceMaster ObjMFoodPreferenceMaster, int UserId, string Username, string[]? ignoreColumns = null);

    }
}

