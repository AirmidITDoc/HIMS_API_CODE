using HIMS.Core.Domain.Logging;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Users
{
    public class SalesService : ISalesService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public SalesService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsync(TSalesHeader objSales, int UserId, string Username)
        {
            objSales.RoundOff = objSales.PaidAmount - objSales.NetAmount;
            _context.TSalesHeaders.Add(objSales);
            await _context.SaveChangesAsync();
            DatabaseHelper odal = new();
            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@SalesId", objSales.SalesId);
            para[1] = new SqlParameter("@StoreId", objSales.StoreId);
            odal.ExecuteScalar("UpdateBillNo", CommandType.StoredProcedure, para);
        }
    }
}
