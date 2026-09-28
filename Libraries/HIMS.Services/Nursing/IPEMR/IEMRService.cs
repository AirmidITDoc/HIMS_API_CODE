using HIMS.Core.Domain.Grid;
using HIMS.Data.Models;

namespace HIMS.Services.Nursing.IPEMR
{
    public partial interface IEMRService
    {
        Task<TIpEmrhistory?> GetByIdAsync(long id);
        Task InsertAsync(TIpEmrhistory objEMR, int userId, string username);
        Task UpdateAsync(TIpEmrhistory objEMR, int userId, string username, string[]? ignoreColumns = null);
    }
}