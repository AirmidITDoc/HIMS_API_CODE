using HIMS.Data.DataProviders;    
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.Masters
{
    public class DoctorShareMasterService : IDoctorShareMasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public DoctorShareMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        
        public virtual async Task InsertAsync(MDoctorPerMaster objMDoctorPerMaster, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.MDoctorPerMasters.Add(objMDoctorPerMaster);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        public virtual async Task UpdateAsync(MDoctorPerMaster objMDoctorPerMaster, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.MDoctorPerMasters.Update(objMDoctorPerMaster);
                _context.Entry(objMDoctorPerMaster).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        //public virtual async Task UpdateAsync(MDoctorPerMaster objMDoctorPerMaster, int userid, string username)
        //{
        //    DatabaseHelper odal = new();
        //    string[] rentity = { };
        //    var entity = objMDoctorPerMaster.ToDictionary();
        //    foreach (var rproperty in rentity)
        //    {
        //        entity.Remove(rproperty);
        //    }

        //    odal.ExecuteNonQuery("update_doctorsharemaster_1", CommandType.StoredProcedure, entity);

        //    await _context.SaveChangesAsync(userid, username);
        //     //throw new notimplementedexception();
        //}


    }
}
