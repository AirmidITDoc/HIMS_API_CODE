using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;

namespace HIMS.Services.Transaction
{
    public partial interface IsmsConfigService
    {
        Task<IPagedList<SMSConfigListDto>> GetSMSconfig(GridRequestModel objGrid);
        Task InsertAsyncSP(SsSmsConfig objSsSmsConfig, int UserId, string Username);
        Task UpdateAsyncSP(SsSmsConfig objSsSmsConfig, int UserId, string Username);
    }
}
