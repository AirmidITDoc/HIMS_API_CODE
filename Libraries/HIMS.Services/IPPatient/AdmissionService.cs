using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Transactions;

namespace HIMS.Services.IPPatient
{
    public class AdmissionService : IAdmissionService
    {
        private readonly HIMSDbContext _context;
        public AdmissionService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<AdmissionListDto>> GetAdmissionListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<AdmissionListDto>(model, "ps_rtrv_Admtd_Ptnt_Dtls");
        }
        public virtual async Task<IPagedList<RequestForIPListDto>> GetAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<RequestForIPListDto>(model, "ps_OPList_RequestForIP");
        }
        public virtual async Task<IPagedList<AdmissionListDto>> GetAdmissionDischargeListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<AdmissionListDto>(model, "ps_rtrv_AdmtdWithDischargeDate_Ptnt_Dtls");
        }
        public virtual async Task<List<Bedmaster>> GetBedmaster(int RoomId)
        {
            var qry = from s in _context.Bedmasters
              .Where(x => (x.RoomId == RoomId) && x.IsAvailible == true && x.IsActive == true)
                      select new Bedmaster()
                      {
                          BedId = s.BedId,
                          BedName = s.BedName,

                      };
            return await qry.Take(50).ToListAsync();
        }


      
        public virtual async Task InsertSP(Admission objAdmission, TPatientPolicyInformation ObjTPatientPolicyInformation, int CurrentUserId, string CurrentUserName)
        {

            // OLD CODE With SP
            DatabaseHelper odal = new();
            string[] rVisitEntity = { "RegId", "AdmissionDate", "AdmissionTime", "PatientTypeId", "HospitalId", "DocNameId", "RefDocNameId", "WardId", "BedId", "DischargeDate",
            "DischargeTime","IsDischarged","IsBillGenerated","CompanyId","TariffId","ClassId","DepartmentId","RelativeName","RelativeAddress","PhoneNo","MobileNo","RelationshipId","AddedBy","IsMlc",
            "MotherName","AdmittedDoctor1","AdmittedDoctor2","RefByTypeId","RefByName","SubTpaComId","PolicyNo","AprovAmount","CompDod","ConvertId","IsOpToIpconv","RefDoctorDept","AdmissionType","AdmissionId","Ischarity"};
            var admientity = objAdmission.ToDictionary();

            foreach (var rProperty in admientity.Keys.ToList())
            {
                if (!rVisitEntity.Contains(rProperty))
                    admientity.Remove(rProperty);
            }
            string VAdmissionId = odal.ExecuteNonQuery("ps_insert_Admission_1", CommandType.StoredProcedure, "AdmissionId", admientity);
            objAdmission.AdmissionId = Convert.ToInt32(VAdmissionId);
            //await _context.LogProcedureExecution(admientity, nameof(Admission), objAdmission.AdmissionId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);
            _ = Task.Run(() => _context.LogProcedureExecution(admientity, nameof(Admission), objAdmission.AdmissionId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName));

            string[] PatientPolicyEntity = { "PatientPolicyId", "Opipid", "Opiptype", "PolicyNo", "PolicyValidateDate", "ApprovedAmount", "CreatedBy", "IsActive" };
            ObjTPatientPolicyInformation.Opipid = Convert.ToInt32(VAdmissionId);

            var Patiententity = ObjTPatientPolicyInformation.ToDictionary();
            foreach (var rProperty in Patiententity.Keys.ToList())
            {
                if (!PatientPolicyEntity.Contains(rProperty))
                    Patiententity.Remove(rProperty);
            }
            string PatientPolicyId = odal.ExecuteNonQuery("ps_insert_T_PatientPolicyInformation", CommandType.StoredProcedure, "PatientPolicyId", Patiententity);
            ObjTPatientPolicyInformation.PatientPolicyId = Convert.ToInt32(PatientPolicyId);
            //await _context.LogProcedureExecution(Patiententity, nameof(TPatientPolicyInformation), ObjTPatientPolicyInformation.PatientPolicyId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);
            _ = Task.Run(() => _context.LogProcedureExecution(Patiententity, nameof(TPatientPolicyInformation), ObjTPatientPolicyInformation.PatientPolicyId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName));

            var tokenObj = new
            {
                BedId = Convert.ToInt32(objAdmission.BedId),
                RoomId = Convert.ToInt32(objAdmission.WardId)
            };
            odal.ExecuteNonQuery("ps_Update_AdmissionBedstatus", CommandType.StoredProcedure, tokenObj.ToDictionary());
            _ = Task.Run(() => _context.LogProcedureExecution(admientity, nameof(Admission), objAdmission.AdmissionId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName));

        }



        //UPDATE SHILPA 09-08-2025//
        public virtual async Task InsertRegSP(Registration ObjRegistration, Admission objAdmission, TPatientPolicyInformation ObjTPatientPolicyInformation, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "RegDate", "RegTime", "PrefixId", "FirstName", "MiddleName", "LastName", "Address", "City", "PinNo", "DateofBirth", "Age", "GenderId", "PhoneNo", "MobileNo", "AddedBy", "AgeYear",
                "AgeMonth", "AgeDay", "CountryId", "StateId", "CityId", "MaritalStatusId", "IsCharity", "ReligionId", "AreaId", "IsSeniorCitizen", "AadharCardNo", "PanCardNo", "Photo", "EmgContactPersonName", "EmgRelationshipId",
                "EmgMobileNo", "EmgLandlineNo", "EngAddress", "EmgAadharCardNo", "EmgDrivingLicenceNo", "MedTourismPassportNo", "MedTourismVisaIssueDate", "MedTourismVisaValidityDate", "MedTourismNationalityId", "MedTourismCitizenship",
                "MedTourismPortOfEntry", "MedTourismDateOfEntry", "MedTourismResidentialAddress", "MedTourismOfficeWorkAddress", "RegId" };

            var entity = ObjRegistration.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            string RegId = odal.ExecuteNonQuery("ps_insert_Registration_1", CommandType.StoredProcedure, "RegId", entity);
            ObjRegistration.RegId = Convert.ToInt64(RegId);
            objAdmission.RegId = Convert.ToInt64(RegId);
            //await _context.LogProcedureExecution(entity, nameof(Registration), ObjRegistration.RegId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);


            string[] rVisitEntity = { "RegId", "AdmissionDate", "AdmissionTime", "PatientTypeId", "HospitalId", "DocNameId", "RefDocNameId", "WardId", "BedId", "DischargeDate",
                "DischargeTime","IsDischarged","IsBillGenerated","CompanyId","TariffId","ClassId","DepartmentId","RelativeName","RelativeAddress","PhoneNo","MobileNo","RelationshipId","AddedBy","IsMlc",
                "MotherName","AdmittedDoctor1","AdmittedDoctor2","RefByTypeId","RefByName","SubTpaComId","PolicyNo","AprovAmount","CompDod","ConvertId","IsOpToIpconv","RefDoctorDept","AdmissionType","AdmissionId","Ischarity"};
            var admientity = objAdmission.ToDictionary();

            foreach (var rProperty in admientity.Keys.ToList())
            {
                if (!rVisitEntity.Contains(rProperty))
                    admientity.Remove(rProperty);
            }
            string VAdmissionId = odal.ExecuteNonQuery("ps_insert_Admission_1", CommandType.StoredProcedure, "AdmissionId", admientity);
            objAdmission.AdmissionId = Convert.ToInt32(VAdmissionId);
            //await _context.LogProcedureExecution(admientity, nameof(Admission), objAdmission.AdmissionId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);

            string[] BEntity = { "BedName", "RoomId", "IsAvailible", "IsActive", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" };
            var tokenObj = new
            {
                BedId = Convert.ToInt32(objAdmission.BedId),
                RoomId = Convert.ToInt32(objAdmission.WardId)
            };
            odal.ExecuteNonQuery("ps_Update_AdmissionBedstatus", CommandType.StoredProcedure, tokenObj.ToDictionary());

            string[] PatientPolicyEntity = { "PatientPolicyId", "Opipid", "Opiptype", "PolicyNo", "PolicyValidateDate", "ApprovedAmount", "CreatedBy", "IsActive" };
            ObjTPatientPolicyInformation.Opipid = Convert.ToInt32(VAdmissionId);

            var Patiententity = ObjTPatientPolicyInformation.ToDictionary();
            foreach (var rProperty in Patiententity.Keys.ToList())
            {
                if (!PatientPolicyEntity.Contains(rProperty))
                    Patiententity.Remove(rProperty);
            }
            string PatientPolicyId = odal.ExecuteNonQuery("ps_insert_T_PatientPolicyInformation", CommandType.StoredProcedure, "PatientPolicyId", Patiententity);
            ObjTPatientPolicyInformation.PatientPolicyId = Convert.ToInt32(PatientPolicyId);
            await _context.LogProcedureExecution(admientity, nameof(Admission), objAdmission.AdmissionId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);


            
        }

        public virtual async Task UpdateAdmissionAsyncSP(Admission objAdmission, int CurrentUserId, string CurrentUserName)
        {
            //throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] rAdmissEntity = {"AdmissionId", "AdmissionDate", "AdmissionTime", "HospitalId", "PatientTypeId", "CompanyId", "TariffId", "DepartmentId", "DocNameId", "RefDocNameId",
               "RelativeName" , "RelativeAddress", "PhoneNo", "RelationshipId", "IsMlc", "MotherName", "AdmittedDoctor1" ,"AdmittedDoctor2", "RefByTypeId","RefByName","IsUpdatedBy","SubTpaComId","IsOpToIpconv","ConvertId","Ischarity"};
            var rAdmissentity1 = objAdmission.ToDictionary();
            foreach (var rProperty in rAdmissentity1.Keys.ToList())
            {
                if (!rAdmissEntity.Contains(rProperty))
                    rAdmissentity1.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_Admission_1", CommandType.StoredProcedure, rAdmissentity1);
            await _context.LogProcedureExecution(rAdmissentity1, nameof(Admission), objAdmission.AdmissionId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);

        }

        public virtual async Task<List<PatientAdmittedListSearchDto>> PatientAdmittedListSearch(string Keyword)
        {
            var qry = from r in _context.Registrations
                      join a in _context.Admissions on r.RegId equals a.RegId
                      join de in _context.MDepartmentMasters on a.DepartmentId equals de.DepartmentId
                      join p in _context.PatientTypeMasters on a.PatientTypeId equals p.PatientTypeId
                      join t in _context.TariffMasters on a.TariffId equals t.TariffId
                      join rm in _context.RoomMasters on a.WardId equals rm.RoomId
                      join b in _context.Bedmasters on a.BedId equals b.BedId
                      join g in _context.DbGenderMasters on r.GenderId equals g.GenderId
                      join d in _context.DoctorMasters on a.DocNameId equals d.DoctorId
                      join doc in _context.DoctorMasters on a.RefDocNameId equals doc.DoctorId into refDoc
                      from doc in refDoc.DefaultIfEmpty() // LEFT JOIN
                      join c in _context.CompanyMasters on a.CompanyId equals c.CompanyId into comp
                      from c in comp.DefaultIfEmpty() // LEFT JOIN
                      where a.IsDischarged == 0 &&
                      ((r.FirstName + " " + r.LastName).Contains(Keyword) || (r.MobileNo ?? "").Contains(Keyword) || (r.RegNo ?? "").Contains(Keyword))
                      orderby r.FirstName
                      select new PatientAdmittedListSearchDto()
                      {
                          FirstName = r.FirstName,
                          MiddleName = r.MiddleName,
                          LastName = r.LastName,
                          RegNo = r.RegNo,
                          AdmissionID = a.AdmissionId,
                          RegID = r.RegId,
                          AdmissionDate = a.AdmissionDate,
                          AdmissionTime = a.AdmissionTime,
                          PatientTypeID = a.PatientTypeId,
                          HospitalID = a.HospitalId,
                          DocNameID = a.DocNameId,
                          RefDocNameID = a.RefDocNameId,
                          WardId = a.WardId,
                          BedId = a.BedId,
                          IsDischarged = a.IsDischarged,
                          MobileNo = a.MobileNo,
                          IPDNo = a.Ipdno,
                          CompanyName = c.CompanyName,
                          TariffName = t.TariffName,
                          TariffId = a.TariffId,
                          ClassId = a.ClassId,
                          DoctorName = "Dr. " + d.FirstName + " " + d.LastName,
                          RoomName = rm.RoomName,
                          BedName = b.BedName,
                          Age = r.Age,
                          AgeMonth = r.AgeMonth,
                          AgeDay = r.AgeDay,
                          GenderName = g.GenderName,
                          DepartmentName = de.DepartmentName,
                          PatientType = p.PatientType,
                          RefDoctorName = "Dr. " + doc.FirstName + " " + doc.LastName,

                      };
            return await qry.Take(25).ToListAsync();
        }
        public virtual async Task<PatientAdmittedListSearchDto> PatientByAdmissionId(long AdmissionId)
        {
            var qry = from r in _context.Registrations
                      join a in _context.Admissions on r.RegId equals a.RegId
                      join t in _context.TariffMasters on a.TariffId equals t.TariffId
                      join rm in _context.RoomMasters on a.WardId equals rm.RoomId
                      join b in _context.Bedmasters on a.BedId equals b.BedId
                      join g in _context.DbGenderMasters on r.GenderId equals g.GenderId
                      join d in _context.DoctorMasters on a.DocNameId equals d.DoctorId
                      join c in _context.CompanyMasters on a.CompanyId equals c.CompanyId into comp
                      from c in comp.DefaultIfEmpty()
                      where a.IsDischarged == 0 && a.AdmissionId == AdmissionId
                      select new PatientAdmittedListSearchDto()
                      {
                          FirstName = r.FirstName,
                          MiddleName = r.MiddleName,
                          LastName = r.LastName,
                          RegNo = r.RegNo,
                          AdmissionID = a.AdmissionId,
                          RegID = r.RegId,
                          AdmissionDate = a.AdmissionDate,
                          AdmissionTime = a.AdmissionTime,
                          PatientTypeID = a.PatientTypeId,
                          HospitalID = a.HospitalId,
                          DocNameID = a.DocNameId,
                          RefDocNameID = a.RefDocNameId,
                          WardId = a.WardId,
                          BedId = a.BedId,
                          IsDischarged = a.IsDischarged,
                          MobileNo = a.MobileNo,
                          IPDNo = a.Ipdno,
                          CompanyName = c.CompanyName,
                          TariffName = t.TariffName,
                          TariffId = a.TariffId,
                          ClassId = a.ClassId,
                          DoctorName = "Dr. " + d.FirstName + " " + d.LastName,
                          RoomName = rm.RoomName,
                          BedName = b.BedName,
                          Age = r.Age,
                          AgeMonth = r.AgeMonth,
                          AgeDay = r.AgeDay,
                          GenderName = g.GenderName

                      };
            return await qry.FirstOrDefaultAsync();
        }

        public virtual async Task<List<PatientAdmittedListSearchDto>> PatientDischargeListSearch(string Keyword)
        {
            var qry = from r in _context.Registrations
                      join a in _context.Admissions on r.RegId equals a.RegId
                      join t in _context.TariffMasters on a.TariffId equals t.TariffId
                      join rm in _context.RoomMasters on a.WardId equals rm.RoomId
                      join b in _context.Bedmasters on a.BedId equals b.BedId
                      join g in _context.DbGenderMasters on r.GenderId equals g.GenderId
                      join d in _context.DoctorMasters on a.DocNameId equals d.DoctorId
                      join c in _context.CompanyMasters on a.CompanyId equals c.CompanyId into comp
                      from c in comp.DefaultIfEmpty()
                      where a.IsDischarged == 1 &&
                      ((r.FirstName + " " + r.LastName).Contains(Keyword) || (r.MobileNo ?? "").Contains(Keyword) || (r.RegNo ?? "").Contains(Keyword))
                      orderby r.FirstName
                      select new PatientAdmittedListSearchDto()
                      {
                          FirstName = r.FirstName,
                          MiddleName = r.MiddleName,
                          LastName = r.LastName,
                          RegNo = r.RegNo,
                          AdmissionID = a.AdmissionId,
                          RegID = r.RegId,
                          AdmissionDate = a.AdmissionDate,
                          AdmissionTime = a.AdmissionTime,
                          PatientTypeID = a.PatientTypeId,
                          HospitalID = a.HospitalId,
                          DocNameID = a.DocNameId,
                          RefDocNameID = a.RefDocNameId,
                          WardId = a.WardId,
                          BedId = a.BedId,
                          IsDischarged = a.IsDischarged,
                          MobileNo = a.MobileNo,
                          IPDNo = a.Ipdno,
                          CompanyName = c.CompanyName,
                          TariffName = t.TariffName,
                          TariffId = a.TariffId,
                          ClassId = a.ClassId,
                          DoctorName = d.FirstName + " " + d.LastName,
                          RoomName = rm.RoomName,
                          BedName = b.BedName,
                          Age = r.Age,
                          GenderName = g.GenderName

                      };
            return await qry.Take(25).ToListAsync();
        }

        public virtual async Task UpdateAsyncInfo(Admission OBJAdmission, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] AEntity = { "PolicyNo", "ApprovedAmount", "AdmissionId" };
            var entity = OBJAdmission.ToDictionary();

            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_CompanyInfoPatient", CommandType.StoredProcedure, entity);
            await _context.LogProcedureExecution(entity, "Admission-CompanyInfo", OBJAdmission.AdmissionId.ToInt(), Core.Domain.Logging.LogAction.Edit, UserId, Username);

        }

        public virtual async Task CancelAsync(Admission OBJAdmission, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header table records
                Admission objAdm = await _context.Admissions.FindAsync(OBJAdmission.AdmissionId);
                objAdm.IsCancelled = OBJAdmission.IsCancelled;
                //objind.IsCancelledBy = OBJAdmission.IsCancelledBy;
                //objind.IsCancelledDateTime = OBJAdmission.IsCancelledDateTime;

                _context.Admissions.Update(objAdm);
                _context.Entry(objAdm).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

    }
}
