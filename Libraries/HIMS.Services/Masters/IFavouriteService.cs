using HIMS.Core.Domain.Grid;
using HIMS.Data.Models;

namespace HIMS.Services.Masters
{
    public partial interface IFavouriteService
    {
        Task<List<FavouriteModel>> GetFavouriteModules(long roleid, long userid);
        Task InsertAsync(TFavouriteUserList objFavourite);
    }
}
