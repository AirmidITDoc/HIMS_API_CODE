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
using HIMS.Data.DTO.IPPatient;
using SkiaSharp;
using static LinqToDB.SqlQuery.SqlPredicate;

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
            return await DatabaseHelper.GetGridDataBySp<VisitDetailListDto>(model, "ps_Rtrv_VisitDetailsList_1_Pagi");
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
                //    List<VisitDetail> objVisit = await _context.VisitDetails.Where(x => x.VisitId == objVisitDetail.VisitId && x.VisitDate == DateTime.Now).ToListAsync();
                //    foreach (var item in objVisit)
                //    {
                //        TTokenNumberWithDoctorWise objToken = await _context.TTokenNumberWithDoctorWises.FirstOrDefaultAsync(x => x.VisitDate == DateTime.Now);
                //        if (objToken != null)
                //        {
                //            objToken.TokenNo = Convert.ToInt32(objToken.TokenNo ?? 0) + 1;

                //            TTokenNumberWithDoctorWise objCurrentToken = new()
                //            {
                //                TokenNo = objToken.TokenNo,
                //                VisitDate = item.VisitDate,
                //                VisitId = item.VisitId,
                //                DoctorId = item.ConsultantDocId,
                //                IsStatus = false
                //            };
                //            _context.TTokenNumberWithDoctorWises.Add(objCurrentToken);
                //            await _context.SaveChangesAsync();
                //        }
                //    }

                //    scope.Complete();
                //}


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
            string RegId = odal.ExecuteNonQuery("ps_insert_Registration_1", CommandType.StoredProcedure, "RegId", entity);
            objRegistration.RegId = Convert.ToInt32(RegId);
            objVisitDetail.RegId = Convert.ToInt32(RegId);

            string[] rVisitEntity = { "Opdno", "IsMark", "Comments", "IsXray", "Height", "Pweight", "Bmi", "Bsl", "SpO2", "Temp", "Pulse", "Bp" };
            var visitentity = objVisitDetail.ToDictionary();
            foreach (var rProperty in rVisitEntity)
            {
                visitentity.Remove(rProperty);
            }
            string VisitId = odal.ExecuteNonQuery("ps_insert_VisitDetails_1", CommandType.StoredProcedure, "VisitId", visitentity);
            objVisitDetail.VisitId = Convert.ToInt32(VisitId);

            var tokenObj = new
            {
                VisitId = Convert.ToInt32(VisitId)
            };
            odal.ExecuteNonQuery("ps_Insert_TokenNumber_DoctorWise", CommandType.StoredProcedure, tokenObj.ToDictionary());

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
            odal.ExecuteNonQuery("ps_update_RegistrationForAppointment_1", CommandType.StoredProcedure, entity);
            objRegistration.RegId = Convert.ToInt32(objRegistration.RegId);
            objVisitDetail.RegId = Convert.ToInt32(objRegistration.RegId);

            string[] rVisitEntity = { "Opdno", "IsMark", "Comments", "IsXray","Height","Pweight","Bmi","Bsl","SpO2","Temp","Pulse","Bp" };
            var visitentity = objVisitDetail.ToDictionary();
            foreach (var rProperty in rVisitEntity)
            {
                visitentity.Remove(rProperty);
            }
            string VisitId = odal.ExecuteNonQuery("ps_insert_VisitDetails_1", CommandType.StoredProcedure, "VisitId", visitentity);
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
            return await DatabaseHelper.GetGridDataBySp<OPBillListDto>(model, "ps_Rtrv_BrowseOPDBill_Pagi");
        }

        public virtual async Task<IPagedList<OPPaymentListDto>> GeOpPaymentListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<OPPaymentListDto>(model, "ps_Rtrv_BrowseOPPaymentList");
        }

        public virtual async Task<IPagedList<OPRefundListDto>> GeOpRefundListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<OPRefundListDto>(model, "ps_Rtrv_BrowseOPDRefundBillList");
        }

        public virtual async Task<IPagedList<OPRegistrationList>> GeOPRgistrationListAsync(GridRequestModel model)
        {

            return await DatabaseHelper.GetGridDataBySp<OPRegistrationList>(model, "ps_Retrieve_RegistrationList");
        }

        public virtual async Task<IPagedList<PrevDrVisistListDto>> GeOPPreviousDrVisitListAsync(GridRequestModel model)
        {

            return await DatabaseHelper.GetGridDataBySp<PrevDrVisistListDto>(model, "ps_Rtrv_PreviousDoctorVisitList");
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

        public virtual async Task<List<VisitDetailsListSearchDto>> VisitDetailsListSearchDto(string Keyword)
        {
            var qry = from registration in _context.Registrations
                       join visitDetails in _context.VisitDetails on registration.RegId equals visitDetails.RegId
                       join tariffMaster in _context.TariffMasters on visitDetails.TariffId equals tariffMaster.TariffId
                       join departmentMaster in _context.MDepartmentMasters on visitDetails.DepartmentId equals departmentMaster.DepartmentId
                       join doctorMaster in _context.DoctorMasters on visitDetails.ConsultantDocId equals doctorMaster.DoctorId
                       join refDoctorMaster in _context.DoctorMasters on visitDetails.RefDocId equals refDoctorMaster.DoctorId into refDoctorGroup
                       from refDoctor in refDoctorGroup.DefaultIfEmpty()
                       join companyMaster in _context.CompanyMasters on visitDetails.CompanyId equals companyMaster.CompanyId into companyGroup
                       from company in companyGroup.DefaultIfEmpty()
                       where visitDetails.VisitDate == DateTime.Today
                          && (registration.FirstName + " " + registration.LastName).Contains(Keyword)
                          || registration.RegNo.Contains(Keyword)
                          || registration.MobileNo.Contains(Keyword)
                       orderby registration.FirstName

                       select new VisitDetailsListSearchDto
                       {
                           FirstName = registration.FirstName,
                           MiddleName = registration.MiddleName,
                           LastName = registration.LastName,
                           RegNo = registration.RegNo,
                           MobileNo = registration.MobileNo,
                           VisitId = visitDetails.VisitId,
                           RegId = visitDetails.RegId,
                           // VisitDate = visitDetails.VisitDate,
                           hospitalId = visitDetails.UnitId,
                           PatientTypeId = visitDetails.PatientTypeId,
                           ConsultantDocId = visitDetails.ConsultantDocId,
                           RefDocId = visitDetails.RefDocId,
                           OPDNo = visitDetails.Opdno,
                           TariffId = visitDetails.TariffId,
                           ClassId = visitDetails.ClassId,
                           TariffName = tariffMaster.TariffName,
                           CompanyId = visitDetails.CompanyId,
                           CompanyName = company != null ? company.CompanyName : string.Empty,
                           //AgeYear = registration.AgeYear,
                           //AgeMonth = registration.AgeMonth,
                           //AgeDay = registration.AgeDay,
                           DepartmentName = departmentMaster.DepartmentName,
                           RefDoctorName =   refDoctor != null ? "Dr. " + refDoctor.FirstName + " " + refDoctor.LastName : string.Empty,
                           DoctorName = "Dr. "+ doctorMaster.FirstName + " " + doctorMaster.LastName
                       };
            return await qry.Take(25).ToListAsync();

        }

        public virtual async Task<List<ServiceMasterDTO>> GetServiceListwithTraiff(int TariffId, int ClassId, string ServiceName)
        {
            var qry = from s in _context.ServiceMasters
                      join d in _context.ServiceDetails.Where(x => (x.TariffId == TariffId || TariffId == 0) && (x.ClassId == ClassId || ClassId == 0)) on s.ServiceId equals d.ServiceId
                      where s.IsActive.Value && (ServiceName == "" || s.ServiceName.Contains(ServiceName))
                      select new ServiceMasterDTO()
                      {
                          ServiceId = s.ServiceId,
                          GroupId = s.GroupId,
                          ServiceShortDesc = s.ServiceShortDesc,
                          ServiceName = s.ServiceName,
                          ClassRate = d.ClassRate ?? 0,
                          TariffId = d.TariffId ?? 0,
                          ClassId = d.ClassId ?? 0,
                          IsEditable = s.IsEditable,
                          CreditedtoDoctor = s.CreditedtoDoctor,
                          IsPathology = s.IsPathology,
                          IsRadiology = s.IsRadiology,
                          IsActive = s.IsActive,
                          PrintOrder = s.PrintOrder,
                          IsPackage = s.IsPackage,
                          DoctorId = s.DoctorId,
                          IsDocEditable = s.IsDocEditable
                      };
            return await qry.Take(50).ToListAsync();
        }

        public virtual async Task UpdateVitalAsync(VisitDetail objPara, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header table records
                //VisitDetail objVisit = await _context.VisitDetails.FindAsync(objPara.VisitId);
                //objVisit.Height = objPara?.Height;
                //objVisit.Pweight = objPara?.Pweight;
                //objVisit.Bmi = objPara?.Bmi;
                //objVisit.Bsl = objPara?.Bsl;
                //objVisit.SpO2 = objPara?.SpO2;
                //objVisit.Temp = objPara?.Temp;
                //objVisit.Pulse = objPara?.Pulse;
                //objVisit.Bp = objPara?.Bp;


                //_context.VisitDetails.Update(objVisit);
                //_context.Entry(objVisit).State = EntityState.Modified;
                //await _context.SaveChangesAsync();

                DatabaseHelper odal = new();
                string[] rEntity = { "RegId", "VisitDate", "VisitTime", "UnitId", "PatientTypeId", "ConsultantDocId", "RefDocId", "Opdno", "TariffId", "CompanyId", "AddedBy", "UpdatedBy", "IsCancelledBy", "IsCancelled", "IsCancelledDate", "ClassId", "DepartmentId", "PatientOldNew", "FirstFollowupVisit", "AppPurposeId", "FollowupDate", "IsMark", "Comments", "IsXray", "CrossConsulFlag", "PhoneAppId" };
                var entity = objPara.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_update_vitalInformation", CommandType.StoredProcedure, entity);

                scope.Complete();
            }
        }
            public virtual async Task<VisitDetail> InsertAsyncSP(VisitDetail objCrossConsultation, int UserId, string Username)
            {
               DatabaseHelper odal = new();
               string[] rEntity = { "Height", "Pweight", "Bmi", "Bsl", "SpO2", "Temp", "Pulse", "Bp", "Opdno", "IsMark", "Comments", "IsXray" };
               var entity = objCrossConsultation.ToDictionary();
               foreach (var rProperty in rEntity)
               {
                entity.Remove(rProperty);
               }
               string VisitID = odal.ExecuteNonQuery("ps_insert_VisitDetails_1", CommandType.StoredProcedure, "VisitId", entity);
               objCrossConsultation.VisitId = Convert.ToInt32(VisitID);

               await _context.SaveChangesAsync(UserId, Username);

                return objCrossConsultation;

            }
        public virtual async Task UpdateAsync(VisitDetail ObjVisitDetail, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            var existing = await _context.VisitDetails.FirstOrDefaultAsync(x => x.VisitId == ObjVisitDetail.VisitId);

            //  Update only the required fields
            existing.ConStartTime = ObjVisitDetail.ConStartTime;
            await _context.SaveChangesAsync();
            scope.Complete();
        }

        public virtual async Task UpdateAsyncv(VisitDetail ObjVisitDetail, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            var existing = await _context.VisitDetails.FirstOrDefaultAsync(x => x.VisitId == ObjVisitDetail.VisitId);

            //  Update only the required fields
            existing.ConEndTime = ObjVisitDetail.ConEndTime;
            existing.CheckOutTime = ObjVisitDetail.CheckOutTime;
            await _context.SaveChangesAsync();
            scope.Complete();
        }
    }  
}





