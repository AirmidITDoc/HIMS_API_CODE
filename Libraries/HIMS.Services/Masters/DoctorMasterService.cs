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
using LinqToDB;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Transactions;
using HIMS.Services.Inventory;
using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Transactions;

namespace HIMS.Services.Masters
{
    public class DoctorMasterService : IDoctorMasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public DoctorMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }


        public virtual async Task InsertAsync(DoctorMaster objDoctor, List<MDoctorDepartmentDet> objDepartment, int CurrentUserId, string CurrentUserName)
        {
            
            {
                using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
                {
                    // Update store table records
                    DoctorMaster StoreInfo = await _context.DoctorMasters.FirstOrDefaultAsync(x => x.DoctorId == objDoctor.DoctorId);
                    StoreInfo.RegNo = Convert.ToString(Convert.ToInt32(StoreInfo.RegNo) + 1);
                    _context.DoctorMasters.Update(StoreInfo);
                    await _context.SaveChangesAsync();

                    // Add header & detail table records
                    objDoctor.RegNo = StoreInfo.RegNo;
                    _context.DoctorMasters.Add(objDoctor);
                    await _context.SaveChangesAsync();

                    // Update item master table records
                    _context.DoctorMasters.UpdateRange(objDoctor);
                    await _context.SaveChangesAsync();

                    scope.Complete();
                }

            }
        }

        public Task InsertAsyncSP(DoctorMaster objDoctor, List<MDoctorDepartmentDet> objDepartment, int CurrentUserId, string CurrentUserName)
        {
            throw new NotImplementedException();
        }

        public Task InsertWithPOAsync(DoctorMaster objDoctor, List<MDoctorDepartmentDet> objDepartment, int CurrentUserId, string CurrentUserName)
        {
            throw new NotImplementedException();
        }

        public virtual async Task UpdateAsync(DoctorMaster objDoctor, List<MDoctorDepartmentDet> objDepartment, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Delete details table realted records
                var lst = await _context.DoctorMasters.Where(x => x.DoctorId == objDoctor.DoctorId).ToListAsync();
                _context.DoctorMasters.RemoveRange(lst);

                // Update header & detail table records
                _context.DoctorMasters.Update(objDoctor);
                _context.Entry(objDoctor).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        public Task UpdateWithPOAsync(DoctorMaster objDoctor, List<MDoctorDepartmentDet> objDepartment, int CurrentUserId, string CurrentUserName)
        {
            throw new NotImplementedException();
        }
    }
}


