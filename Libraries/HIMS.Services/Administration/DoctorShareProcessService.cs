using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Transactions;

namespace HIMS.Services.Administration
{
    public class DoctorShareProcessService : IDoctorShareProcessService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public DoctorShareProcessService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual void DoctorShareInsert(AddCharge ObjAddCharges, int UserId, string Username, DateTime FromDate, DateTime ToDate)
        {
            DatabaseHelper odal = new();
            string[] AEntity = {  "ChargesId","ChargesDate","OpdIpdType","OpdIpdId","ServiceId","Price","Qty","TotalAmt","ConcessionPercentage","ConcessionAmount","NetAmount","DoctorId", "DocPercentage","DocAmt","HospitalAmt","IsGenerated","AddedBy","IsCancelled","IsCancelledBy","IsCancelledDate","IsPathology","IsRadiology",
            "IsDoctorShareGenerated","IsInterimBillFlag","IsPackage","IsSelfOrCompanyService","PackageId","ChargesTime","PackageMainChargeId","ClassId","RefundAmount","CPrice","CQty","CTotalAmount","IsComServ","IsPrintCompSer","ServiceName","ChPrice","ChQty","ChTotalAmount","IsBillableCharity","SalesId","BillNo","IsHospMrk","BillNoNavigation","UnitId","TariffId","DoctorName","WardId","BedId","ServiceCode","CompanyServiceName","IsInclusionExclusion","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate"};
            var Rentity = ObjAddCharges.ToDictionary();
            foreach (var rProperty in AEntity)
            {
                Rentity.Remove(rProperty);
            }
            Rentity["FromDate"] = FromDate;
            Rentity["ToDate"] = ToDate;

            odal.ExecuteNonQuery("OP_DoctorSharePerCalculation_1", CommandType.StoredProcedure, Rentity);
            odal.ExecuteNonQuery("IP_DoctorSharePerCalculation_1", CommandType.StoredProcedure, Rentity);

        }
        public virtual async Task InsertAsync(MDoctorPerMaster ObjMDoctorPerMaster, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.MDoctorPerMasters.Add(ObjMDoctorPerMaster);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync(MDoctorPerMaster ObjMDoctorPerMaster, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.MDoctorPerMasters.Update(ObjMDoctorPerMaster);
                _context.Entry(ObjMDoctorPerMaster).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
    }
}
