using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.DietkitchenMaster
{
    public partial interface IDietCategoryMasterService
    {
        Task InsertAsync(MDietCategoryMaster ObjMDietCategoryMaster, int UserId, string Username);
        Task UpdateAsync(MDietCategoryMaster ObjMDietCategoryMaster, int UserId, string Username, string[]? ignoreColumns = null);  

    }
}
