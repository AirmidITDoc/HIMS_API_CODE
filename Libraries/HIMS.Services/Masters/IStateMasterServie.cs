using HIMS.Core.Domain.Grid;
using HIMS.Data.Models;

namespace HIMS.Services.Masters
{


    public partial interface IStateMasterServie
    {
        Task<IPagedList<MCityMaster>> GetAllPagedAsync(GridRequestModel objGrid);
    }
}
