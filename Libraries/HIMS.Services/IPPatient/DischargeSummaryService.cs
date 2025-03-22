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
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Pathology;

namespace HIMS.Services.IPPatient
{
    public class DischargeSummaryService : IDischargeSummaryService
    {
        private readonly HIMSDbContext _context;
        public DischargeSummaryService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<PatientClearanceAprovViewListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PatientClearanceAprovViewListDto>(model, "m_Rtrv_PatientClearanceAprovViewList");
        }
        public virtual async Task<IPagedList<PatientClearanceApprovalListDto>> GetListAsyncP(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PatientClearanceApprovalListDto>(model, "m_Rtrv_PatientClearanceApprovalList");
        }
        public virtual async Task<IPagedList<DischrageSummaryListDTo>> IPDischargesummaryList(GridRequestModel objGrid)
        {
            return await DatabaseHelper.GetGridDataBySp<DischrageSummaryListDTo>(objGrid, "v_Rtrv_T_DischargeSummary");
        }

        public virtual async Task<IPagedList<IPPrescriptiononDischargeListDto>> IPPrescriptionDischargesummaryList(GridRequestModel objGrid)
        {
            return await DatabaseHelper.GetGridDataBySp<IPPrescriptiononDischargeListDto>(objGrid, "v_Rtrv_IP_Prescription_Discharge");
        }
        public virtual async Task InsertAsyncSP(DischargeSummary ObjDischargeSummary, List<TIpPrescriptionDischarge> ObjTIpPrescriptionDischarge, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "AddedByDate", "UpdatedBy", "UpdatedByDate", "TemplateDescriptionHtml" };
            var entity = ObjDischargeSummary.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string VDischargeSummaryId = odal.ExecuteNonQuery("sp_insert_DischargeSummary_1", CommandType.StoredProcedure, "DischargeSummaryId", entity);
            ObjDischargeSummary.DischargeSummaryId = Convert.ToInt32(VDischargeSummaryId);

