using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Nursing;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System.Data;
using System.Transactions;

namespace HIMS.Services.Nursing
{
    public class ClinicalCareService : IClinicalCareService
    {
        private readonly HIMSDbContext _context;
        public ClinicalCareService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task<IPagedList<AdmisionListNursingListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<AdmisionListNursingListDto>(model, "m_Rtrv_AdmisionList_NursingList");
        }

        public virtual async Task<IPagedList<IPPathologyListDto>> GetListAsync1(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<IPPathologyListDto>(model, "Retrive_IP_PathologyList");
        }
        public virtual async Task<IPagedList<NursingWeightListDto>> NursingWeightList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<NursingWeightListDto>(model, "m_Rtrv_NursingWeight");
        }
        public virtual async Task<IPagedList<NursingPainAssessmentListDto>> NursingPainAssessmentList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<NursingPainAssessmentListDto>(model, "m_Rtrv_NursingPainAssessment");
        }

        public virtual async Task<IPagedList<NursingSugarlevelListDto>> NursingSugarlevelList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<NursingSugarlevelListDto>(model, "m_Rtrv_NursingSugarlevel");
        }

        public virtual async Task<IPagedList<NursingVitalsListDto>> NursingVitalsList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<NursingVitalsListDto>(model, "m_Rtrv_NursingVitals");
        }


        public virtual async Task<IPagedList<NursingOxygenVentilatorListDto>> NursingOxygenVentilatorList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<NursingOxygenVentilatorListDto>(model, "m_Rtrv_NursingOxygenVentilator");
        }


        public virtual async Task InsertAsync(TNursingVital ObjTNursingVital, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TNursingVitals.Add(ObjTNursingVital);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }


        public virtual void Cancel(TNursingVital objTNursingVital, int UserId, string Username)
        {
            //throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] BEntity = {  "VitalDate", "VitalTime", "AdmissionId", "Temperature", "Pulse", "Respiration", "BloodPresure", "Cvp", "Peep", "ArterialBloodPressure", "PapressureReading",
            "Brady","Apnea","AbdominalGrith","Desaturation","SaturationWithO2","SaturationWithoutO2","Po2","Fio2","Pfration","SuctionType","CreatedBy","CreatedDate","ModifiedDate","ModifiedBy"};
            var TEntity = objTNursingVital.ToDictionary();
            foreach (var rProperty in BEntity)
            {
                TEntity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("PS_Delete_T_NursingVitals", CommandType.StoredProcedure, TEntity);
        }


        public virtual void Cancel(TNursingSugarLevel objTNursingSugarLevel, int UserId, string Username)
        {
            //throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] rEntity = { "EntryDate", "EntryTime", "Bsl", "UrineSugar", "Ettpressure", "UrineKetone", "Bodies", "IntakeMode", "AdmissionId", "ReportedToRmo", "CreatedBy", "CreatedDate", "ModifiedDate", "ModifiedBy" };
            var eEntity = objTNursingSugarLevel.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                eEntity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("PS_Delete_T_NursingSugarLevel", CommandType.StoredProcedure, eEntity);
        }


        public virtual void Cancel1(TNursingPainAssessment objTNursingPainAssessment, int UserId, string Username)
        {
            //throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] rEntity = { "PainAssessementValue", "PainAssessmentTime", "PainAssessmentDate", "AdmissionId", "CreatedBy", "CreatedDate", "ModifiedDate", "ModifiedBy" };
            var TEntity = objTNursingPainAssessment.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                TEntity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("PS_Delete_T_NursingPainAssessment", CommandType.StoredProcedure, TEntity);
        }



        public virtual void Cancel(TNursingWeight objTNursingWeight, int UserId, string Username)
        {
            //throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] rEntity = { "PatWeightValue", "PatWeightTime", "PatWeightDate", "AdmissionId", "CreatedBy", "CreatedDate", "ModifiedDate", "ModifiedBy" };
            var HEntity = objTNursingWeight.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                HEntity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("PS_Delete_T_NursingWeight", CommandType.StoredProcedure, HEntity);
        }

        public virtual void Cancel(TNursingOrygenVentilator objTNursingOrygenVentilator, int UserId, string Username)
        {
            //throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] rEntity = { "EntryDate", "EntryTime", "Mode", "TidolV", "SetRange", "Ipap", "MinuteV", "RateTotal", "Epap", "Peep", "Pc", "Mvpercentage", "PrSup", "Fio2", "Ie", "SaturationWithO2", "FlowTrigger", "OxygenRate", "AdmissionId", "CreatedBy", "CreatedDate", "ModifiedDate", "ModifiedBy" };
            var KEntity = objTNursingOrygenVentilator.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                KEntity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("PS_Delete_T_NursingOrygenVentilator", CommandType.StoredProcedure, KEntity);
        }
    }
}
