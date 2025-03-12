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

namespace HIMS.Services.OutPatient
{
    public  class PrescriptionOPTemplateService : IPrescriptionOPTemplateService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public PrescriptionOPTemplateService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsyncSP(MPresTemplateH ObjMPresTemplateH, MPresTemplateD ObjMPresTemplateD, int UserId, string Username)
        {
               //Add header table records
                DatabaseHelper odal = new();
                string[] rEntity = {"IsUpdatedBy", "CreatedBy", "ModifiedBy", "ModifiedDate", "CreatedDate",};
                var entity = ObjMPresTemplateH.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                string VPresId = odal.ExecuteNonQuery("insert_M_PresTemplateH_1", CommandType.StoredProcedure, "PresId", entity);
                ObjMPresTemplateH.PresId = Convert.ToInt32(VPresId);
                ObjMPresTemplateD.PresId = Convert.ToInt32(VPresId);


                string[] Entity = { "PresDetId"};
                var Dentity = ObjMPresTemplateD.ToDictionary();
                foreach (var rProperty in Entity)
                {
                Dentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("insert_M_PresTemplateD_1", CommandType.StoredProcedure, Dentity);

        }

        public virtual async Task InsertAsync(MPresTemplateH ObjMPresTemplateH, MPresTemplateD ObjMPresTemplateD, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.MPresTemplateHs.Add(ObjMPresTemplateH);
                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }

    }
}
