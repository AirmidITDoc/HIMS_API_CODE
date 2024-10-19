using HIMS.Core.Domain.Grid;
using HIMS.Data.Models;

namespace HIMS.Services.Masters
{
    public partial interface IFavouriteService
    {
        Task<IPagedList<FavouriteModel>> GetFavouriteModules(GridRequestModel objGrid, List<SearchGrid> list);
        Task InsertAsync(TFavouriteUserList objFavourite);
    }
}
