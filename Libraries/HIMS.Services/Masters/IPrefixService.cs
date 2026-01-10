using HIMS.Core.Domain.Grid;
using HIMS.Data.Models;

namespace HIMS.Services.Masters
{
    public partial interface IPrefixService
    {
        Task<IPagedList<DbPrefixMaster>> GetAllPagedAsync(GridRequestModel objGrid);
        Task<Tuple<List<TestA>, List<TestB>>> GetListMultiple();
    }
}
