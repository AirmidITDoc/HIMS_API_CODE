using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;


namespace HIMS.Services.Inventory
{
    public  class HSNCodeMasterService : IHSNCodeMasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public HSNCodeMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
       
        public List<HSNCodeMasterListDTO> SearchHSNCode(string HSNcode)
        {
            DatabaseHelper sql = new();

            SqlParameter[] para ={ new SqlParameter("@Keyword", HSNcode)};

            var data = sql.FetchListBySP<HSNCodeMasterListDTO>( "ps_HSNCodeSearch", para );

            return data;
        }

    }
}
