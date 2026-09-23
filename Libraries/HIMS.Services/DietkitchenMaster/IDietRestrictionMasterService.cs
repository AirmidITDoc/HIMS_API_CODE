using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Models;

namespace HIMS.Services.DietkitchenMaster
{
    public partial interface IDietRestrictionMasterService  
    {
        Task InsertAsync(MDietRestrictionMaster ObjMDietRestrictionMaster, int UserId, string Username);
        Task UpdateAsync(MDietRestrictionMaster ObjMDietRestrictionMaster, int UserId, string Username, string[]? ignoreColumns = null);

    }
}