            foreach (var item in ObjTIpPrescriptionDischarge)
            {

                string[] DEntity = { "PrecriptionId", "CreatedDate", "ModifiedBy", "ModifiedDate", "IsClosed" };
                var pentity = item.ToDictionary();
                foreach (var Property in DEntity)
                {
                    pentity.Remove(Property);
                }
                odal.ExecuteNonQuery("ps_insert_T_IP_Prescription_Discharge_1", CommandType.StoredProcedure, pentity);

            }
        }
        public virtual async Task UpdateAsyncSP(DischargeSummary ObjDischargeSummary, List<TIpPrescriptionDischarge> ObjTIpPrescriptionDischarge, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "AdmissionId", "DischargeSummaryDate", "DischargeSummaryTime", "AddedBy", "AddedByDate", "UpdatedByDate", "TemplateDescriptionHtml" };
            var Uentity = ObjDischargeSummary.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                Uentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("sp_update_DischargeSummary_1", CommandType.StoredProcedure, Uentity);


            foreach (var item in ObjTIpPrescriptionDischarge)
            {
                var tokensObj = new
                {
                    OPDIPDID = Convert.ToInt32(item.OpdIpdId)
                };
                odal.ExecuteNonQuery("sp_Delete_T_IP_Prescription_Discharge", CommandType.StoredProcedure, tokensObj.ToDictionary());
            }


            foreach (var item in ObjTIpPrescriptionDischarge)
            {

                string[] DEntity1 = { "PrecriptionId", "CreatedDate", "ModifiedBy", "ModifiedDate", "IsClosed" };
                var pentity1 = item.ToDictionary();
                foreach (var Property in DEntity1)
                {
                    pentity1.Remove(Property);
                }
                odal.ExecuteNonQuery("ps_insert_T_IP_Prescription_Discharge_1", CommandType.StoredProcedure, pentity1);

            }

        }
        public virtual async Task InsertAsyncTemplate(DischargeSummary ObjDischargeTemplate, List<TIpPrescriptionDischarge> ObjTIpPrescriptionTemplate, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "History", "Diagnosis", "Investigation", "OpertiveNotes", "TreatmentGiven", "TreatmentAdvisedAfterDischarge", "Remark", "DischargeSummaryDate", "OpDate","Optime","DischargeSummaryTime","DoctorAssistantName","ClaimNumber","PreOthNumber","AddedByDate",
                                 "UpdatedBy","UpdatedByDate","SurgeryProcDone","Icd10code","ClinicalConditionOnAdmisssion","OtherConDrOpinions","ConditionAtTheTimeOfDischarge","PainManagementTechnique","LifeStyle","WarningSymptoms","Radiology","ClinicalFinding"};
            var Tentity = ObjDischargeTemplate.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                Tentity.Remove(rProperty);
            }

            string TDischargeSummaryId = odal.ExecuteNonQuery("ps_insert_DischargeSummaryTemplate", CommandType.StoredProcedure, "DischargeSummaryId", Tentity);
            ObjDischargeTemplate.DischargeSummaryId = Convert.ToInt32(TDischargeSummaryId);

            foreach (var item in ObjTIpPrescriptionTemplate)
            {
                string[] DEntity = { "PrecriptionId", "CreatedDate", "ModifiedBy", "ModifiedDate", "IsClosed" };
                var pentity = item.ToDictionary();
                foreach (var Property in DEntity)
                {
                    pentity.Remove(Property);
                }
                odal.ExecuteNonQuery("v_insert_T_IP_Prescription_Discharge_1", CommandType.StoredProcedure, pentity);
            }
        }
        public virtual async Task UpdateAsyncTemplate(DischargeSummary ObjDischargeTemplate, List<TIpPrescriptionDischarge> ObjTIpPrescriptionTemplate, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "AdmissionId","History", "Diagnosis", "Investigation", "ClinicalFinding", "OpertiveNotes", "TreatmentGiven", "TreatmentAdvisedAfterDischarge", "Remark", "OpDate", "Optime", "DischargeSummaryTime", "DoctorAssistantName", "ClaimNumber", "PreOthNumber", "AddedBy",
                                 "AddedByDate","UpdatedByDate","SurgeryProcDone","Icd10code","ClinicalConditionOnAdmisssion","OtherConDrOpinions","ConditionAtTheTimeOfDischarge","PainManagementTechnique","LifeStyle","WarningSymptoms","Radiology","DischargeSummaryDate"};
            var Sentity = ObjDischargeTemplate.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                Sentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_DischargeSummaryTemplate", CommandType.StoredProcedure, Sentity);
          
            var tokensObj = new
            {
                OPDIPDID = Convert.ToInt32(ObjDischargeTemplate.AdmissionId)
            };
            odal.ExecuteNonQuery("PS_Delete_T_IP_Prescription_Discharge", CommandType.StoredProcedure, tokensObj.ToDictionary());
            foreach (var item in ObjTIpPrescriptionTemplate)
            {
                string[] DEntity = { "PrecriptionId", "CreatedDate", "ModifiedBy", "ModifiedDate", "IsClosed" };
                var pentity = item.ToDictionary();
                foreach (var Property in DEntity)
                {
                    pentity.Remove(Property);
                }
                odal.ExecuteNonQuery("v_insert_T_IP_Prescription_Discharge_1", CommandType.StoredProcedure, pentity);


            }
        }
        public virtual async Task InsertDischargeSP(Discharge ObjDischarge, Admission ObjAdmission, Bedmaster ObjBedmaster, int currentUserId, string currentUserName)
        {
            // throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] rEntity = { "IsCancelled", "UpdatedBy", "IsCancelledby", "IsCancelledDate", "IsMrdreceived", "MrdreceivedDate", "MrdreceivedTime", "MrdreceivedUserId", "MrdreceivedName", "CreatedBy","CreatedDate","ModifiedBy","ModifiedDate" };
            var entity = ObjDischarge.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string DischargeId = odal.ExecuteNonQuery("v_insert_Discharge_1", CommandType.StoredProcedure, "DischargeId", entity);
            ObjDischarge.DischargeId = Convert.ToInt32(DischargeId);

            string[] AEntity = { "RegId","AdmissionDate","AdmissionTime","PatientTypeId","HospitalId","DocNameId","RefDocNameId","WardId","BedId","IsBillGenerated","Ipdno","IsCancelled","CompanyId","TariffId","ClassId",
            "DepartmentId","RelativeName","RelativeAddress","PhoneNo","MobileNo","RelationshipId","AddedBy","IsMlc","MotherName","AdmittedDoctor1","AdmittedDoctor2","IsProcessing",
            "Ischarity","RefByTypeId","RefByName","IsMarkForDisNur","IsMarkForDisNurId","IsMarkForDisNurDateTime","IsCovidFlag","IsCovidUserId","IsCovidUpdateDate","IsUpdatedBy","SubTpaComId","PolicyNo","AprovAmount","CompDod",
            "IsPharClearance","Ipnumber","EstimatedAmount", "ApprovedAmount", "HosApreAmt", "PathApreAmt", "PharApreAmt", "RadiApreAmt","PharDisc", "CompBillNo", "CompBillDate", "CompDiscount" ,"CompDisDate", "CBillNo", "CFinalBillAmt", "CDisallowedAmt", "ClaimNo", "HdiscAmt", "COutsideInvestAmt", "RecoveredByPatient" ,"HChargeAmt", "HAdvAmt", "HBillId",
            "HBillDate" ,"HBillNo", "HTotalAmt", "HDiscAmt1", "HNetAmt","HPaidAmt","HBalAmt","DischargeSummaries","Discharges","TIpPrescriptionDischarges","IsOpToIpconv","RefDoctorDept","AdmissionType","MedicalApreAmt","AdminPer","AdminAmt","SubTpacomp","IsCtoH","IsInitinatedDischarge"};
            var Admientity = ObjAdmission.ToDictionary();
            foreach (var rProperty in AEntity)
            {
                Admientity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("m_update_Admission_3", CommandType.StoredProcedure, Admientity);

            string[] BEntity = { "BedName", "RoomId", "IsAvailible", "IsActive", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" };
            var Bentity = ObjBedmaster.ToDictionary();
            foreach (var rProperty in BEntity)
            {
                Bentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("m_Update_DischargeBedRelease", CommandType.StoredProcedure, Bentity);

        }
        public virtual async Task UpdateDischargeSP(Discharge ObjDischarge, Admission ObjAdmission, int currentUserId, string currentUserName)
        {
            // throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] rEntity = { "AdmissionId", "IsCancelled", "AddedBy", "UpdatedBy", "IsCancelledby", "IsCancelledDate", "IsMrdreceived", "MrdreceivedDate", "MrdreceivedTime", "MrdreceivedUserId", "MrdreceivedName","CreatedBy","CreatedDate","ModifiedDate"};
            var Dentity = ObjDischarge.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                Dentity.Remove(rProperty);
                // Add the new parameter
                Dentity["ModeOfDischargeId"] = 0; // Ensure objpayment 
                Dentity["ModifiedBy"] = 0; // Ensure objpayment 
            }
            odal.ExecuteNonQuery("m_update_Discharge_1", CommandType.StoredProcedure, Dentity);

            string[] AEntity = { "RegId","AdmissionDate","AdmissionTime","PatientTypeId","HospitalId","DocNameId","RefDocNameId","WardId","BedId","IsBillGenerated","Ipdno","IsCancelled","CompanyId","TariffId","ClassId",
            "DepartmentId","RelativeName","RelativeAddress","PhoneNo","MobileNo","RelationshipId","AddedBy","IsMlc","MotherName","AdmittedDoctor1","AdmittedDoctor2","IsProcessing",
            "Ischarity","RefByTypeId","RefByName","IsMarkForDisNur","IsMarkForDisNurId","IsMarkForDisNurDateTime","IsCovidFlag","IsCovidUserId","IsCovidUpdateDate","IsUpdatedBy","SubTpaComId","PolicyNo","AprovAmount","CompDod",
            "IsPharClearance","Ipnumber","EstimatedAmount", "ApprovedAmount", "HosApreAmt", "PathApreAmt", "PharApreAmt", "RadiApreAmt","PharDisc", "CompBillNo", "CompBillDate", "CompDiscount" ,"CompDisDate", "CBillNo", "CFinalBillAmt", "CDisallowedAmt", "ClaimNo", "HdiscAmt", "COutsideInvestAmt", "RecoveredByPatient" ,"HChargeAmt", "HAdvAmt", "HBillId",
            "HBillDate" ,"HBillNo", "HTotalAmt", "HDiscAmt1", "HNetAmt","HPaidAmt","HBalAmt","DischargeSummaries","Discharges","TIpPrescriptionDischarges","IsOpToIpconv","RefDoctorDept","AdmissionType","MedicalApreAmt","AdminPer","AdminAmt","SubTpacomp","IsCtoH","IsInitinatedDischarge"};
            var Aentity = ObjAdmission.ToDictionary();
            foreach (var rProperty in AEntity)
            {
                Aentity.Remove(rProperty);

            }
            odal.ExecuteNonQuery("update_Admission_3", CommandType.StoredProcedure, Aentity);
        }
       
        public virtual async Task DischargeInsertAsyncSP(InitiateDischarge ObjInitiateDischarge, int currentUserId, string currentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.InitiateDischarges.Add(ObjInitiateDischarge);
                await _context.SaveChangesAsync();

                Admission objAdmission = await _context.Admissions.FirstOrDefaultAsync(x => x.AdmissionId == ObjInitiateDischarge.AdmId);
                if (objAdmission != null)
                {
                    objAdmission.IsInitinatedDischarge = 1;
                    _context.Entry(objAdmission).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync(InitiateDischarge ObjInitiateDischarge, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {

                //Update header &detail table records
                _context.InitiateDischarges.Update(ObjInitiateDischarge);
                _context.Entry(ObjInitiateDischarge).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
    }
}





