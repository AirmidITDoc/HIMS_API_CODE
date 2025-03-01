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

namespace HIMS.Services.IPPatient
{
    public class DischargeSummaryService : IDischargeSummaryService
    {
        private readonly HIMSDbContext _context;
        public DischargeSummaryService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsyncSP(DischargeSummary ObjDischargeSummary, List<TIpPrescriptionDischarge> ObjTIpPrescriptionDischarge, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "AddedByDate", "UpdatedBy", "UpdatedByDate" };
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
        public virtual async Task UpdateAsyncSP(DischargeSummary ObjDischargeSummary, TIpPrescriptionDischarge ObjTIpPrescriptionDischarge, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "AdmissionId", "DischargeSummaryDate", "DischargeSummaryTime", "AddedBy", "AddedByDate", "UpdatedByDate" };
            var Uentity = ObjDischargeSummary.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                Uentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("sp_update_DischargeSummary_1", CommandType.StoredProcedure, Uentity);

            var tokensObj = new
            {
                OPDIPDID = Convert.ToInt32(ObjTIpPrescriptionDischarge.OpdIpdId)
            };
            odal.ExecuteNonQuery("sp_Delete_T_IP_Prescription_Discharge", CommandType.StoredProcedure, tokensObj.ToDictionary());

            string[] DEntity = { "PrecriptionId", "CreatedDate", "ModifiedBy", "ModifiedDate", "IsClosed" };
            var pentity = ObjTIpPrescriptionDischarge.ToDictionary();
            foreach (var Property in DEntity)
            {
                pentity.Remove(Property);
            }
            odal.ExecuteNonQuery("ps_insert_T_IP_Prescription_Discharge_1", CommandType.StoredProcedure, pentity);

        }
        public virtual async Task InsertAsyncTemplate(DischargeSummary ObjDischargeTemplate, List<TIpPrescriptionDischarge> ObjTIpPrescriptionTemplate, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "History", "Diagnosis", "Investigation", "OpertiveNotes", "TreatmentGiven", "TreatmentAdvisedAfterDischarge", "Remark", "DischargeSummaryDate", "OpDate","Optime","DischargeSummaryTime","DoctorAssistantName","ClaimNumber","PreOthNumber","AddedByDate",
                                 "UpdatedBy","UpdatedByDate","SurgeryProcDone","Icd10code","ClinicalConditionOnAdmisssion","OtherConDrOpinions","ConditionAtTheTimeOfDischarge","PainManagementTechnique","LifeStyle","WarningSymptoms","Radiology","ClinicalFinding" };
            var Tentity = ObjDischargeTemplate.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                Tentity.Remove(rProperty);
            }
            // Add the new parameter
            Tentity["TemplateDescriptionHtml"] = 0; // Ensure objpayment 

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
    }
}













