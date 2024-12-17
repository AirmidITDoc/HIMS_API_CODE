using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Transactions;
using LinqToDB;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Data.Common;
using HIMS.Core.Domain.Grid;
using static LinqToDB.Sql;

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
