using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using Microsoft.Data.SqlClient;
using System.Transactions;

namespace HIMS.Services.Masters
{
    public class FavouriteService : IFavouriteService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public FavouriteService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<List<FavouriteModel>> GetFavouriteModules(long roleid, long userid)
        {
            DatabaseHelper sql = new();
            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@RoleId", roleid);
            para[1] = new SqlParameter("@UserId", userid);
            List<FavouriteModel> lstMenu = sql.FetchListByQuery<FavouriteModel>(@"select @UserId UserId,M.LinkName,M.Icon,M.LinkAction,P.MenuId,CONVERT(BIT,CASE WHEN F.FavouriteId>0 THEN 1 ELSE 0 END) IsFavourite from MenuMaster M
INNER JOIN PermissionMaster P ON M.Id=P.MenuId AND P.IsView=1 AND P.RoleId=@RoleId
LEFT JOIN T_FavouriteUserList F ON M.Id=F.MenuId AND F.UserId=@UserId", para);
            return lstMenu;
        }

        public virtual async Task InsertAsync(TFavouriteUserList objFavourite)
        {
            var exist = await _context.TFavouriteUserLists.FirstOrDefaultAsync(x => x.UserId == objFavourite.UserId && x.MenuId == objFavourite.MenuId);
            if ((exist?.MenuId ?? 0) > 0)
            {
                _context.TFavouriteUserLists.Remove(exist);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.TFavouriteUserLists.Add(objFavourite);
                await _context.SaveChangesAsync();
            }
        }
    }
}
