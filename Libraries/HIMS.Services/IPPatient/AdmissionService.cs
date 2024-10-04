using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Data.DataProviders;
using System.Data;
using System.Transactions;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Cells.Drawing;

namespace HIMS.Services.IPPatient
{
    public class AdmissionService : IAdmissionService
    {
        private readonly HIMSDbContext _context;
        public AdmissionService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsyncSP(Registration objRegistration, Admission objAdmission, int CurrentUserId, string CurrentUserName)
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
            objAdmission.RegId = Convert.ToInt32(RegId);

            string[] rVisitEntity = { "Ipdno", "IsCancelled", "IsProcessing", "Ischarity", "IsMarkForDisNur", "IsMarkForDisNurId", "IsMarkForDisNurDateTime", "IsCovidFlag" , "IsCovidUserId", "IsCovidUpdateDate",
                "IsUpdatedBy", "MedicalApreAmt" , "IsPharClearance", "Ipnumber", "EstimatedAmount", "ApprovedAmount", "HosApreAmt", "PathApreAmt", "PharApreAmt", "RadiApreAmt","IsUpdatedBy","PharDisc", "CompBillNo", "CompBillDate", "CompDiscount" ,"CompDisDate", "CBillNo", "CFinalBillAmt", "CDisallowedAmt", "ClaimNo", "HdiscAmt", "COutsideInvestAmt", "RecoveredByPatient" ,"HChargeAmt", "HAdvAmt", "HBillId",
                "HBillDate" ,"HBillNo", "HTotalAmt", "HDiscAmt1", "HNetAmt","HPaidAmt","HBalAmt","DischargeSummaries","Discharges","TIpPrescriptionDischarges"};
            var admientity = objAdmission.ToDictionary();
            foreach (var rProperty in rVisitEntity)
            {
                admientity.Remove(rProperty);
            }
            string AdmissionId = odal.ExecuteNonQuery("v_insert_Admission_1", CommandType.StoredProcedure, "AdmissionId", admientity);
            objAdmission.AdmissionId = Convert.ToInt32(AdmissionId);

            var tokenObj = new
            {
                BedId = Convert.ToInt32(objAdmission.BedId)
            };
            odal.ExecuteNonQuery("m_Update_AdmissionBedstatus", CommandType.StoredProcedure, tokenObj.ToDictionary());
        }

        public virtual async Task InsertRegAsyncSP(Admission objAdmission, int currentUserId, string currentUserName)
        {
            //throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] rVisitEntity = { "Ipdno", "IsCancelled", "IsProcessing", "Ischarity", "IsMarkForDisNur", "IsMarkForDisNurId", "IsMarkForDisNurDateTime", "IsCovidFlag" , "IsCovidUserId", "IsCovidUpdateDate",
                "IsUpdatedBy", "MedicalApreAmt" , "IsPharClearance", "Ipnumber", "EstimatedAmount", "ApprovedAmount", "HosApreAmt", "PathApreAmt", "PharApreAmt", "RadiApreAmt","IsUpdatedBy"
            ,"PharDisc", "CompBillNo", "CompBillDate", "CompDiscount" ,"CompDisDate", "CBillNo", "CFinalBillAmt", "CDisallowedAmt", "ClaimNo", "HdiscAmt", "COutsideInvestAmt", "RecoveredByPatient" ,"HChargeAmt", "HAdvAmt", "HBillId",
                "HBillDate" ,"HBillNo", "HTotalAmt", "HDiscAmt1", "HNetAmt","HPaidAmt","HBalAmt","DischargeSummaries","Discharges","TIpPrescriptionDischarges"};
            var visitentity = objAdmission.ToDictionary();
            foreach (var rProperty in rVisitEntity)
            {
                visitentity.Remove(rProperty);
            }
            string AdmissionId = odal.ExecuteNonQuery("insert_Admission_1", CommandType.StoredProcedure, "AdmissionId", visitentity);
            objAdmission.AdmissionId = Convert.ToInt32(AdmissionId);
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
            "AprovAmount","CompDod","RefDoctorDept","AdmissionType"};
            var rAdmissentity1 = objAdmission.ToDictionary();
            foreach (var rProperty in rAdmissEntity)
            {
                rAdmissentity1.Remove(rProperty);
            }
            odal.ExecuteNonQuery("v_update_Admission_1", CommandType.StoredProcedure,rAdmissentity1);
           // objAdmission.AdmissionId = Convert.ToInt32(objAdmission.AdmissionId);
        }
    }
}
