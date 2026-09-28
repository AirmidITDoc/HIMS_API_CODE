using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.DietKitchen;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.DietkitchenMaster
{
    public partial interface IAllergyMasterService
    {
        Task InsertAsync(MAllergyMaster ObjMAllergyMaster, int UserId, string Username);
        Task UpdateAsync(MAllergyMaster ObjMAllergyMaster, int UserId, string Username, string[]? ignoreColumns = null);
        Task<IPagedList<AllergyListDto>> GetAllergyListAsync(GridRequestModel model);

    }
}


