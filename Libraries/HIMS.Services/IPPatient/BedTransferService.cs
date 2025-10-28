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
        public virtual void InsertSP(TBedTransferDetail objBedTransferDetail, Bedmaster ObjBedMaster, Bedmaster ObjBedMasterUpdate, Admission ObjAdmission, int CurrentUserId, string CurrentUserName)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "TransferId" };
            var Entity = objBedTransferDetail.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                Entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_Insert_BedTransferDetails_1", CommandType.StoredProcedure, Entity);

            //throw new NotImplementedException();
            string[] rBedEntity = { "BedName", "RoomId", "IsAvailible", "IsActive", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" };
            var rbedentity = ObjBedMaster.ToDictionary();
            foreach (var rProperty in rBedEntity)
            {
                rbedentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_M_BedMasterTofreebed", CommandType.StoredProcedure, rbedentity);

            string[] BedEntity = { "BedName", "RoomId", "IsAvailible", "IsActive", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" };
            var bedentity = ObjBedMasterUpdate.ToDictionary();
            foreach (var rProperty in BedEntity)
            {
                bedentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_BedMaster", CommandType.StoredProcedure, bedentity);

            string[] AEntity = { "RegId", "AdmissionDate", "AdmissionTime", "PatientTypeId", "HospitalId", "DocNameId", "RefDocNameId", "DischargeDate", "DischargeTime", "IsDischarged", "IsBillGenerated", "Ipdno", "IsCancelled", "CompanyId", "TariffId", "DepartmentId",
                "RelativeName","RelativeAddress","PhoneNo","MobileNo","RelationshipId","AddedBy","IsMlc","MotherName","AdmittedDoctor1","AdmittedDoctor2","IsProcessing","Ischarity","RefByTypeId","RefByName","IsMarkForDisNur","IsMarkForDisNurId","IsMarkForDisNurDateTime",
                "IsCovidFlag","IsCovidUserId","IsCovidUpdateDate","IsUpdatedBy","SubTpaComId","PolicyNo","AprovAmount","CompDod","IsPharClearance","Ipnumber","EstimatedAmount","ApprovedAmount","HosApreAmt","PathApreAmt","PharApreAmt","RadiApreAmt",
                "PharDisc","CompBillNo","CompBillDate","CompDiscount","CompDisDate","CBillNo","CFinalBillAmt","CDisallowedAmt","ClaimNo","HdiscAmt","COutsideInvestAmt","RecoveredByPatient",
                "HChargeAmt","HAdvAmt","HBillId","HBillDate","HBillNo","HTotalAmt","HDiscAmt1","HNetAmt","HPaidAmt","HBalAmt","IsOpToIpconv","RefDoctorDept","AdmissionType","MedicalApreAmt","AdminPer","AdminAmt","SubTpacomp","IsCtoH","IsInitinatedDischarge","ConvertId","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate"};
            var aentity = ObjAdmission.ToDictionary();
            foreach (var rProperty in AEntity)
            {
                aentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_Update_AdmissionforBedMaster", CommandType.StoredProcedure, aentity);

        }

    }
}






