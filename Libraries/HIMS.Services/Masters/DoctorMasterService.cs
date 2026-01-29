using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System.Data;
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
        public virtual async Task<IPagedList<DoctorMasterListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DoctorMasterListDto>(model, "ps_Rtrv_DoctorMasterList_Pagi");
        }
        public virtual async Task<IPagedList<DoctorChargesDetailListDto>> ListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DoctorChargesDetailListDto>(model, "ps_DoctorChargesDetailList");
        }
        public virtual async Task<IPagedList<DoctorSignpageListDto>> ListAsyncs(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DoctorSignpageListDto>(model, "ps_rtrv_Signpagelist");
        }
        public virtual async Task<IPagedList<DoctorQualificationDetailsListDto>> ListAsyncQ(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DoctorQualificationDetailsListDto>(model, "ps_DoctorQualificationDetails");
        }
        public virtual async Task<IPagedList<DoctorLeaveDetailsListDto>> ListAsyncL(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DoctorLeaveDetailsListDto>(model, "ps_DoctorLeaveDetailsList");
        }
        //public virtual async Task<IPagedList<DoctorShareListDto>> GetList(GridRequestModel model)
        //{
        //    return await DatabaseHelper.GetGridDataBySp<DoctorShareListDto>(model, "PS_Rtrv_BillListForDocShr");
        //}
        //public virtual async Task<IPagedList<DoctorShareLbyNameListDto>> GetList1(GridRequestModel model)
        //{
        //    return await DatabaseHelper.GetGridDataBySp<DoctorShareLbyNameListDto>(model, "PS_m_Rtrv_DoctorShareList_by_Name");
        //}
        public virtual async Task<List<ContantListDto>> ConstantListAsync(string ConstantType)
        {
            var query = _context.MConstants.AsQueryable();

            if (!string.IsNullOrEmpty(ConstantType))
            {
                string lowered = ConstantType.ToLower();
                query = query.Where(d => d.ConstantType != null && d.ConstantType.ToLower().Contains(lowered));
            }

            var data = await query
                .OrderBy(d => d.ConstantId)
                .Select(d => new ContantListDto
                {
                    ConstantId = d.ConstantId,
                    ConstantType = d.ConstantType,
                })
                .Take(50)
                .ToListAsync();

            return data;
        }

        //public virtual async Task<List<BedmasterDto>> GetBedmaster(int RoomId)
        //{
        //    return await _context.Bedmasters
        //                         .Where(x => x.RoomId == RoomId)
        //                         .Select(s => new BedmasterDto
        //                         {
        //                             BedId = s.BedId,
        //                             BedName = s.BedName
        //                         })
        //                         .Take(50)
        //                         .ToListAsync();
        //}



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
                // Delete details table realted records
                var lst = await _context.MDoctorDepartmentDets.Where(x => x.DoctorId == objDoctorMaster.DoctorId).ToListAsync();
                if (lst.Count > 0)
                {
                    _context.MDoctorDepartmentDets.RemoveRange(lst);
                }
                // Delete details table realted records
                var lsts = await _context.MDoctorQualificationDetails.Where(x => x.DoctorId == objDoctorMaster.DoctorId).ToListAsync();
                if (lst.Count > 0)
                {
                    _context.MDoctorQualificationDetails.RemoveRange(lsts);
                }
                // Delete details table realted records
                var lstd = await _context.MDoctorExperienceDetails.Where(x => x.DoctorId == objDoctorMaster.DoctorId).ToListAsync();
                if (lst.Count > 0)
                {
                    _context.MDoctorExperienceDetails.RemoveRange(lstd);
                }
                // Delete details table realted records
                var lstq = await _context.MDoctorScheduleDetails.Where(x => x.DoctorId == objDoctorMaster.DoctorId).ToListAsync();
                if (lst.Count > 0)
                {
                    _context.MDoctorScheduleDetails.RemoveRange(lstq);
                }
                // Delete details table realted records
                var lstp = await _context.MDoctorChargesDetails.Where(x => x.DoctorId == objDoctorMaster.DoctorId).ToListAsync();
                if (lst.Count > 0)
                {
                    _context.MDoctorChargesDetails.RemoveRange(lstp);
                }
                // Delete details table realted records
                var lsty = await _context.MDoctorLeaveDetails.Where(x => x.DoctorId == objDoctorMaster.DoctorId).ToListAsync();
                if (lst.Count > 0)
                {
                    _context.MDoctorLeaveDetails.RemoveRange(lsty);
                }
                // Delete details table realted records
                var lstz = await _context.MDoctorSignPageDetails.Where(x => x.DoctorId == objDoctorMaster.DoctorId).ToListAsync();
                if (lst.Count > 0)
                {
                    _context.MDoctorSignPageDetails.RemoveRange(lstz);
                }
                await _context.SaveChangesAsync();
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
            return await _context.DoctorMasters.Include(x => x.MDoctorDepartmentDets).Where(y => y.IsConsultant.Value && y.MDoctorDepartmentDets.Any(z => z.DepartmentId == DeptId)).ToListAsync();
        }

        public async Task<List<DoctorMaster>> GetDoctorsByDocType(int DocTypeId)
        {
            return await _context.DoctorMasters.Where(x => x.IsConsultant == true && x.DoctorTypeId == DocTypeId).ToListAsync();
        }
        //public async Task<List<MConsentMaster>> GetDoctorsByConsentType(int DeptId)
        //{
        //    return await _context.MConsentMasters.Where(x => x.DepartmentId == DeptId).ToListAsync();

        //}




        public async Task<List<DoctorMaster>> GetDoctorWithDepartment()
        {
            var result = await _context.DoctorMasters
    .Select(x => new
    {
        Doctor = x,
        DepartmentNames = x.MDoctorDepartmentDets
                          .Select(y => y.Department.DepartmentName)
                          .ToList()
    }).Where(y => y.Doctor.IsConsultant.Value)
    .ToListAsync();
            return result.Select(x => new DoctorMaster
            {
                DoctorId = x.Doctor.DoctorId,
                FirstName = x.Doctor.FirstName,
                MiddleName = x.Doctor.MiddleName,
                LastName = x.Doctor.LastName,
                DeptNames = string.Join(',', x.DepartmentNames)
            }).ToList();
            //return await _context.DoctorMasters.Include(x => x.MDoctorDepartmentDets.Select(y=>y.Department.DepartmentName)).Where(y => y.IsConsultant.Value).ToListAsync();
        }

        public virtual async Task<List<DoctorMaster>> SearchDoctor(string str)
        {
            return await this._context.DoctorMasters.Where(x => (x.FirstName + " " + x.LastName).ToLower().Contains(str)).Take(25).ToListAsync();
        }
        public virtual async Task InsertAsync(MDoctorExecutiveLinkInfo ObjMDoctorExecutiveLinkInfo, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            string[] DEntity = { "Id", "DoctorId", "EmployeId", "CreatedBy" };
            var entity = ObjMDoctorExecutiveLinkInfo.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!DEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            string VID = odal.ExecuteNonQuery("ps_Insert_DoctorExecutiveLinkInfo", CommandType.StoredProcedure, "Id", entity);
            ObjMDoctorExecutiveLinkInfo.Id = Convert.ToInt32(VID);
            await _context.LogProcedureExecution(entity, nameof(MDoctorExecutiveLinkInfo), (int)ObjMDoctorExecutiveLinkInfo.Id, Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);


        }
        public virtual async Task UpdateAsync(MDoctorExecutiveLinkInfo ObjMDoctorExecutiveLinkInfo, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            string[] DEntity = { "Id", "DoctorId", "EmployeId", "ModifiedBy" };
            var entity = ObjMDoctorExecutiveLinkInfo.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!DEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_Update_DoctorExecutiveLinkInfo", CommandType.StoredProcedure, entity);
            await _context.LogProcedureExecution(entity, nameof(MDoctorExecutiveLinkInfo), (int)ObjMDoctorExecutiveLinkInfo.Id, Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);


        }

    }
}