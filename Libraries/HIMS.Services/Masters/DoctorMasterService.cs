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
using System.Net.NetworkInformation;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.DTO.Administration;

namespace HIMS.Services.Masters
{
    public class DoctorMasterService : IDoctorMasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public DoctorMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<DoctorMaster>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DoctorMaster>(model, "m_Rtrv_DoctorMasterList_Pagi");
        }
        public virtual async Task<IPagedList<DoctorShareListDto>> GetList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DoctorShareListDto>(model, "PS_Rtrv_BillListForDocShr");
        }
        public virtual async Task<IPagedList<DoctorShareLbyNameListDto>> GetList1(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DoctorShareLbyNameListDto>(model, "PS_m_Rtrv_DoctorShareList_by_Name");
        }


        public virtual async Task<IPagedList<DoctorMaster>> GetAllPagedAsync(GridRequestModel objGrid)
        {
            var qry = from d in _context.DoctorMasters
                      join p in _context.DbPrefixMasters on d.PrefixId equals p.PrefixId
                      join t in _context.DoctorTypeMasters on d.DoctorTypeId equals t.Id
                      select new DoctorMaster()
                      {
                          DoctorId = d.DoctorId,
                          PrefixName = p.PrefixName,
                          FirstName = d.FirstName,
                          MiddleName = d.MiddleName,
                          LastName = d.LastName,
                          DateofBirth = d.DateofBirth,
                          Address = d.Address,
                          City = d.City,
                          Pin = d.Pin,
                          Phone = d.Phone,
                          Mobile = d.Mobile,
                          Education = d.Education,
                          IsConsultant = d.IsConsultant,
                          IsRefDoc = d.IsRefDoc,
                          DoctorTypeName = t.DoctorType,
                          AgeYear = d.AgeYear,
                          AgeMonth = d.AgeMonth,
                          AgeDay = d.AgeDay,
                          PassportNo = d.PassportNo,
                          Esino = d.Esino,
                          RegNo = d.RegNo,
                          RegDate = d.RegDate,
                          MahRegDate = d.MahRegDate,
                          MahRegNo = d.MahRegNo,
                          RefDocHospitalName = d.RefDocHospitalName,
                          IsInHouseDoctor = d.IsInHouseDoctor,
                          PanCardNo = d.PanCardNo,
                          AadharCardNo = d.AadharCardNo
                      };
            return await qry.BuildPredicate(objGrid);
        }
        public virtual async Task<DoctorMaster> GetById(int Id)
        {
            return await this._context.DoctorMasters.Include(x => x.MDoctorDepartmentDets).FirstOrDefaultAsync(x => x.DoctorId == Id);
        }
        public virtual async Task InsertAsyncSP(DoctorMaster objDoctorMaster, int UserId, string Username)
        {

            try
            {
                //Add header table records
                DatabaseHelper odal = new();
                string[] rEntity = { "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" };
                var entity = objDoctorMaster.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                string DoctorId = odal.ExecuteNonQuery("m_insert_DoctorMaster_1", CommandType.StoredProcedure, "DoctorId", entity);
                objDoctorMaster.DoctorId = Convert.ToInt32(DoctorId);

                // Add details table records
                foreach (var objAssign in objDoctorMaster.MDoctorDepartmentDets)
                {
                    objAssign.DoctorId = objDoctorMaster.DoctorId;
                }
                _context.MDoctorDepartmentDets.AddRange(objDoctorMaster.MDoctorDepartmentDets);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // Delete header table realted records
                DoctorMaster? objSup = await _context.DoctorMasters.FindAsync(objDoctorMaster.DoctorId);
                if (objSup != null)
                {
                    _context.DoctorMasters.Remove(objSup);
                }

                // Delete details table realted records
                var lst = await _context.MDoctorDepartmentDets.Where(x => x.DoctorId == objDoctorMaster.DoctorId).ToListAsync();
                if (lst.Count > 0)
                {
                    _context.MDoctorDepartmentDets.RemoveRange(lst);
                }
                await _context.SaveChangesAsync();
            }
        }


        public virtual async Task InsertAsync(DoctorMaster objDoctorMaster, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.DoctorMasters.Add(objDoctorMaster);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        public virtual async Task UpdateAsync(DoctorMaster objDoctorMaster, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.DoctorMasters.Update(objDoctorMaster);
                _context.Entry(objDoctorMaster).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        public Task<IPagedList<LvwDoctorMasterList>> GetListAsync1(GridRequestModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DoctorMaster>> GetDoctorsByDepartment(int DeptId)
        {
            return await _context.DoctorMasters.Include(x => x.MDoctorDepartmentDets).Where(y => y.MDoctorDepartmentDets.Any(z => z.DepartmentId == DeptId)).ToListAsync();
        }

        public virtual async Task<List<DoctorMaster>> SearchDoctor(string str)
        {
            return await this._context.DoctorMasters.Where(x => (x.FirstName + " " + x.LastName).ToLower().Contains(str)).Take(25).ToListAsync();
        }
    }
}