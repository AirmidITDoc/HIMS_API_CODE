using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.Nursing
{
    public class PriscriptionService : IPriscriptionService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public PriscriptionService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
       
        public virtual async Task InsertAsyncSP(TIpprescriptionReturnH objIpprescriptionReturnH, int UserId, string Username)
        {
            try

        {
        //Add header table records
        DatabaseHelper odal = new();
        string[] rEntity = { " PresNo" };
        var entity = objIpprescriptionReturnH.ToDictionary();
        foreach (var rProperty in rEntity)
        {
            entity.Remove(rProperty);
        }
        string vPresReId = odal.ExecuteNonQuery("v_insert_T_IPPrescriptionReturnH_1", CommandType.StoredProcedure, "PresReId", entity);
                objIpprescriptionReturnH.PresReId = Convert.ToInt32(vPresReId);

        // Add details table records
        foreach (var objAssign in objIpprescriptionReturnH.TIpprescriptionReturnDs)
        {
            objAssign.PresReId = objIpprescriptionReturnH.PresReId;
        }
        _context.TIpprescriptionReturnDs.AddRange(objIpprescriptionReturnH.TIpprescriptionReturnDs);
        await _context.SaveChangesAsync();
        }
          catch (Exception)
            {
                // Delete header table realted records
            TIpprescriptionReturnH? objSup = await _context.TIpprescriptionReturnHs.FindAsync(objIpprescriptionReturnH.PresReId);
             if (objSup != null)
              {
             _context.TIpprescriptionReturnHs.Remove(objSup);
              }

        // Delete details table realted records
        var lst = await _context.TIpprescriptionReturnDs.Where(x => x.PresReId == objIpprescriptionReturnH.PresReId).ToListAsync();
        if (lst.Count > 0)
        {
            _context.TIpprescriptionReturnDs.RemoveRange(lst);
        }
        await _context.SaveChangesAsync();
           }
    
        }
        public virtual async Task InsertAsync(THlabRequest objTHlabRequest, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.THlabRequests.Add(objTHlabRequest);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
       }
    }
}
    

