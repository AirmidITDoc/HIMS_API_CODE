using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;

namespace HIMS.Services.Masters
{
    public partial interface IDoctorMasterService
    {
        Task InsertAsync(DoctorMaster objDoctorMaster, int UserId, string Username);
        Task InsertAsyncSP(DoctorMaster objDoctorMaster, int UserId, string Username);
        Task UpdateAsync(DoctorMaster objDoctorMaster, int UserId, string Username);
        Task<IPagedList<DoctorMaster>> GetListAsync(GridRequestModel objGrid);
        Task<DoctorMaster> GetById(int Id);

        Task<IPagedList<DoctorMaster>> GetAllPagedAsync(GridRequestModel objGrid);
        Task<IPagedList<LvwDoctorMasterList>> GetListAsync1(GridRequestModel model);

    }
}
