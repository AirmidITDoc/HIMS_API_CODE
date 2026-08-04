using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;


namespace HIMS.Services.Masters
{
    public  class AreaMasterService: IAreaMasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public AreaMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public List<AreaMasterDto> searchAreaMaster(string Keyword)
        {
            DatabaseHelper sql = new();
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@Keyword", Keyword);
            return sql.FetchListBySP<AreaMasterDto>("ps_Rtrv_AreaMaster", para);
        }

    }
}
