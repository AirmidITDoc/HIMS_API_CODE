using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HIMS.Services.OutPatient
{
   public class OPAddchargesService:IOPAddchargesService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public OPAddchargesService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task DeleteAsyncSP(AddCharge objAddcharges, int currentUserId, string currentUserName)
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

        public virtual async Task InsertAsyncSP(AddCharge objAddcharges, int currentUserId, string currentUserName)
        {
           
            //throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] rEntity = { "IsDoctorShareGenerated", "IsInterimBillFlag", "RefundAmount", "CPrice", "CQty", "CTotalAmount", "IsComServ", "IsPrintCompSer", "ServiceName", "ChPrice", "ChQty", "ChTotalAmount", "IsBillableCharity", "SalesId", "BillNo", "BillNoNavigation" };
            var entity = objAddcharges.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string ChargesId = odal.ExecuteNonQuery("N_insert_IPAddCharges_1", CommandType.StoredProcedure, "ChargesId", entity);
            
        }
    }
}
