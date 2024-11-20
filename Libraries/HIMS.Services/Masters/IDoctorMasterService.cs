using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Master;
using HIMS.Data.Models;

namespace HIMS.Services.Masters
{
    public partial interface IDoctorMasterService
    {
        Task InsertAsync(DoctorMaster objDoctor, int UserId, string Username);
        Task InsertAsyncSP(DoctorMaster objDoctor, int UserId, string Username);
        Task UpdateAsync(DoctorMaster objDoctor, int UserId, string Username);
        Task<IPagedList<DoctoreMasterDto>> GetListAsync(GridRequestModel objGrid);

    }
}
