﻿using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.Inventory
{
    public  class RadiologyTestService : IRadiologyTestService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public RadiologyTestService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsyncSP(MRadiologyTestMaster objRadio, int UserId, string Username)
        {
            try
            {
                //Add header table records
                DatabaseHelper odal = new();
                string[] rEntity = { " Addedby ", "Updatedby", "CreatedBy", "CreatedDate", "ModifiedBy", " ModifiedDate", "MRadiologyTemplateDetail" };
                var entity = objRadio.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                string VTestId = odal.ExecuteNonQuery("M_Insert_M_Radiology_TestMaster", CommandType.StoredProcedure, "TestId", entity);
                objRadio.TestId = Convert.ToInt32(VTestId);



                // Add details table records
                foreach (var objTemplate in objRadio.MRadiologyTemplateDetails)
                {
                    objTemplate.TestId = objRadio.TestId;
                }
                _context.MRadiologyTemplateDetails.AddRange(objRadio.MRadiologyTemplateDetails);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // Delete header table realted records
                MRadiologyTestMaster? objSup = await _context.MRadiologyTestMasters.FindAsync(objRadio.TestId);
                if (objSup != null)
                {
                    _context.MRadiologyTestMasters.Remove(objSup);
                }

                // Delete details table realted records
                var lst = await _context.MRadiologyTemplateDetails.Where(x => x.TestId == objRadio.TestId).ToListAsync();
                if (lst.Count > 0)
                {
                    _context.MRadiologyTemplateDetails.RemoveRange(lst);
                }
                await _context.SaveChangesAsync();
            }
        }
        public virtual async Task InsertAsync(MRadiologyTestMaster objRadio, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.MRadiologyTestMasters.Add(objRadio);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync(MRadiologyTestMaster objRadio, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.MRadiologyTestMasters.Update(objRadio);
                _context.Entry(objRadio).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        public virtual async Task CancelAsync(MRadiologyTestMaster objRadio, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header table records
                MRadiologyTestMaster objRadiology = await _context.MRadiologyTestMasters.FindAsync(objRadio.TestId);
                objRadiology.CreatedDate = objRadio.CreatedDate;
                objRadiology.ModifiedBy = objRadio.ModifiedBy;
                _context.MRadiologyTestMasters.Update(objRadiology);
                _context.Entry(objRadiology).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
       
        public virtual async Task<List<MRadiologyTestMaster>> GetAllRadiologyTest()
        {
            var query = from M in _context.MRadiologyTestMasters
                        where M.IsActive == true
                        orderby M.TestId
                        select M;

            return await query.ToListAsync();
        }
        //public virtual async Task<MRadiologyTestMaster> GetByIdRadiologyTest(long Id)
        //{
        //    var query = from M in _context.MRadiologyTestMasters
        //                where M.IsActive == true && M.TestId == Id
        //                orderby M.TestId
        //                select M;

        //    return await query;
        //}
    }
}

    
