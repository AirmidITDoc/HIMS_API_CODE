using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;

namespace HIMS.Services.Masters
{
    public partial interface IParameterMasterService
    {
        Task<IPagedList<MPathParameterListDto>> MPathParameterList(GridRequestModel objGrid);
        Task InsertAsync(MPathParameterMaster objPara, int UserId, string Username);
        //Task InsertAsyncSP(MPathParameterMaster objPara, int UserId, string Username);
        Task UpdateAsync(MPathParameterMaster objPara, int UserId, string Username);
        Task CancelAsync(MPathParameterMaster objPara, int UserId, string Username);
    }
}
