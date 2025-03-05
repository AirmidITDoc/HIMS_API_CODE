using HIMS.Core.Domain.Grid;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using LinqToDB;

namespace HIMS.Services.Masters
{
    public class PrefixService : IPrefixService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public PrefixService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<DbPrefixMaster>> GetAllPagedAsync(GridRequestModel objGrid)
        {
            var qry = from p in _context.DbPrefixMasters
                      join s in _context.DbGenderMasters on p.SexId equals s.GenderId
                      select new DbPrefixMaster() { PrefixId = p.PrefixId, PrefixName = p.PrefixName, IsActive = p.IsActive, GenderName = s.GenderName, SexId = p.SexId };
            return await qry.BuildPredicate(objGrid);
        }
    }
}
