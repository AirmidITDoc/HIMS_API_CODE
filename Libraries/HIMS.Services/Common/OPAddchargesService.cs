using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System.Data;

namespace HIMS.Services.Common
{
    public class OPAddchargesService : IOPAddchargesService
    {
        private readonly HIMSDbContext _context;
        public OPAddchargesService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual void DeleteAsyncSP(AddCharge objAddcharges, int currentUserId, string currentUserName)
        {
            DatabaseHelper odal = new();

            string[] rEntity = { "ChargesDate", "OpdIpdType", "OpdIpdId", "ServiceId", "Price", "Qty", "TotalAmt", "ConcessionPercentage", "ConcessionAmount", "NetAmount", "DoctorId", "DocPercentage", "DocAmt", "HospitalAmt", "IsGenerated", "AddedBy", "IsCancelled", "IsCancelledDate", "IsPathology", "IsRadiology", "PackageMainChargeId", "IsPackage", "IsSelfOrCompanyService", "PackageId", "ChargesTime", "ClassId", "IsDoctorShareGenerated", "IsInterimBillFlag", "RefundAmount", "CPrice", "CQty", "CTotalAmount", "IsComServ", "IsPrintCompSer", "ServiceName", "ChPrice", "ChQty", "ChTotalAmount", "IsBillableCharity", "SalesId", "BillNo", "BillNoNavigation" };
            var entity = objAddcharges.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("Delete_IPAddcharges", CommandType.StoredProcedure, entity);

        }

        public virtual void InsertSP(AddCharge objAddcharges, int currentUserId, string currentUserName)
        {

            //throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] rEntity = { "IsDoctorShareGenerated", "IsInterimBillFlag", "RefundAmount", "CPrice", "CQty", "CTotalAmount", "IsComServ", "IsPrintCompSer", "ServiceName", "ChPrice", "ChQty", "ChTotalAmount", "IsBillableCharity", "SalesId", "BillNo", "BillNoNavigation" };
            var entity = objAddcharges.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string ChargesId = odal.ExecuteNonQuery("v_insert_IPAddCharges_1", CommandType.StoredProcedure, "ChargesId", entity);

        }
    }
}
