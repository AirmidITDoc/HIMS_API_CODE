using HIMS.Data.DataProviders;
using System.Data;
using System.Transactions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using HIMS.Services.OutPatient;
using HIMS.Core.Domain.Grid;
using HIMS.Data.Extensions;
using System.Dynamic;
using Microsoft.Data.SqlClient;
using static LinqToDB.Common.Configuration;
using static LinqToDB.Reflection.Methods.LinqToDB.Insert;
using HIMS.Data.DTO.OPPatient;

namespace HIMS.Services.OPPatient
{
    public class VisitDetailsService : IVisitDetailsService
    {
        private readonly HIMSDbContext _context;
        public VisitDetailsService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<VisitDetailListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<VisitDetailListDto>(model,"m_Rtrv_VisitDetailsList_1_Pagi");
        }

        public virtual async Task CancelAsync(VisitDetail objVisitDetail, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header table records
                VisitDetail objVisit = await _context.VisitDetails.FindAsync(objVisitDetail.VisitId);
                objVisit.IsCancelled = objVisitDetail.IsCancelled;
                objVisit.IsCancelledBy = objVisitDetail.IsCancelledBy;
                objVisit.IsCancelledDate = objVisitDetail.IsCancelledDate;
                _context.VisitDetails.Update(objVisit);
                _context.Entry(objVisit).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task InsertAsync(Registration objRegistration, VisitDetail objVisitDetail, int CurrentUserId, string CurrentUserName)
        {
            //// NEW CODE With EDMX
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Add Registration table records
                _context.Registrations.Add(objRegistration);
                await _context.SaveChangesAsync();

                //// Update Registration table records
                //ConfigSetting objConfigRSetting = await _context.ConfigSettings.FindAsync(Convert.ToInt64(1));
                //objConfigRSetting.RegNo = Convert.ToString(Convert.ToInt32(objConfigRSetting.RegNo) + 1);
                //_context.ConfigSettings.Update(objConfigRSetting);
                //_context.Entry(objConfigRSetting).State = EntityState.Modified;
                //await _context.SaveChangesAsync();

                // Add VisitDetail table records
                objVisitDetail.RegId = objRegistration.RegId;
                _context.VisitDetails.Add(objVisitDetail);
                await _context.SaveChangesAsync();

                // Update VisitDetail table records
                ConfigSetting objConfigSetting = await _context.ConfigSettings.FindAsync(Convert.ToInt64(1));
                objConfigSetting.Opno = Convert.ToString(Convert.ToInt32(objConfigSetting.Opno) + 1);
                _context.ConfigSettings.Update(objConfigSetting);
                _context.Entry(objConfigSetting).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                // Add TokenNumber table records
                List<VisitDetail> objVisit = await _context.VisitDetails.Where(x => x.VisitId == objVisitDetail.VisitId && x.VisitDate == DateTime.Now).ToListAsync();
                foreach (var item in objVisit)
                {
                    TTokenNumberWithDoctorWise objToken = await _context.TTokenNumberWithDoctorWises.FirstOrDefaultAsync(x => x.VisitDate == DateTime.Now);
                    if (objToken != null)
                    {
                        objToken.TokenNo = Convert.ToInt32(objToken.TokenNo ?? 0) + 1;

                        TTokenNumberWithDoctorWise objCurrentToken = new()
                        {
                            TokenNo = objToken.TokenNo,
                            VisitDate = item.VisitDate,
                            VisitId = item.VisitId,
                            DoctorId = item.ConsultantDocId,
                            IsStatus = false
                        };
                        _context.TTokenNumberWithDoctorWises.Add(objCurrentToken);
                        await _context.SaveChangesAsync();
                    }
                }

                scope.Complete();
            }


        }





        public virtual async Task InsertAsyncSP(Registration objRegistration, VisitDetail objVisitDetail, int CurrentUserId, string CurrentUserName)
        {
            // OLD CODE With SP
            DatabaseHelper odal = new();
            string[] rEntity = { "RegNo", "UpdatedBy", "RegPrefix", "AnnualIncome", "IsIndientOrWeaker", "RationCardNo", "IsMember" };
            var entity = objRegistration.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string RegId = odal.ExecuteNonQuery("v_insert_Registration_1", CommandType.StoredProcedure, "RegId", entity);
            objRegistration.RegId = Convert.ToInt32(RegId);
            objVisitDetail.RegId = Convert.ToInt32(RegId);

            string[] rVisitEntity = { "Opdno", "IsMark", "Comments", "IsXray" };
            var visitentity = objVisitDetail.ToDictionary();
            foreach (var rProperty in rVisitEntity)
            {
                visitentity.Remove(rProperty);
            }
            string VisitId = odal.ExecuteNonQuery("v_insert_VisitDetails_1", CommandType.StoredProcedure, "VisitId", visitentity);
            objVisitDetail.VisitId = Convert.ToInt32(VisitId);

            var tokenObj = new
            {
                VisitId = Convert.ToInt32(VisitId)
            };
            odal.ExecuteNonQuery("v_Insert_TokenNumber_DoctorWise", CommandType.StoredProcedure, tokenObj.ToDictionary());

            // Add TokenNumber table records
            //List<VisitDetail> objVisit = await _context.VisitDetails.Where(x => x.VisitId == objVisitDetail.VisitId && x.VisitDate == DateTime.Now).ToListAsync();
            //foreach (var item in objVisit)
            //{
            //    TTokenNumberWithDoctorWise objToken = await _context.TTokenNumberWithDoctorWises.FirstOrDefaultAsync(x => x.VisitDate == DateTime.Now);
            //    if (objToken != null)
            //    {
            //        objToken.TokenNo = Convert.ToInt32(objToken.TokenNo ?? 0) + 1;

            //        TTokenNumberWithDoctorWise objCurrentToken = new()
            //        {
            //            TokenNo = objToken.TokenNo,
            //            VisitDate = item.VisitDate,
            //            VisitId = item.VisitId,
            //            DoctorId = item.ConsultantDocId,
            //            IsStatus = false
            //        };
            //        _context.TTokenNumberWithDoctorWises.Add(objCurrentToken);
            //        await _context.SaveChangesAsync();
            //    }
            //}
        }

