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
using HIMS.Services.Inventory;
using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Transactions;
using LinqToDB;
using Aspose.Cells.Charts;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Master;

namespace HIMS.Services.Masters
{
    public class DoctorMasterService : IDoctorMasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public DoctorMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<DoctoreMasterDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DoctoreMasterDto>(model, "m_Rtrv_DoctorMasterList_Pagi");
        }
        public virtual async Task InsertAsyncSP(DoctorMaster objDoctor, int UserId, string Username)
        {

            try
            
            {
                //Add header table records
                DatabaseHelper odal = new();
                string[] rEntity = { "RefDocHospitalName", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "MDoctorDepartmentDets" };
                var entity = objDoctor.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                string DoctorId = odal.ExecuteNonQuery("Insert_DoctorMaster_1", CommandType.StoredProcedure, "DoctorId", entity);
                objDoctor.DoctorId = Convert.ToInt32(DoctorId);

                // Add details table records
                foreach (var objItem in objDoctor.MDoctorDepartmentDets)
                {
                    objItem.DoctorId = objDoctor.DoctorId;
                }
                _context.MDoctorDepartmentDets.AddRange(objDoctor.MDoctorDepartmentDets);
                await _context.SaveChangesAsync();
            

            }
            catch (Exception)
            {
                // Delete header table realted records
                DoctorMaster? objdoctor = await _context.DoctorMasters.FindAsync(objDoctor.DoctorId);
                if (objDoctor != null)
                {
                    _context.DoctorMasters.Remove(objdoctor);
                }

                // Delete details table realted records
                var lst = await _context.MDoctorDepartmentDets.Where(x => x.DoctorId == objDoctor.DoctorId).ToListAsync();
                if (lst.Count > 0)
                {
                    _context.MDoctorDepartmentDets.RemoveRange(lst);
                }
                await _context.SaveChangesAsync();

               
            }
        }
        public virtual async Task InsertAsync(DoctorMaster objDoctor, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.DoctorMasters.Add(objDoctor);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        public virtual async Task UpdateAsync(DoctorMaster objDoctor, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.DoctorMasters.Update(objDoctor);
                _context.Entry(objDoctor).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

    }
}