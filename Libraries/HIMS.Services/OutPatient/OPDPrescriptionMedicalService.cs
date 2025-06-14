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
            return await DatabaseHelper.GetGridDataBySp<OPRequestListDto>(model, "ps_Rtrv_OPRequestList");
        }
        public virtual async Task<IPagedList<GetVisitInfoListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<GetVisitInfoListDto>(model, "ps_Rtrv_GetVisitInfo");
        }
        public virtual async Task<IPagedList<PrescriptionDetailsVisitWiseListDto>> GetListAsyncL(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PrescriptionDetailsVisitWiseListDto>(model, "sp_Rtrv_PrescriptionDetailsVisitWise");
        }

        public virtual async Task<IPagedList<MOpcasepaperDignosisMaster>> GetDignosisListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<MOpcasepaperDignosisMaster>(model, "ps_Rtrv_OPCasepaperDignosisList");
        }

        public virtual async Task<IPagedList<OPrtrvDignosisListDto>> TDignosisrRtrvList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<OPrtrvDignosisListDto>(model, "ps_Rtrv_OPCasepaperDignosisMaster");
        }
        public virtual async Task<IPagedList<getPrescriptionTemplateDetailsListDto>> TemplateDetailsList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<getPrescriptionTemplateDetailsListDto>(model, "ps_RtrvTemplate_PrescriptionList");
        }

        //Ashu///  Modifierd by shilpa 2025/14/06 m_Update_VisitFollowupDate//
        public virtual async Task InsertPrescriptionAsyncSP(List<TPrescription> objTPrescription,VisitDetail ObjVisitDetail,List<TOprequestList> objTOprequestList, List<MOpcasepaperDignosisMaster> objmOpcasepaperDignosisMaster, int UserId, string UserName)
        {
            DatabaseHelper odal = new();
            foreach (var modelItem in objTPrescription)
            {

                var tokensObj = new
                {
                    OPIPID = Convert.ToInt32(modelItem.OpdIpdIp)

                };
                odal.ExecuteNonQuery("sp_delete_OPPrescription_1", CommandType.StoredProcedure, tokensObj.ToDictionary());
            }

            foreach (var modelItem in objTPrescription)
            {
                modelItem.Date = Convert.ToDateTime(modelItem.Date);
                modelItem.Ptime = Convert.ToDateTime(modelItem.Ptime);
                modelItem.CreatedBy = UserId;
                objTOprequestList.ForEach(x => { x.OpIpId = modelItem.OpdIpdIp; x.CreatedBy = UserId; x.ModifiedBy = UserId; });
                objmOpcasepaperDignosisMaster.ForEach(x => { x.VisitId = modelItem.OpdIpdIp; });

                string[] rEntity = { "PrecriptionId", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn" };
                var entity = modelItem.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                entity["IsAddBy"] = 0; // Ensure objpayment has OPDIPDType
                odal.ExecuteNonQuery("ps_insert_OPPrescription_1", CommandType.StoredProcedure, entity);

                string[] VDetailEntity = { "RegId","VisitDate","VisitTime","UnitId","PatientTypeId","ConsultantDocId","RefDocId","Opdno","TariffId","CompanyId","AddedBy","UpdatedBy","IsCancelledBy","IsCancelled","IsCancelledDate", "ClassId", "DepartmentId","PatientOldNew","FirstFollowupVisit","AppPurposeId", "IsMark", "Comments", "IsXray", "CrossConsulFlag", "PhoneAppId", "Height","Pweight","Bmi","Bsl","SpO2", "Temp", "Pulse", "Bp"};
                var VEntity = ObjVisitDetail.ToDictionary();
                foreach (var rProperty in VDetailEntity)
                {
                    VEntity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("m_Update_VisitFollowupDate", CommandType.StoredProcedure, VEntity);

            }
            foreach (var item in objTOprequestList)
            {

                string[] rDetailEntity = { "RequestTranId", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn" };

                var PrescriptionEntity = item.ToDictionary();
                foreach (var rProperty in rDetailEntity)
                {
                    PrescriptionEntity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_Insert_T_OPRequestList", CommandType.StoredProcedure, PrescriptionEntity);

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
        //public virtual async Task<List<OPrtrvDignosisListDto>> GetOPrtrvDignosisList(string DescriptionType)
        //{
        //    //  select DescriptionName, DescriptionType from M_OPCasepaperDignosisMaster
        //    //Where DescriptionType = @DescriptionType


        //    var qry = (from MOpcasepaperDignosisMaster in _context.MOpcasepaperDignosisMasters
        //                   //Where DescriptionType = @DescriptionType
        //               where (string.IsNullOrEmpty(DescriptionType) || MOpcasepaperDignosisMaster.DescriptionType.Contains(DescriptionType))
        //                orderby MOpcasepaperDignosisMaster.Id


        //               select new OPrtrvDignosisListDto
        //               {

        //                   //StoreId = assignItem != null ? assignItem.StoreId : 0,
        //                   DescriptionType = MOpcasepaperDignosisMaster.DescriptionType,
        //                   DescriptionName = MOpcasepaperDignosisMaster.DescriptionName


        //               });

        //    return await qry.Take(50).ToListAsync();
        //}
       
    }
}
