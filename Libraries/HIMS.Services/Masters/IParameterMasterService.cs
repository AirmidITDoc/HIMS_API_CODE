using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;

namespace HIMS.Services.Masters
{
    public partial interface IParameterMasterService
    {
        Task<IPagedList<MPathParameterListDto>> MPathParameterList(GridRequestModel objGrid);
        Task InsertAsync(MPathParameterMaster objPara, int UserId, string Username);
        Task UpdateAsync(MPathParameterMaster objPara, int UserId, string Username, string[]? references);
        Task CancelAsync(MPathParameterMaster objPara, int UserId, string Username);
        Task UpdateFormulaAsync(MPathParameterMaster objPara, int UserId, string Username);
    }
}
