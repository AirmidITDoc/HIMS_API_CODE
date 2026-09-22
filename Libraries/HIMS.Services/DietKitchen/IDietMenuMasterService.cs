using System;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Services.DietKitchen;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.GRN;

namespace HIMS.Services.DietKitchen
{
    public partial interface IDietMenuMasterService
    {
        Task<IPagedList<DietmenumasterListDto>> GetDietmenumasterList(GridRequestModel objGrid);

        Task InsertAsync(MDietMenuMaster ObjMDietMenuMaster, int UserId, string Username);

        Task UpdateAsync(MDietMenuMaster model, int currentUserId, string currentUserName, string[]? ignoreColumns = null);

    }
}
