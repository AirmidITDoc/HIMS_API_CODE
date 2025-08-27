using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OTManagment
{
    public class EmergencyService : IEmergencyService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public EmergencyService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<EmergencyListDto>> GetListAsyn(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<EmergencyListDto>(model, "ps_Rtrv_Emergency_list");
        }
        public virtual async Task InsertAsyncSP(TEmergencyAdm objTEmergencyAdm, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] Entity = { "SeqNo", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "CreatedDate", "ModifiedBy", "ModifiedDate", "IsConverted" };
            var entity = objTEmergencyAdm.ToDictionary();
            foreach (var rProperty in Entity)
            {
                entity.Remove(rProperty);
            }
          
            string VEmgId = odal.ExecuteNonQuery("ps_insert_T_EmergencyAdm_1", CommandType.StoredProcedure, "EmgId", entity);
            objTEmergencyAdm.EmgId = Convert.ToInt32(VEmgId);
        }
        public virtual async Task UpdateSP(TEmergencyAdm objTEmergencyAdm, int UserId, string UserName)
        {
            DatabaseHelper odal = new();
            string[] UEntity = { "SeqNo", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "CreatedBy", "CreatedDate", "ModifiedDate", "IsConverted" };
            var Entity = objTEmergencyAdm.ToDictionary();
            foreach (var rProperty in UEntity)
            {
                Entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_T_EmergencyAdm_1", CommandType.StoredProcedure, Entity);
        }
        public virtual async Task CancelSP(TEmergencyAdm objTEmergencyAdm, int UserId, string UserName)
        {
            DatabaseHelper odal = new();
            string[] CEntity = { "RegId", "EmgDate", "EmgTime", "SeqNo", "FirstName", "MiddleName", "LastName", "Address", "MobileNo", "DepartmentId", "DoctorId", "IsCancelled", "IsCancelledDate", "PrefixId", "AgeYear", "GenderId","CityId",
                "IsCancelledDate", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate","StateId","CountryId","DateofBirth","AgeMonth","AgeDay","Comment","Classid","Tariffid","RefDoctorId","AttendingDoctorId","IsMlc","IsConverted","ClassId","TariffId"};
            var Entity = objTEmergencyAdm.ToDictionary();
            foreach (var rProperty in CEntity)
            {
                Entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("PS_Cancel_T_EmergencyAdm_1", CommandType.StoredProcedure, Entity);
        }
        public virtual async Task Update(AddCharge ObjAddCharge, int UserId, string UserName,long EmgId,long NewAdmissionId)
        {
            DatabaseHelper odal = new();
            string[] UEntity = { "ChargesId", "ChargesDate", "OpdIpdType", "OpdIpdId", "ServiceId", "Qty", "TotalAmt", "ConcessionPercentage", "ConcessionAmount", "NetAmount", "DoctorId", "DocPercentage", "DocAmt", "HospitalAmt", "IsGenerated", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "IsPathology", "IsRadiology", "IsDoctorShareGenerated", "IsInterimBillFlag", "IsPackage", "IsSelfOrCompanyService", "PackageId", "ChargesTime", "PackageMainChargeId", "ClassId", "RefundAmount", 
                "CPrice","ChPrice", "CQty", "CTotalAmount", "IsComServ", "IsPrintCompSer","ServiceName","ChPrice","ChQty","ChTotalAmount","IsBillableCharity","SalesId","BillNo","IsHospMrk","BillNoNavigation","Price" };
            var Entity = ObjAddCharge.ToDictionary();
            foreach (var rProperty in UEntity)
            {
                Entity.Remove(rProperty);

            }
            Entity["EmgId"] = EmgId;
            Entity["NewAdmissionId"] = NewAdmissionId;
            odal.ExecuteNonQuery("pd_UpdateAddChargesFromEmergency", CommandType.StoredProcedure, Entity);
        }
    }
}






