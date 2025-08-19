using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Data;

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
        public virtual async Task<IPagedList<AdmissionListDto>> GetAdmissionDischargeListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<AdmissionListDto>(model, "ps_rtrv_AdmtdWithDischargeDate_Ptnt_Dtls");
        }


        public virtual void InsertAsyncSP(Registration objRegistration, Admission objAdmission, int CurrentUserId, string CurrentUserName)
        {

            // OLD CODE With SP
            DatabaseHelper odal = new();
            string[] rEntity = { "RegNo", "RegPrefix", "AnnualIncome", "IsIndientOrWeaker", "RationCardNo", "IsMember", "UpdatedBy", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" };
            var entity = objRegistration.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string RegId = odal.ExecuteNonQuery("ps_insert_Registration_1", CommandType.StoredProcedure, "RegId", entity);
            objRegistration.RegId = Convert.ToInt64(RegId);
            objAdmission.RegId = Convert.ToInt64(RegId);

            string[] rVisitEntity = { "Ipdno", "IsCancelled", "IsProcessing", "Ischarity", "IsMarkForDisNur", "IsMarkForDisNurId", "IsMarkForDisNurDateTime", "IsCovidFlag", "IsCovidUserId", "IsCovidUpdateDate",
            "IsUpdatedBy","IsPharClearance","Ipnumber","EstimatedAmount","HosApreAmt","ApprovedAmount","PathApreAmt","PharApreAmt","RadiApreAmt","PharDisc","CompBillNo","CompBillDate","CompDiscount","CompDisDate",
            "CBillNo","CFinalBillAmt","CDisallowedAmt","ClaimNo","HdiscAmt","COutsideInvestAmt","RecoveredByPatient","HChargeAmt","HAdvAmt","HBillId","HBillDate","HBillNo","HTotalAmt","HDiscAmt1","HNetAmt","HPaidAmt",
            "HBalAmt","MedicalApreAmt","AdminPer","AdminAmt","SubTpacomp","IsCtoH","IsInitinatedDischarge","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate"};
            var admientity = objAdmission.ToDictionary();
            foreach (var rProperty in rVisitEntity)
            {
                admientity.Remove(rProperty);
            }
            string VAdmissionId = odal.ExecuteNonQuery("ps_insert_Admission_1", CommandType.StoredProcedure, "AdmissionId", admientity);
            objAdmission.AdmissionId = Convert.ToInt32(VAdmissionId);

            var tokenObj = new
            {
                BedId = Convert.ToInt32(objAdmission.BedId)
            };
            odal.ExecuteNonQuery("ps_Update_AdmissionBedstatus", CommandType.StoredProcedure, tokenObj.ToDictionary());
        }

        public virtual async Task InsertRegAsyncSP(Registration ObjRegistration, Admission objAdmission, int currentUserId, string currentUserName)
        {
            DatabaseHelper odal = new();

            string[] rEntity = { "RegNo", "RegPrefix", "AnnualIncome", "IsIndientOrWeaker", "RationCardNo", "IsMember", "UpdatedBy", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" };
            var entity = ObjRegistration.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string RegId = odal.ExecuteNonQuery("ps_insert_Registration_1", CommandType.StoredProcedure, "RegId", entity);
            ObjRegistration.RegId = Convert.ToInt64(RegId);
            objAdmission.RegId = Convert.ToInt32(RegId);


            string[] rVisitEntity = { "Ipdno", "IsCancelled", "IsProcessing", "Ischarity", "IsMarkForDisNur", "IsMarkForDisNurId", "IsMarkForDisNurDateTime", "IsCovidFlag" , "IsCovidUserId", "IsCovidUpdateDate",
                "IsUpdatedBy", "MedicalApreAmt" , "IsPharClearance", "Ipnumber", "EstimatedAmount", "ApprovedAmount", "HosApreAmt", "PathApreAmt", "PharApreAmt", "RadiApreAmt","IsUpdatedBy"
                ,"PharDisc", "CompBillNo", "CompBillDate", "CompDiscount" ,"CompDisDate", "CBillNo", "CFinalBillAmt", "CDisallowedAmt", "ClaimNo", "HdiscAmt", "COutsideInvestAmt", "RecoveredByPatient" ,"HChargeAmt", "HAdvAmt", "HBillId",
                "HBillDate" ,"HBillNo", "HTotalAmt", "HDiscAmt1", "HNetAmt","HPaidAmt","HBalAmt","DischargeSummaries","Discharges","TIpPrescriptionDischarges","AdminPer","AdminAmt","SubTpacomp","IsCtoH","IsInitinatedDischarge","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate"};
            var visitentity = objAdmission.ToDictionary();
            //objAdmission.RegId = Convert.ToInt64(objRegistration.RegId);

            foreach (var rProperty in rVisitEntity)
            {
                visitentity.Remove(rProperty);
            }
            string AdmissionId = odal.ExecuteNonQuery("ps_insert_Admission_1", CommandType.StoredProcedure, "AdmissionId", visitentity);
            objAdmission.AdmissionId = Convert.ToInt32(AdmissionId);



            var tokenObj = new
            {
                BedId = Convert.ToInt32(objAdmission.BedId)
            };
            odal.ExecuteNonQuery("ps_Update_AdmissionBedstatus", CommandType.StoredProcedure, tokenObj.ToDictionary());
        }


        public virtual async Task UpdateAdmissionAsyncSP(Admission objAdmission, int currentUserId, string currentUserName)
        {
            //throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] rAdmissEntity = {"RegId", "Ipdno", "IsCancelled", "IsProcessing", "Ischarity", "IsMarkForDisNur", "IsMarkForDisNurId", "IsMarkForDisNurDateTime", "IsCovidFlag", "IsCovidUserId", "IsCovidUpdateDate",
               "MedicalApreAmt" , "IsPharClearance", "Ipnumber", "EstimatedAmount", "ApprovedAmount", "HosApreAmt", "PathApreAmt", "PharApreAmt", "RadiApreAmt","AddedBy"
            ,"PharDisc", "CompBillNo", "CompBillDate", "CompDiscount" ,"CompDisDate", "CBillNo", "CFinalBillAmt", "CDisallowedAmt", "ClaimNo", "HdiscAmt", "COutsideInvestAmt", "RecoveredByPatient" ,"HChargeAmt", "HAdvAmt", "HBillId",
                "HBillDate" ,"HBillNo", "HTotalAmt", "HDiscAmt1", "HNetAmt","HPaidAmt","HBalAmt","DischargeSummaries","Discharges","TIpPrescriptionDischarges"
            ,"WardId", "BedId", "DischargeDate", "DischargeTime","IsDischarged","IsBillGenerated","ClassId","Discharges","PhoneNo ","MobileNo","PolicyNo",
            "AprovAmount","CompDod","RefDoctorDept","AdmissionType","AdminPer","AdminAmt","SubTpacomp","IsCtoH","IsInitinatedDischarge","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate"};
            var rAdmissentity1 = objAdmission.ToDictionary();
            foreach (var rProperty in rAdmissEntity)
            {
                rAdmissentity1.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_Admission_1", CommandType.StoredProcedure, rAdmissentity1);
            // objAdmission.AdmissionId = Convert.ToInt32(objAdmission.AdmissionId);
        }

        public virtual async Task<List<PatientAdmittedListSearchDto>> PatientAdmittedListSearch(string Keyword)
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
                          DoctorName ="Dr. "+ d.FirstName + " " + d.LastName,
                          RoomName = rm.RoomName,
                          BedName = b.BedName,
                          Age = r.Age,
                          AgeMonth = r.AgeMonth,
                          AgeDay = r.AgeDay,
                          GenderName = g.GenderName

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
                      where a.IsDischarged == 0 && a.AdmissionId==AdmissionId
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
                          DoctorName =  d.FirstName + " " + d.LastName,
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
            string[] IEntity = { "RegId","AdmissionDate" ,"AdmissionTime","PatientTypeId", "HospitalId", "DocNameId", "RefDocNameId","WardId", "BedId", "DischargeDate", "DischargeTime", "IsDischarged", "IsBillGenerated", "Ipdno", "IsCancelled", "CompanyId", "TariffId",
            "ClassId","DepartmentId","RelativeName","RelativeAddress","PhoneNo","MobileNo","RelationshipId","AddedBy","IsMlc","MotherName","AdmittedDoctor1","AdmittedDoctor2","IsProcessing",
            "Ischarity","RefByTypeId","RefByName","IsMarkForDisNur","IsMarkForDisNurId","IsMarkForDisNurDateTime","IsCovidFlag","IsCovidUpdateDate",
            "IsUpdatedBy","SubTpaComId","CompDod","IsPharClearance","Ipnumber","CompBillNo","CompBillDate","CompDisDate","CompDiscount","CBillNo","HChargeAmt","HAdvAmt","HBillId","HBillDate",
            "HBillNo","HTotalAmt","HDiscAmt1","HNetAmt","HPaidAmt","HBalAmt","ConvertId","IsOpToIpconv","RefDoctorDept","AdmissionType","AdminPer","AdminAmt","SubTpacomp","IsCtoH","IsInitinatedDischarge","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate","IsCovidUserId","AprovAmount"};
            var centity = OBJAdmission.ToDictionary();
            foreach (var rProperty in IEntity)
            {
                centity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("ps_update_CompanyInfo", CommandType.StoredProcedure, centity);
        }
    }
}
