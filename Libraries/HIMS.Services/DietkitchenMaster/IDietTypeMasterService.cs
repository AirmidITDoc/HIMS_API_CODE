using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.DietkitchenMaster
{
    public partial interface IDietTypeMasterService
    {
        Task InsertAsync(MDietTypeMaster ObjMDietTypeMaster, int UserId, string Username);
        Task UpdateAsync(MDietTypeMaster ObjMDietTypeMaster, int UserId, string Username, string[]? ignoreColumns = null);


    }
}
