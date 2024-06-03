using HIMS.Core.Domain.Grid;
using HIMS.Core.Domain.Logging;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Infrastructure;
using HIMS.Data.Models;
using HIMS.Services.Permissions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Common
{
    public class CommonService : ICommonService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public CommonService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public dynamic GetDataSetByProc(string mode, ListRequestModel model)
        {
            DatabaseHelper odal = new();
            CommonConfig commonConfig = new CommonConfig(mode, model);
            DataSet ds = odal.FetchDataSetBySP(commonConfig.comonParams.Sp_Name, commonConfig.comonParams.Sql_Para);
            dynamic result = new ExpandoObject();
            foreach (DataTable dt in ds.Tables)
            {
                var dict = (IDictionary<string, object>)result;
                if (dt.Rows.Count > 0)
                    dict[dt.TableName] = dt.ToDynamic();
            }
            return result;
        }
    }
}
