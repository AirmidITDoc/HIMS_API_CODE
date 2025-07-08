using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Master;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Masters
{
    public partial interface ICampMasterService
    {
        Task<MCampMaster> GetById(int Id);
        //Task<IPagedList<CampMasterListDto>> MPathParameterList(GridRequestModel objGrid);
        Task InsertAsync(MCampMaster objMCampMaster, int UserId, string Username);
        Task UpdateAsync(MCampMaster objMCampMaster, int UserId, string Username);
        Task CancelAsync(MCampMaster objMCampMaster, int UserId, string Username);
        //Task UpdateFormulaAsync(MCampMaster objMCampMaster, int UserId, string Username);
    }
}
