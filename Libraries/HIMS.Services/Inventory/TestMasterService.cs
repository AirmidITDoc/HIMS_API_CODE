using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.Inventory
{
    public class TestMasterService : ITestMasterServices
    {
        private readonly Data.Models.HIMSDbContext _context;
        public TestMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsyncSP(MPathTestMaster objTest, int UserId, string Username)
        {
            try
            {
                // //Add header table records
                DatabaseHelper odal = new();


                string[] rEntity = { "IsActive", "UpdatedBy", "CreatedBy", "CreatedDate", "TestTime", "TestDate", "ModifiedBy", "ModifiedDate ", "MPathTemplateDetail" };
                var entity = objTest.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                string TestId = odal.ExecuteNonQuery("insert_PathologyTestMaster_1", CommandType.StoredProcedure, "TestId", entity);
                objTest.TestId = Convert.ToInt32(TestId);

                // Add details table records
                foreach (var objItem in objTest.MPathTemplateDetail)
                {
                    objItem.TestId = objTest.TestId;
                }
                _context.MPathTemplateDetails.AddRange(objTest.MPathTemplateDetail);
                await _context.SaveChangesAsync(UserId, Username);
            }
            catch (Exception)
            {
                // Delete header table realted records
                MPathTestMaster? objtest = await _context.MPathTestMasters.FindAsync(objTest.TestId);
                if (objtest != null)
                {
                    _context.MPathTestMasters.Remove(objtest);
                }

                // Delete details table realted records
                var lst = await _context.MPathTemplateDetails.Where(x => x.TestId == objTest.TestId).ToListAsync();
                if (lst.Count > 0)
                {
                    _context.MPathTemplateDetails.RemoveRange(lst);
                }
                await _context.SaveChangesAsync();
            }
        }


        public virtual async Task InsertAsync(MPathTestMaster objTest, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update store table records
                MPathTemplateDetail1 TestInfo = await _context.MPathTemplateDetails1.FirstOrDefaultAsync(x => x.TestId == objTest.TestId);
                if (TestInfo != null)
                {
                    //TestInfo.TestId = Convert.ToString(Convert.ToInt32(TestInfo.TestId ?? "0") + 1);
                    _context.MPathTemplateDetails1.Update(TestInfo);
                    await _context.SaveChangesAsync();
                }

                // Add header & detail table records

                //objTest.TestId = (TestInfo != null) ? TestInfo.TestId : "0";
                _context.MPathTestMasters.Add(objTest);
                await _context.SaveChangesAsync();

                scope.Complete();
            }






        }
    }
}


