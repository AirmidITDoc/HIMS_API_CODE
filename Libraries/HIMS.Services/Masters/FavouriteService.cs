using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using System.Collections.Generic;
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
        public virtual async Task<IPagedList<FavouriteModel>> GetFavouriteModules(GridRequestModel objGrid,List<SearchGrid> list)
        {
            List<SearchModel> fields = SearchFieldExtension.GetSearchFields(list);

            long roleid = Convert.ToInt64(fields.Find(x => x.FieldName?.ToLower() == "roleid".ToLower())?.FieldValueString);
            long userid = Convert.ToInt64(fields.Find(x => x.FieldName?.ToLower() == "userid".ToLower())?.FieldValueString);

            var query = (from M in _context.MenuMasters
                        join P in _context.PermissionMasters on M.Id equals P.MenuId 
                        join F in _context.TFavouriteUserLists on M.Id equals F.MenuId
                         where P.IsView == true
                            && P.RoleId == roleid
                            && F.UserId == userid
                         orderby M.Id
                        select new FavouriteModel()
                        {
                            UserId = userid,
                            LinkName = M.LinkName,
                            Icon = M.Icon ?? string.Empty,
                            LinkAction = M.LinkAction ?? string.Empty,
                            MenuId = P.MenuId,
                            IsFavourite = F.FavouriteId > 0
                        });

            //if (!string.IsNullOrWhiteSpace(objGrid.SortField))
            //{
            //    query = query.OrderBy(objGrid.SortField, objGrid.SortOrder == -1);
            //}
            return await query.ToPagedListAsync(objGrid.First, objGrid.Rows, objGrid.ExportType != 0 || objGrid.Rows == -1);
        }

        public virtual async Task InsertAsync(TFavouriteUserList objFavourite)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Delete Exsting match table records
                var lst = await _context.TFavouriteUserLists.Where(x => x.MenuId == objFavourite.MenuId && x.UserId == objFavourite.UserId).ToListAsync();
                _context.TFavouriteUserLists.RemoveRange(lst);

                // Add new table records
                _context.TFavouriteUserLists.Add(objFavourite);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
    }
}
