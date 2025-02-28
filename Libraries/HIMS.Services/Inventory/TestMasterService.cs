using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
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
        public virtual async Task<IPagedList<TestMasterDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<TestMasterDto>(model, "Rtrv_PathTestForUpdate");
        }
        public virtual async Task InsertAsyncSP(MPathTestMaster objTest, int UserId, string Username)
        {
            try
            {
                //Add header table records
                DatabaseHelper odal = new();
                string[] rEntity = { "UpdatedBy", "CreatedBy", "CreatedDate", "TestTime", "TestDate", "ModifiedBy", "ModifiedDate", "MPathTemplateDetails", "MPathTestDetailMasters" };
                var entity = objTest.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                string TestId = odal.ExecuteNonQuery("insert_PathologyTestMaster_1", CommandType.StoredProcedure, "TestId", entity);
                objTest.TestId = Convert.ToInt32(TestId);
             
                // Add sub table records
                if (objTest.IsTemplateTest == 1)
                {
                    foreach (var item in objTest.MPathTemplateDetails)
                    {
                        item.TestId = objTest.TestId;
                    }
                    _context.MPathTemplateDetails.AddRange(objTest.MPathTemplateDetails);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    foreach (var item in objTest.MPathTestDetailMasters)
                    {
                        item.TestId = objTest.TestId;
                    }
                    _context.MPathTestDetailMasters.AddRange(objTest.MPathTestDetailMasters);
                    await _context.SaveChangesAsync();
                }
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

                var lst1 = await _context.MPathTestDetailMasters.Where(x => x.TestId == objTest.TestId).ToListAsync();
                if (lst1.Count > 0)
                {
                    _context.MPathTestDetailMasters.RemoveRange(lst1);
                }
                await _context.SaveChangesAsync();
            }
        }


        public virtual async Task InsertAsync(MPathTestMaster objTest, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // remove conditional records
                if (objTest.IsTemplateTest == 1)
                    objTest.MPathTestDetailMasters = null;
                else
                    objTest.MPathTemplateDetails = null;
                // Add header table records
                _context.MPathTestMasters.Add(objTest);
                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync(MPathTestMaster objTest, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.MPathTestMasters.Update(objTest);
                _context.Entry(objTest).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        //public virtual async Task CancelAsync(MPathTestMaster objTest, int CurrentUserId, string CurrentUserName)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //    {
        //        // Update header table records
        //        MPathTestMaster objPathology = await _context.MPathTestMasters.FindAsync(objTest.TestId);
        //        objPathology.IsActive = false;
        //        objPathology.CreatedDate = objTest.CreatedDate;
        //        objPathology.ModifiedBy = objTest.ModifiedBy;
        //        _context.MPathTestMasters.Update(objPathology);
        //        _context.Entry(objPathology).State = EntityState.Modified;
        //        await _context.SaveChangesAsync();

        //        scope.Complete();
        //    }
        //}
    }
}


