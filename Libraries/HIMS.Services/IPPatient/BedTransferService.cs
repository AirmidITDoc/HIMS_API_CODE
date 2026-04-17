using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System.Data;

namespace HIMS.Services.IPPatient
{
    public class BedTransferService : IBedTransferService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public BedTransferService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<BedTransferDetailListDto>> BedTransferDetailList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<BedTransferDetailListDto>(model, "ps_Rtrv_BedTransferDetails");
        }
        public virtual async Task<IPagedList<BedTransferInformationListDto>> BedTransferInformationList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<BedTransferInformationListDto>(model, "ps_BedTransferInformation_list");
        }
        public virtual async Task InsertSP(TBedTransferDetail objBedTransferDetail, Bedmaster ObjBedMaster, Bedmaster ObjBedMasterUpdate, Admission ObjAdmission, int CurrentUserId, string CurrentUserName)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "AdmissionId","FromDate","FromTime","FromWardId","FromBedId","FromClassId","ToDate","ToTime","ToWardId","ToBedId","ToClassId","Remark","AddedBy","IsCancelled","IsCancelledBy"};
            var Entity = objBedTransferDetail.ToDictionary();
            foreach (var rProperty in Entity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
                    Entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_Insert_BedTransferDetails_1", CommandType.StoredProcedure, Entity);
            _ = Task.Run(() => _context.LogProcedureExecution(Entity, nameof(TBedTransferDetail), Convert.ToInt32(objBedTransferDetail.TransferId), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName));


            string[] rBedEntity = { "BedId"};
            var rbedentity = ObjBedMaster.ToDictionary();

            foreach (var rProperty in rbedentity.Keys.ToList())
            {
                if (!rBedEntity.Contains(rProperty))
                    rbedentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_M_BedMasterTofreebed", CommandType.StoredProcedure, rbedentity);
            _ = Task.Run(() => _context.LogProcedureExecution(Entity, nameof(Bedmaster), Convert.ToInt32(ObjBedMaster.BedId), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName));


            string[] BedEntity = {"BedId"};
            var bedentity = ObjBedMasterUpdate.ToDictionary();

            foreach (var rProperty in bedentity.Keys.ToList())
            {
                if (!BedEntity.Contains(rProperty))
                    bedentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_BedMaster", CommandType.StoredProcedure, bedentity);
            _ = Task.Run(() => _context.LogProcedureExecution(Entity, nameof(Bedmaster), Convert.ToInt32(ObjBedMasterUpdate.BedId), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName));


            string[] AEntity = { "AdmissionId", "BedId", "WardId", "ClassId"};
            var aentity = ObjAdmission.ToDictionary();

            foreach (var rProperty in aentity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    aentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_Update_AdmissionforBedMaster", CommandType.StoredProcedure, aentity);
            _ = Task.Run(() => _context.LogProcedureExecution(Entity, nameof(Admission), Convert.ToInt32(ObjAdmission.AdmissionId), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName));


        }

    }
}






