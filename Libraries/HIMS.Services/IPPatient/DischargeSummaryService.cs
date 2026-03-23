using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Transactions;
using System.Xml;

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
            return await DatabaseHelper.GetGridDataBySp<DischrageSummaryListDTo>(objGrid, "ps_Rtrv_T_DischargeSummary");
        }

        public virtual async Task<IPagedList<IPPrescriptiononDischargeListDto>> IPPrescriptionDischargesummaryList(GridRequestModel objGrid)
        {
            return await DatabaseHelper.GetGridDataBySp<IPPrescriptiononDischargeListDto>(objGrid, "ps_Rtrv_IP_Prescription_Discharge");
        }

        public virtual async Task InsertSP(DischargeSummary ObjDischargeSummary, List<TIpPrescriptionDischarge> ObjTIpPrescriptionDischarge, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "AdmissionId", "DischargeId", "History", "Diagnosis", "Investigation", "ClinicalFinding", "OpertiveNotes", "TreatmentGiven", "TreatmentAdvisedAfterDischarge", "Followupdate", "Remark", "DischargeSummaryDate", "OpDate", "Optime", "DischargeDoctor1", "DischargeDoctor2", "DischargeDoctor3", "DischargeSummaryTime", "DoctorAssistantName", "ClaimNumber", "PreOthNumber", "AddedBy", "UpdatedBy", "SurgeryProcDone", "Icd10code", "ClinicalConditionOnAdmisssion", "OtherConDrOpinions", "ConditionAtTheTimeOfDischarge", "PainManagementTechnique", "LifeStyle", "WarningSymptoms", "Radiology", "IsNormalOrDeath", "DischargeSummaryId" };
            var entity = ObjDischargeSummary.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            string VDischargeSummaryId = odal.ExecuteNonQuery("ps_insert_DischargeSummary_1", CommandType.StoredProcedure, "DischargeSummaryId", entity);
            ObjDischargeSummary.DischargeSummaryId = Convert.ToInt32(VDischargeSummaryId);
            //await _context.LogProcedureExecution(entity, nameof(DischargeSummary), Convert.ToInt32(ObjDischargeSummary.DischargeSummaryId), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);
            _ = Task.Run(() => _context.LogProcedureExecution(entity, nameof(DischargeSummary), ObjDischargeSummary.DischargeSummaryId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName));


            foreach (var item in ObjTIpPrescriptionDischarge)
            {

                string[] DEntity = { "OpdIpdId", "OpdIpdType", "Date", "Ptime", "ClassId", "GenericId", "DrugId", "DoseId", "Days", "InstructionId", "QtyPerDay", "TotalQty", "Instruction", "Remark", "IsEnglishOrIsMarathi", "StoreId", "CreatedBy" };
                var pentity = item.ToDictionary();
                foreach (var rProperty in pentity.Keys.ToList())
                {
                    if (!DEntity.Contains(rProperty))
                        pentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_insert_T_IP_Prescription_Discharge_1", CommandType.StoredProcedure, pentity);
                _ = Task.Run(() => _context.LogProcedureExecution(pentity, nameof(TIpPrescriptionDischarge), item.PrecriptionId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName));


            }
        }

        public virtual async Task UpdateSP(DischargeSummary ObjDischargeSummary, List<TIpPrescriptionDischarge> ObjTIpPrescriptionDischarge, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "DischargeSummaryId", "DischargeId", "History", "Diagnosis", "Investigation", "ClinicalFinding", "OpertiveNotes", "TreatmentGiven", "TreatmentAdvisedAfterDischarge", "Followupdate", "Remark", "OpDate", "Optime", "DischargeDoctor1", "DischargeDoctor2", "DischargeDoctor3", "DoctorAssistantName", "ClaimNumber", "PreOthNumber", "AddedBy", "UpdatedBy", "SurgeryProcDone", "Icd10code", "ClinicalConditionOnAdmisssion", "OtherConDrOpinions", "ConditionAtTheTimeOfDischarge", "PainManagementTechnique", "LifeStyle", "WarningSymptoms", "Radiology", "IsNormalOrDeath" };
            var Uentity = ObjDischargeSummary.ToDictionary();
            foreach (var rProperty in Uentity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
                    Uentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_DischargeSummary_1", CommandType.StoredProcedure, Uentity);
            _ = Task.Run(() => _context.LogProcedureExecution(Uentity, nameof(DischargeSummary), ObjDischargeSummary.DischargeSummaryId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName));



            foreach (var item in ObjTIpPrescriptionDischarge)
            {
                var tokensObj = new
                {
                    OPDIPDID = Convert.ToInt32(item.OpdIpdId)
                };
                odal.ExecuteNonQuery("ps_Delete_T_IP_Prescription_Discharge", CommandType.StoredProcedure, tokensObj.ToDictionary());

            }


            foreach (var item in ObjTIpPrescriptionDischarge)
            {

                string[] DEntity1 = { "OpdIpdId", "OpdIpdType", "Date", "Ptime", "ClassId", "GenericId", "DrugId", "DoseId", "Days", "InstructionId", "QtyPerDay", "TotalQty", "Instruction", "Remark", "IsEnglishOrIsMarathi", "StoreId", "CreatedBy" };
                var pentity1 = item.ToDictionary();
                foreach (var rProperty in pentity1.Keys.ToList())
                {
                    if (!DEntity1.Contains(rProperty))
                        pentity1.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_insert_T_IP_Prescription_Discharge_1", CommandType.StoredProcedure, pentity1);
                _ = Task.Run(() => _context.LogProcedureExecution(pentity1, nameof(TIpPrescriptionDischarge), item.PrecriptionId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName));



            }

        }

        public virtual void InsertTemplate(DischargeSummary ObjDischargeTemplate, List<TIpPrescriptionDischarge> ObjTIpPrescriptionTemplate, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "History", "Diagnosis", "Investigation", "OpertiveNotes", "TreatmentGiven", "TreatmentAdvisedAfterDischarge", "Remark", "DischargeSummaryDate", "OpDate","Optime","DischargeSummaryTime","DoctorAssistantName","ClaimNumber","PreOthNumber","AddedByDate",
                                 "UpdatedByDate","SurgeryProcDone","Icd10code","ClinicalConditionOnAdmisssion","OtherConDrOpinions","ConditionAtTheTimeOfDischarge","PainManagementTechnique","LifeStyle","WarningSymptoms","Radiology","ClinicalFinding"};
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
                odal.ExecuteNonQuery("ps_insert_T_IP_Prescription_Discharge_1", CommandType.StoredProcedure, pentity);
            }
        }
        public virtual void UpdateTemplate(DischargeSummary ObjDischargeTemplate, List<TIpPrescriptionDischarge> ObjTIpPrescriptionTemplate, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = {"History", "Diagnosis", "Investigation", "ClinicalFinding", "OpertiveNotes", "TreatmentGiven", "TreatmentAdvisedAfterDischarge", "Remark", "OpDate", "Optime", "DischargeSummaryTime", "DoctorAssistantName", "ClaimNumber", "PreOthNumber",
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
            odal.ExecuteNonQuery("ps_Delete_T_IP_Prescription_Discharge", CommandType.StoredProcedure, tokensObj.ToDictionary());
            foreach (var item in ObjTIpPrescriptionTemplate)
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
        public virtual async Task  InsertDischargeSP(Discharge ObjDischarge, Admission ObjAdmission, Bedmaster ObjBedmaster, int CurrentUserId, string CurrentUserName)
        {
            // throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] rEntity = { "AdmissionId", "DischargeDate", "DischargeTime", "DischargeTypeId", "DischargedDocId", "DischargedRmoid", "ModeOfDischargeId", "AddedBy", "ModifiedBy", "DischargeId" };
            var entity = ObjDischarge.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            string DischargeId = odal.ExecuteNonQuery("ps_insert_Discharge_1", CommandType.StoredProcedure, "DischargeId", entity);
            ObjDischarge.DischargeId = Convert.ToInt32(DischargeId);
            _ = Task.Run(() => _context.LogProcedureExecution(entity, nameof(Discharge), ObjDischarge.DischargeId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName));



            string[] AEntity = { "AdmissionId", "DischargeDate", "DischargeTime", "IsDischarged" };
            var Admientity = ObjAdmission.ToDictionary();
            foreach (var rProperty in Admientity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    Admientity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_Admission_DischareInfo_3", CommandType.StoredProcedure, Admientity);
            _ = Task.Run(() => _context.LogProcedureExecution(entity, nameof(Admission), ObjAdmission.AdmissionId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName));


            string[] BEntity = { "BedId"};
            var Bentity = ObjBedmaster.ToDictionary();
            foreach (var rProperty in Bentity.Keys.ToList())
            {
                if (!BEntity.Contains(rProperty))
                    Bentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_Update_DischargeBedRelease", CommandType.StoredProcedure, Bentity);
            _ = Task.Run(() => _context.LogProcedureExecution(entity, nameof(Bedmaster), ObjBedmaster.BedId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName));


        }

        public virtual async Task UpdateDischargeSP(Discharge ObjDischarge, Admission ObjAdmission, int CurrentUserId, string CurrentUserName)
        {
            // throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] rEntity = { "AdmissionId", "DischargeId", "DischargeDate", "DischargeTime", "DischargeTypeId", "DischargedDocId", "DischargedRmoid", "AddedBy", "ModeOfDischargeId", "ModifiedBy" };
            var Dentity = ObjDischarge.ToDictionary();
            foreach (var rProperty in Dentity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
                    Dentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_Discharge_1", CommandType.StoredProcedure, Dentity);
            _ = Task.Run(() => _context.LogProcedureExecution(Dentity, nameof(Discharge), ObjDischarge.DischargeId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName));


            string[] AEntity = { "AdmissionId", "DischargeDate", "DischargeTime", "IsDischarged" };
            var Aentity = ObjAdmission.ToDictionary();
            foreach (var rProperty in Aentity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    Aentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_Admission_DischareInfo_3", CommandType.StoredProcedure, Aentity);
            _ = Task.Run(() => _context.LogProcedureExecution(Dentity, nameof(Admission), ObjAdmission.AdmissionId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName));

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