        public virtual async Task UpdateAsyncSP(Registration objRegistration, VisitDetail objVisitDetail, int CurrentUserId, string CurrentUserName)
        {



            // OLD CODE With SP
            DatabaseHelper odal = new();
            string[] rEntity = { "RegNo", "RegPrefix", "AnnualIncome", "IsIndientOrWeaker", "RationCardNo", "IsMember", "ReligionId", "AreaId", "IsSeniorCitizen", "AddedBy", "RegDate", "RegTime" };
            var entity = objRegistration.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("m_update_RegistrationForAppointment_1", CommandType.StoredProcedure, entity);
            objRegistration.RegId = Convert.ToInt32(objRegistration.RegId);
            objVisitDetail.RegId = Convert.ToInt32(objRegistration.RegId);

            string[] rVisitEntity = { "Opdno", "IsMark", "Comments", "IsXray" };
            var visitentity = objVisitDetail.ToDictionary();
            foreach (var rProperty in rVisitEntity)
            {
                visitentity.Remove(rProperty);
            }
            string VisitId = odal.ExecuteNonQuery("v_insert_VisitDetails_1", CommandType.StoredProcedure, "VisitId", visitentity);
            objVisitDetail.VisitId = Convert.ToInt32(VisitId);

            //var tokenObj = new
            //{
            //    PatVisitID = Convert.ToInt32(VisitId)
            //};
            //odal.ExecuteNonQuery("m_Insert_TokenNumber_DoctorWise", CommandType.StoredProcedure, tokenObj.ToDictionary());

            // Add TokenNumber table records
            List<VisitDetail> objVisit = await _context.VisitDetails.Where(x => x.VisitId == objVisitDetail.VisitId && x.VisitDate == DateTime.Now).ToListAsync();
            foreach (var item in objVisit)
            {
                TTokenNumberWithDoctorWise objToken = await _context.TTokenNumberWithDoctorWises.FirstOrDefaultAsync(x => x.VisitDate == DateTime.Now);
                if (objToken != null)
                {
                    objToken.TokenNo = Convert.ToInt32(objToken.TokenNo ?? 0) + 1;

                    TTokenNumberWithDoctorWise objCurrentToken = new()
                    {
                        TokenNo = objToken.TokenNo,
                        VisitDate = item.VisitDate,
                        VisitId = item.VisitId,
                        DoctorId = item.ConsultantDocId,
                        IsStatus = false
                    };
                    _context.TTokenNumberWithDoctorWises.Add(objCurrentToken);
                    await _context.SaveChangesAsync();
                }
            }
        }

      
        public virtual async Task<IPagedList<OPBillListDto>> GetBillListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<OPBillListDto>(model, "m_Rtrv_BrowseOPDBill_Pagi");
        }

        public virtual async Task<IPagedList<OPPaymentListDto>> GeOpPaymentListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<OPPaymentListDto>(model, "m_Rtrv_BrowseOPPaymentList");
        }

        public virtual async Task<IPagedList<OPRefundListDto>> GeOpRefundListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<OPRefundListDto>(model, "m_Rtrv_BrowseOPDRefundBillList");
        }

        public virtual async Task<IPagedList<OPRegistrationList>> GeOPRgistrationListAsync(GridRequestModel model)
        {
           
            return await DatabaseHelper.GetGridDataBySp<OPRegistrationList>(model, "Retrieve_RegistrationList");
        }

        //public virtual async Task<IPagedList<OPPhoneAppointmentList>> GeOPPhoneAppListAsync(GridRequestModel model)
        //{
        //    return await DatabaseHelper.GetGridDataBySp<OPPhoneAppointmentList>(model,"Retrieve_PhoneAppList");
        //}

        public List<DeptDoctorListDoT> GetDoctor(int DepartementId)
        {
            DatabaseHelper sql = new();
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@DepartmentId", DepartementId);
            List<DeptDoctorListDoT> lstMenu = sql.FetchListByQuery<DeptDoctorListDoT>("SELECT  dbo.DoctorMaster.DoctorId, dbo.DoctorMaster.FirstName FROM     dbo.M_DoctorDepartmentDet INNER JOIN  dbo.DoctorMaster ON dbo.M_DoctorDepartmentDet.DoctorId = dbo.DoctorMaster.DoctorId  where dbo.M_DoctorDepartmentDet.DepartmentId=@DepartmentId", para);
            return lstMenu;
        }


        public virtual async Task<IPagedList<DeptDoctorListDoT>> GetListAsyncDoc(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DeptDoctorListDoT>(model, "ps_getDepartmentWiseDoctorList");
        }
    }
}
