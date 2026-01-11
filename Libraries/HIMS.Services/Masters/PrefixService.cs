using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using LinqToDB;
using Microsoft.Data.SqlClient;

namespace HIMS.Services.Masters
{
    public class TestA
    {
        public long OTRequestId { get; set; }
        public DateTime OTRequestDate { get; set; }
        public DateTime OTRequestTime { get; set; }
        public string OTRequestNo { get; set; }
        public long OPIPID { get; set; }
    }
    public class TestB
    {
        public long OTReservationId { get; set; }
        public DateTime OTReservationDate { get; set; }
        public DateTime OTReservationTime { get; set; }
        public string OTReservationNo { get; set; }
        public long OTRequestId { get; set; }
        public long OPIPID { get; set; }
    }
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
        public virtual async Task<Tuple<List<TestA>, List<TestB>>> GetListMultiple()
        {
            DatabaseHelper sql = new();
            SqlParameter[] para = Array.Empty<SqlParameter>();

            return await sql.Get2ResultsFromSp<TestA, TestB>("ps_getMultipleTabForReport", para);
        }
    }
}
