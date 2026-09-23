using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Models;

namespace HIMS.Services.DietkitchenMaster
{
    public partial interface IAllergyMasterService
    {
        Task InsertAsync(MAllergyMaster ObjMAllergyMaster, int UserId, string Username);
        Task UpdateAsync(MAllergyMaster ObjMAllergyMaster, int UserId, string Username, string[]? ignoreColumns = null);

    }
}


