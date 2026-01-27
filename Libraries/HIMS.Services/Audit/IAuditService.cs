using HIMS.Core.Domain.Grid;
using HIMS.Data.Models;

namespace HIMS.Services.Audit
{
    public partial interface IAuditService
    {
        Task<IPagedList<AuditLog>> GetAllPagedAsync(GridRequestModel objGrid);
    }
}
