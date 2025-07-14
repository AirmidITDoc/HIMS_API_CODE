using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OTManagment
{
    public partial interface IEmergencyService
    {
        Task<IPagedList<EmergencyListDto>> GetListAsyn(GridRequestModel objGrid);
        Task InsertAsyncSP(TEmergencyAdm objTEmergencyAdm, int UserId, string Username);
        Task UpdateSP(TEmergencyAdm objTEmergencyAdm, int UserId, string UserName);
        Task CancelSP(TEmergencyAdm objTEmergencyAdm, int UserId, string UserName);




    }
}
