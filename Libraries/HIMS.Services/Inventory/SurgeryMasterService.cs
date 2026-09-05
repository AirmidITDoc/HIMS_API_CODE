using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using LinqToDB.Common;
using Microsoft.Data.SqlClient;


namespace HIMS.Services.Inventory
{
    public class SurgeryMasterService : ISurgeryMasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public SurgeryMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
       
        public List<MOtSurgeryMaster> GetSurgeryNameBySurgeryType(int SiteDescId)
        {
            DatabaseHelper sql = new();

            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@SiteDescId", SiteDescId);

            return sql.FetchListByQuery<MOtSurgeryMaster>("EXEC ps_RrvGetSurgeryNameBySurgeryType @SiteDescId", para);
        }


    }
}
