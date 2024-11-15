using HIMS.Data.DataProviders;
using System.Data;
using System.Transactions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Aspose.Cells.Drawing;


namespace HIMS.Services.OutPatient
{
    public class DischargeServiceSP:IDischargeServiceSP
    {
        private readonly Data.Models.HIMSDbContext _context;
        public DischargeServiceSP(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsyncSP(Discharge objDischarge, Admission objAdmission, int currentUserId, string currentUserName)
        {
            // throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] rEntity = { "IsCancelled", "UpdatedBy", "IsCancelledby", "IsCancelledDate", "IsMrdreceived", "MrdreceivedDate", "MrdreceivedTime", "MrdreceivedUserId", "MrdreceivedName", "Admission" };
            var entity = objDischarge.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string DischargeId = odal.ExecuteNonQuery("v_insert_Discharge_1", CommandType.StoredProcedure, "DischargeId", entity);
            objDischarge.DischargeId = Convert.ToInt32(DischargeId);
            objAdmission.AdmissionId = Convert.ToInt32(objDischarge.AdmissionId);

            string[] rVisitEntity = { "RegId","AdmissionDate","AdmissionTime","PatientTypeId","HospitalId","DocNameId","RefDocNameId","WardId","BedId","IsBillGenerated","Ipdno","IsCancelled","CompanyId","TariffId","ClassId",
            "DepartmentId","RelativeName","RelativeAddress","PhoneNo","MobileNo","RelationshipId","AddedBy","IsMlc","MotherName","AdmittedDoctor1","AdmittedDoctor2","IsProcessing",
            "Ischarity","RefByTypeId","RefByName","IsMarkForDisNur","IsMarkForDisNurId","IsMarkForDisNurDateTime","IsCovidFlag","IsCovidUserId","IsCovidUpdateDate","IsUpdatedBy","SubTpaComId","PolicyNo","AprovAmount","CompDod",
            "IsPharClearance","Ipnumber","EstimatedAmount", "ApprovedAmount", "HosApreAmt", "PathApreAmt", "PharApreAmt", "RadiApreAmt","PharDisc", "CompBillNo", "CompBillDate", "CompDiscount" ,"CompDisDate", "CBillNo", "CFinalBillAmt", "CDisallowedAmt", "ClaimNo", "HdiscAmt", "COutsideInvestAmt", "RecoveredByPatient" ,"HChargeAmt", "HAdvAmt", "HBillId",
            "HBillDate" ,"HBillNo", "HTotalAmt", "HDiscAmt1", "HNetAmt","HPaidAmt","HBalAmt","DischargeSummaries","Discharges","TIpPrescriptionDischarges"};
            var admientity = objAdmission.ToDictionary();
            foreach (var rProperty in rVisitEntity)
            {
                admientity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("m_update_Admission_3", CommandType.StoredProcedure, entity);
            // string BedId = objAdmission.BedId;

            var tokenObj = new
            {
                //BedId = Convert.ToInt32(BedId)
            };
            odal.ExecuteNonQuery("m_Update_DischargeBedRelease", CommandType.StoredProcedure, tokenObj.ToDictionary());
        }

        public virtual async Task UpdateAsyncSP(Discharge objDischarge, Admission objAdmission, int currentUserId, string currentUserName)
        {
            // throw new NotImplementedException();

            DatabaseHelper odal = new();
            string[] rEntity = { "IsCancelled", "AddedBy", "IsCancelledby", "IsCancelledDate", "IsMrdreceived", "MrdreceivedDate", "MrdreceivedTime", "MrdreceivedUserId", "MrdreceivedName", "Admission" };
            var entity = objDischarge.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("m_update_Discharge_1", CommandType.StoredProcedure,entity);
            //objDischarge.DischargeId = Convert.ToInt32(DischargeId);
            objAdmission.AdmissionId = Convert.ToInt32(objDischarge.AdmissionId);

            string[] rVisitEntity = { "RegId","AdmissionDate","AdmissionTime","PatientTypeId","HospitalId","DocNameId","RefDocNameId","WardId","BedId","IsBillGenerated","Ipdno","IsCancelled","CompanyId","TariffId","ClassId",
             "DepartmentId","RelativeName","RelativeAddress","PhoneNo","MobileNo","RelationshipId","AddedBy","IsMlc","MotherName","AdmittedDoctor1","AdmittedDoctor2","IsProcessing",
             "Ischarity","RefByTypeId","RefByName","IsMarkForDisNur","IsMarkForDisNurId","IsMarkForDisNurDateTime","IsCovidFlag","IsCovidUserId","IsCovidUpdateDate","IsUpdatedBy","SubTpaComId","PolicyNo","AprovAmount","CompDod",
             "IsPharClearance","Ipnumber","EstimatedAmount", "ApprovedAmount", "HosApreAmt", "PathApreAmt", "PharApreAmt", "RadiApreAmt","PharDisc", "CompBillNo", "CompBillDate", "CompDiscount" ,"CompDisDate", "CBillNo", "CFinalBillAmt", "CDisallowedAmt", "ClaimNo", "HdiscAmt", "COutsideInvestAmt", "RecoveredByPatient" ,"HChargeAmt", "HAdvAmt", "HBillId",
             "HBillDate" ,"HBillNo", "HTotalAmt", "HDiscAmt1", "HNetAmt","HPaidAmt","HBalAmt","DischargeSummaries","Discharges","TIpPrescriptionDischarges"};
            var admientity = objAdmission.ToDictionary();
            foreach (var rProperty in rVisitEntity)
            {
                admientity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("m_update_Admission_3", CommandType.StoredProcedure, entity);
            // string BedId = objAdmission.BedId;

            var tokenObj = new
            {
                //BedId = Convert.ToInt32(BedId)
            };
            odal.ExecuteNonQuery("m_Update_DischargeBedRelease", CommandType.StoredProcedure, tokenObj.ToDictionary());
        }
    }
    }
