using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Transactions;

namespace HIMS.Services.OutPatient
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
                //    List<VisitDetail> objVisit = await _context.VisitDetails.Where(x => x.VisitId == objVisitDetail.VisitId && x.VisitDate == AppTime.Now).ToListAsync();
                //    foreach (var item in objVisit)
                //    {
                //        TTokenNumberWithDoctorWise objToken = await _context.TTokenNumberWithDoctorWises.FirstOrDefaultAsync(x => x.VisitDate == AppTime.Now);
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
       
        public virtual async Task InsertAsyncSP(Registration objRegistration, VisitDetail objVisitDetail, TPatientPolicyInformation ObjTPatientPolicyInformation, int CurrentUserId, string CurrentUserName)
        {
            // OLD CODE With SP
            DatabaseHelper odal = new();
            string[] rEntity = { "RegDate", "RegTime", "PrefixId", "FirstName", "MiddleName", "LastName", "Address", "City", "PinNo", "DateofBirth", "Age", "GenderId", "PhoneNo", "MobileNo", "AddedBy", "AgeYear", "AgeMonth", "AgeDay", "CountryId", "StateId", "CityId", "MaritalStatusId", "IsCharity", "ReligionId", "AreaId", "IsSeniorCitizen", "AadharCardNo", "PanCardNo", "Photo", "EmgContactPersonName", "EmgRelationshipId", "EmgMobileNo", "EmgLandlineNo", "EngAddress", "EmgAadharCardNo", "EmgDrivingLicenceNo", "MedTourismPassportNo", "MedTourismVisaIssueDate", "MedTourismVisaValidityDate", "MedTourismNationalityId", "MedTourismCitizenship", "MedTourismPortOfEntry", "MedTourismDateOfEntry", "MedTourismResidentialAddress", "MedTourismOfficeWorkAddress","RegId" };
            var entity = objRegistration.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            string RegId = odal.ExecuteNonQuery("ps_insert_Registration_1", CommandType.StoredProcedure, "RegId", entity);
            objRegistration.RegId = Convert.ToInt32(RegId);
            objVisitDetail.RegId = Convert.ToInt32(RegId);
            await _context.LogProcedureExecution(entity, nameof(Registration), objRegistration.RegId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);

            string[] rVisitEntity = { "RegId", "VisitDate", "VisitTime", "UnitId", "PatientTypeId", "ConsultantDocId", "RefDocId", "TariffId", "CompanyId", "AddedBy", "UpdatedBy", "IsCancelledBy", "IsCancelled", "IsCancelledDate", "ClassId", "DepartmentId", "PatientOldNew", "FirstFollowupVisit", "AppPurposeId", "FollowupDate", "CrossConsulFlag", "PhoneAppId", "CampId", "CrossConsultantDrId", "VisitId" };
            var visitentity = objVisitDetail.ToDictionary();
            foreach (var rProperty in visitentity.Keys.ToList())
            {
                if (!rVisitEntity.Contains(rProperty))
                    visitentity.Remove(rProperty);
            }
            string VisitId = odal.ExecuteNonQuery("ps_insert_VisitDetails_1", CommandType.StoredProcedure, "VisitId", visitentity);
            objVisitDetail.VisitId = Convert.ToInt32(VisitId);
            await _context.LogProcedureExecution(visitentity, nameof(VisitDetail), objVisitDetail.VisitId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);

            string[] PatientPolicyEntity = { "PatientPolicyId", "Opipid", "Opiptype", "PolicyNo", "PolicyValidateDate", "ApprovedAmount", "CreatedBy", "IsActive" };
            ObjTPatientPolicyInformation.Opipid = Convert.ToInt32(VisitId);

            var Patiententity = ObjTPatientPolicyInformation.ToDictionary();
            foreach (var rProperty in Patiententity.Keys.ToList())
            {
                if (!PatientPolicyEntity.Contains(rProperty))
                    Patiententity.Remove(rProperty);
            }
            string PatientPolicyId = odal.ExecuteNonQuery("ps_insert_T_PatientPolicyInformation", CommandType.StoredProcedure, "PatientPolicyId", Patiententity);
            ObjTPatientPolicyInformation.PatientPolicyId = Convert.ToInt32(PatientPolicyId);

            await _context.LogProcedureExecution(Patiententity, nameof(TPatientPolicyInformation), ObjTPatientPolicyInformation.PatientPolicyId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);


            var tokenObj = new
            {
                VisitId = Convert.ToInt32(VisitId)
            };
            odal.ExecuteNonQuery("ps_Insert_TokenNumber_DoctorWise", CommandType.StoredProcedure, tokenObj.ToDictionary());
            await _context.LogProcedureExecution(tokenObj.ToDictionary(), nameof(VisitDetail), objVisitDetail.VisitId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);
        }

        public virtual async Task UpdateAsyncSP(VisitDetail objVisitDetail, TPatientPolicyInformation ObjTPatientPolicyInformation, int CurrentUserId, string CurrentUserName)
        {

            // OLD CODE With SP
            DatabaseHelper odal = new();
            string[] rVisitEntity = { "RegId", "VisitDate", "VisitTime", "UnitId", "PatientTypeId", "ConsultantDocId", "RefDocId", "TariffId", "CompanyId", "AddedBy", "UpdatedBy", "IsCancelledBy", "IsCancelled", "IsCancelledDate", "ClassId", "DepartmentId", "PatientOldNew", "FirstFollowupVisit", "AppPurposeId", "FollowupDate", "CrossConsulFlag", "PhoneAppId", "CampId", "CrossConsultantDrId", "VisitId" };
            var visitentity = objVisitDetail.ToDictionary();
            foreach (var rProperty in visitentity.Keys.ToList())
            {
                if (!rVisitEntity.Contains(rProperty))
                    visitentity.Remove(rProperty);
            }
            string VisitId = odal.ExecuteNonQuery("ps_insert_VisitDetails_1", CommandType.StoredProcedure, "VisitId", visitentity);
            objVisitDetail.VisitId = Convert.ToInt32(VisitId);
            await _context.LogProcedureExecution(visitentity, nameof(VisitDetail), objVisitDetail.VisitId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);

            string[] PatientPolicyEntity = { "PatientPolicyId", "Opipid", "Opiptype", "PolicyNo", "PolicyValidateDate", "ApprovedAmount", "CreatedBy", "IsActive" };
           
            ObjTPatientPolicyInformation.Opipid = Convert.ToInt32(VisitId);
            var Patiententity = ObjTPatientPolicyInformation.ToDictionary();
            foreach (var rProperty in Patiententity.Keys.ToList())
            {
                if (!PatientPolicyEntity.Contains(rProperty))
                    Patiententity.Remove(rProperty);
            }
            string PatientPolicyId = odal.ExecuteNonQuery("ps_insert_T_PatientPolicyInformation", CommandType.StoredProcedure, "PatientPolicyId", Patiententity);
            ObjTPatientPolicyInformation.PatientPolicyId = Convert.ToInt32(PatientPolicyId);
            //objVisitDetail.VisitId = Convert.ToInt32(VisitId);


            await _context.LogProcedureExecution(Patiententity, nameof(TPatientPolicyInformation), ObjTPatientPolicyInformation.PatientPolicyId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);

            var tokenObj = new
            {
                VisitId = Convert.ToInt32(VisitId)
            };
            odal.ExecuteNonQuery("ps_Insert_TokenNumber_DoctorWise", CommandType.StoredProcedure, tokenObj.ToDictionary());
            await _context.LogProcedureExecution(tokenObj.ToDictionary(), nameof(VisitDetail), objVisitDetail.VisitId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);
        }


        public virtual async Task<IPagedList<OPBillListDto>> GetBillListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<OPBillListDto>(model, "ps_Rtrv_BrowseOPDBill_Pagi");
        }

        public virtual async Task<IPagedList<OPPaymentListDto>> GeOpPaymentListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<OPPaymentListDto>(model, "ps_Rtrv_BrowseOPPaymentList");
        }

        public virtual async Task<IPagedList<OPPaymentListDto>> GetPatientWisePaymentList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<OPPaymentListDto>(model, "ps_Rtrv_PatientWisePaymentList");
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

        public List<VisitDetailsListSearchDto> SearchPatient(string Keyword)
        {
            DatabaseHelper sql = new();
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@Keyword", Keyword);
            return sql.FetchListBySP<VisitDetailsListSearchDto>("ps_Rtrv_PatientVisitedListSearch", para);
        }

        public List<ServiceMasterDTO> SearchGetServiceListwithTraiff(int TariffId, int ClassId, string SrvcName )
        {
            DatabaseHelper sql = new();
            SqlParameter[] para = new SqlParameter[3];
           
            para[0] = new SqlParameter("@TariffId", TariffId);
            para[1] = new SqlParameter("@ClassId", ClassId);
            para[2] = new SqlParameter("@SrvcName", SrvcName);

            List<ServiceMasterDTO> lstServiceList = sql.FetchListBySP<ServiceMasterDTO>("ps_Rtrv_ServicesList", para);
            return lstServiceList;
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
                          RefDoctorName = refDoctor != null ? "Dr. " + refDoctor.FirstName + " " + refDoctor.LastName : string.Empty,
                          DoctorName = "Dr. " + doctorMaster.FirstName + " " + doctorMaster.LastName
                      };
            return await qry.Take(25).ToListAsync();

        }

        public virtual async Task<VisitDetailsListSearchDto> PatientByVisitId(long VisitId)
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
                         && visitDetails.VisitId == VisitId
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
                          DepartmentName = departmentMaster.DepartmentName,
                          RefDoctorName = refDoctor != null ? "Dr. " + refDoctor.FirstName + " " + refDoctor.LastName : string.Empty,
                          DoctorName = "Dr. " + doctorMaster.FirstName + " " + doctorMaster.LastName
                      };
            return await qry.FirstOrDefaultAsync();

        }

       

        public virtual async Task<List<ServiceMasterDTO>> GetServiceListwithTraiff(int TariffId, int ClassId, string ServiceName)
        {
            var qry = (from s in _context.ServiceMasters
                       join d in _context.ServiceDetails.Where(x => (x.TariffId == TariffId || TariffId == 0) && (x.ClassId == ClassId || ClassId == 0)) on s.ServiceId equals d.ServiceId
                       //on s.ServiceId equals d.ServiceId
                       join x in _context.ServiceWiseCompanyCodes on s.ServiceId equals x.ServiceId
                       where (s.IsActive == true)
                             && (ServiceName == "" || s.ServiceName.Contains(ServiceName))
                       select new ServiceMasterDTO()
                       {
                           ServiceId = s.ServiceId,
                           GroupId = s.GroupId,
                           ServiceShortDesc = s.ServiceShortDesc,
                           ServiceName = s.ServiceName,
                           //ClassRate = d.ClassRate ?? 0,
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
                           IsDocEditable = s.IsDocEditable,
                           CompanyCode = x.CompanyCode ?? "",
                           CompanyServicePrint = x.CompanyServicePrint ?? "",
                           IsInclusionOrExclusion = x.IsInclusionOrExclusion,
                           IsPathOutSource = s.IsPathOutSource
                       });
            var sql = qry.Take(50).ToQueryString();
            Console.WriteLine(sql);
            return await qry.Take(50).ToListAsync();

        }

        public virtual async Task  UpdateVital(VisitDetail objPara, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {

                DatabaseHelper odal = new();
                string[] rEntity = { "VisitId", "Height", "Pweight", "Bmi", "Bsl", "SpO2", "Temp", "Pulse", "Bp" };
                var entity = objPara.ToDictionary();
                foreach (var rProperty in entity.Keys.ToList())
                {
                    if (!rEntity.Contains(rProperty))
                        entity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_update_vitalInformation", CommandType.StoredProcedure, entity);
                await _context.LogProcedureExecution(entity, nameof(VisitDetail), objPara.VisitId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);


                scope.Complete();
            }
        }
        public virtual async Task InsertAsyncSP(VisitDetail objCrossConsultation, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "RegId", "VisitDate", "VisitTime", "UnitId", "PatientTypeId", "ConsultantDocId", "RefDocId", "TariffId", "CompanyId", "AddedBy", "UpdatedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "ClassId", "DepartmentId", "PatientOldNew", "FirstFollowupVisit", "AppPurposeId", "FollowupDate", "CrossConsulFlag", "PhoneAppId", "CampId", "CrossConsultantDrId", "VisitId" };
            var entity = objCrossConsultation.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            string VisitID = odal.ExecuteNonQuery("ps_insert_VisitDetails_1", CommandType.StoredProcedure, "VisitId", entity);
            objCrossConsultation.VisitId = Convert.ToInt32(VisitID);

            var tokenObj = new
            {
                VisitId = Convert.ToInt32(VisitID)
            };
            odal.ExecuteNonQuery("ps_Insert_TokenNumber_DoctorWise", CommandType.StoredProcedure, tokenObj.ToDictionary());
            await _context.LogProcedureExecution(tokenObj.ToDictionary(), nameof(VisitDetail), objCrossConsultation.VisitId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);
            await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);

        }
        public virtual async Task ConsultantDoctorUpdate(VisitDetail objVisitDetail, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "ConsultantDocId", "DepartmentId", "VisitId"};
            var entity = objVisitDetail.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_Update_ConsultationDoctor_Visit", CommandType.StoredProcedure, entity);
            await _context.LogProcedureExecution(entity, nameof(VisitDetail), objVisitDetail.VisitId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);
            await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);

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
            existing.IsMark = true;
            await _context.SaveChangesAsync();
            scope.Complete();
        }
        public virtual async Task RequestForOPTOIP(VisitDetail ObjVisitDetail, int CurrentUserId, string CurrentUserName)
        {
            //throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] BEntity = { "VisitId", "IsConvertRequestForIp" };
            var TEntity = ObjVisitDetail.ToDictionary();
            foreach (var rProperty in TEntity.Keys.ToList())
            {
                if (!BEntity.Contains(rProperty))
                    TEntity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("ps_RequestForOPTOIP", CommandType.StoredProcedure, TEntity);
            await _context.LogProcedureExecution(TEntity, nameof(VisitDetail), ObjVisitDetail.VisitId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);

        }
        public virtual async Task VistDateTimeUpdateAsync(VisitDetail ObjVisitDetail, int CurrentUserId, string CurrentUserName)
        {

            DatabaseHelper odal = new();
            string[] AEntity = { "VisitId", "VisitDate", "VisitTime" };
            var Rentity = ObjVisitDetail.ToDictionary();
            foreach (var rProperty in Rentity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    Rentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_Update_VisitDateTime", CommandType.StoredProcedure, Rentity);
            await _context.LogProcedureExecution(Rentity, nameof(VisitDetail), ObjVisitDetail.VisitId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);


        }
        public virtual async Task VisitUpdateAsync(VisitDetail ObjVisitDetail, int CurrentUserId, string CurrentUserName)
        {

            DatabaseHelper odal = new();
            string[] AEntity = { "VisitId", "PatientTypeId", "ConsultantDocId", "RefDocId", "TariffId", "CompanyId", "ClassId" };
            var Rentity = ObjVisitDetail.ToDictionary();
            foreach (var rProperty in Rentity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    Rentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_Update_VisitDetails", CommandType.StoredProcedure, Rentity);
            await _context.LogProcedureExecution(Rentity, nameof(VisitDetail), ObjVisitDetail.VisitId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);


        }


    }
}





























