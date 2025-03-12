using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.OutPatient
{
    public class OPDPrescriptionMedicalService : IOPDPrescriptionMedicalService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public OPDPrescriptionMedicalService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<OPRequestListDto>> TOprequestList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<OPRequestListDto>(model, "m_Rtrv_OPRequestList");
        }
        public virtual async Task<IPagedList<GetVisitInfoListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<GetVisitInfoListDto>(model, "m_Rtrv_GetVisitInfo");
        }
        public virtual async Task<IPagedList<PrescriptionDetailsVisitWiseListDto>> GetListAsyncL(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PrescriptionDetailsVisitWiseListDto>(model, "Get_PrescriptionDetailsVisitWise");
        }

        public virtual async Task<IPagedList<MOpcasepaperDignosisMaster>> GetDignosisListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<MOpcasepaperDignosisMaster>(model, "m_Rtrv_OPCasepaperDignosisList");
        }

        public virtual async Task<IPagedList<OPrtrvDignosisListDto>> TDignosisrRtrvList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<OPrtrvDignosisListDto>(model, "m_Rtrv_OPCasepaperDignosisMaster");
        }


        //Ashu///
        public virtual async Task InsertPrescriptionAsyncSP(TPrescription objTPrescription, List<TOprequestList> objTOprequestList, List<MOpcasepaperDignosisMaster> objmOpcasepaperDignosisMaster, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            var tokensObj = new
            {
                OPIPID = Convert.ToInt32(objTPrescription.OpdIpdIp)
            };
            odal.ExecuteNonQuery("sp_delete_OPPrescription_1", CommandType.StoredProcedure, tokensObj.ToDictionary());

            string[] rEntity = { "PrecriptionId", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn" };
            var entity = objTPrescription.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            entity["IsAddBy"] = 0; // Ensure objpayment has OPDIPDType
            odal.ExecuteNonQuery("ps_insert_OPPrescription_1", CommandType.StoredProcedure, entity);

            foreach (var item in objTOprequestList)
            {

                string[] rDetailEntity = { "RequestTranId", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn" };

                var PrescriptionEntity = item.ToDictionary();
                foreach (var rProperty in rDetailEntity)
                {
                    PrescriptionEntity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("sp_Insert_T_OPRequestList", CommandType.StoredProcedure, PrescriptionEntity);

            }

            foreach (var item in objmOpcasepaperDignosisMaster)
            {
                string[] PayEntity = { "Id", };
                var CasepaperEntity = item.ToDictionary();
                foreach (var rProperty in PayEntity)
                {
                    CasepaperEntity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("sp_Insert_OPCasepaperDignosisMaster", CommandType.StoredProcedure, CasepaperEntity);


            }
        }
      
        public virtual async Task UpdateAsync(TPrescription OBJTPrescription, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled);
                var Prescription = await _context.TPrescriptions.FindAsync(OBJTPrescription.PrecriptionId);

            if (Prescription != null)  
            {
                Prescription.DoseId = OBJTPrescription.DoseId;
                await _context.SaveChangesAsync();  

                scope.Complete(); 
            }
        }
        public virtual async Task UpdateAsyncGeneric(TPrescription OBJTPrescription, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled);
                var Prescription = await _context.TPrescriptions.FindAsync(OBJTPrescription.PrecriptionId);

            if (Prescription != null)
            {
                Prescription.GenericId = OBJTPrescription.GenericId;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
    }
}
