using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data;

namespace HIMS.Services.Pharmacy
{
    public  class InPatientService : IInPatientService
    {

        private readonly Data.Models.HIMSDbContext _context;
        public InPatientService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<SalesBillListDto>> salesbrowselist(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SalesBillListDto>(model, "ps_Rtrv_SalesInPatientBillList");
        }
        public virtual async Task<IPagedList<InPatientSalesDetailsListDto>> Getsalesdetaillist(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<InPatientSalesDetailsListDto>(model, "ps_Rtrv_SalesInPatientDetails");
        }
        public virtual async Task<IPagedList<SalesReturnBillListDto>> salesreturnlist(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SalesReturnBillListDto>(model, "ps_Rtrv_SalesInPatientReturnBillList");
        }
        public virtual async Task<IPagedList<SalesInPatientReturnDetailsListDto>> salesreturndetaillist(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SalesInPatientReturnDetailsListDto>(model, "ps_Rtrv_InPatientSalesReturnDetails");
        }
        public virtual async Task<float> GetStock(long StockId)
        {
            TCurrentStock objStock = await _context.TCurrentStocks.FirstOrDefaultAsync(x => x.StockId == StockId);
            return (objStock?.BalanceQty ?? 0) - (objStock?.GrnRetQty ?? 0);
        }
        public virtual async Task InsertSalesInPatientAsyncSPC(TSalesInpatientHeader ObjSalesHeader, List<TCurrentStock> ObjTCurrentStock, TIpPrescription ObjPrescription, TSalesDraftHeader ObjDraftHeader, int CurrentUserId, string CurrentUserName)
        {
            // Open transaction
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection());
                odal.SetTransaction(transaction.GetDbTransaction());

                //  Insert Sales Header
                string[] AEntity = { "Date", "Time", "OpIpId", "OpIpType", "TotalAmount", "VatAmount", "DiscAmount", "NetAmount", "PaidAmount", "BalanceAmount", "ConcessionReasonId", "ConcessionAuthorizationId", "IsSellted", "IsPrint", "IsFree", "UnitId",
                    "ExternalPatientName", "DoctorName", "StoreId", "IsPrescription", "AddedBy", "CreditReason", "CreditReasonId", "WardId", "BedId", "DiscperH", "IsPurBill", "IsBillCheck", "SalesHeadName", "SalesTypeId", "RegId", "ExtMobileNo", "RoundOff",
                    "ExtAddress", "SalesId" /*TSalesDetails*/ };
                var entity = ObjSalesHeader.ToDictionary();

                foreach (var rProperty in entity.Keys.ToList())
                {
                    if (!AEntity.Contains(rProperty))
                        entity.Remove(rProperty);
                }

                string SalesId = odal.ExecuteNonQueryNew("ps_insert_T_SalesInpatientHeader_1", CommandType.StoredProcedure, "SalesId", entity);
                ObjSalesHeader.SalesId = Convert.ToInt32(SalesId);
                await _context.LogProcedureExecution(entity, nameof(TSalesInpatientHeader), (int)ObjSalesHeader.SalesId, Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);


                // 2️⃣ Insert Sales Details (EF)
                foreach (var item in ObjSalesHeader.TSalesInpatientDetails)
                    item.SalesId = ObjSalesHeader.SalesId;

                _context.TSalesInpatientDetails.AddRange(ObjSalesHeader.TSalesInpatientDetails);
                await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);


                // 3️⃣ Update Current Stock (SP)
                foreach (var items in ObjTCurrentStock)
                {
                    string[] SEntity = { "ItemId", "IssueQty", "IstkId", "StoreId" };
                    var IIentity = items.ToDictionary();
                    foreach (var rProperty in IIentity.Keys.ToList())
                    {
                        if (!SEntity.Contains(rProperty))
                            IIentity.Remove(rProperty);
                    }

                    odal.ExecuteNonQueryNew("ps_Update_T_CurStk_Sales_Id_1", CommandType.StoredProcedure, "", IIentity);
                    await _context.LogProcedureExecution(IIentity,  nameof(TCurrentStock),  (int)items.StockId,   Core.Domain.Logging.LogAction.Add,   CurrentUserId, CurrentUserName );
                }

                var SalesIdObj = new { ObjSalesHeader.SalesId };
                odal.ExecuteNonQueryNew("ps_Cal_DiscAmount_SalesInpatientHeader", CommandType.StoredProcedure, "", SalesIdObj.ToDictionary());
                odal.ExecuteNonQueryNew("ps_Cal_GSTAmount_SalesInpatientHeader", CommandType.StoredProcedure, "", SalesIdObj.ToDictionary());

                // 5️⃣ Update Prescription
                string[] TEntity = { "OpIpId", "IsClosed" };
                var Nentity = ObjPrescription.ToDictionary();
                foreach (var rProperty in Nentity.Keys.ToList())
                {
                    if (!TEntity.Contains(rProperty))
                        Nentity.Remove(rProperty);
                }

                odal.ExecuteNonQueryNew("ps_Update_T_IPPrescription_Isclosed_Status_1", CommandType.StoredProcedure, "", Nentity);
                await _context.LogProcedureExecution(entity, nameof(TIpPrescription), (int)ObjPrescription.IppreId, Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);


                // 6️⃣ Update Draft Header
                string[] DEntity = { "DsalesId", "IsClosed" };
                var Hentity = ObjDraftHeader.ToDictionary();
                foreach (var rProperty in Hentity.Keys.ToList())
                {
                    if (!DEntity.Contains(rProperty))
                        Hentity.Remove(rProperty);
                }
                odal.ExecuteNonQueryNew("ps_Update_T_SalDraHeader_IsClosed_1", CommandType.StoredProcedure, "", Hentity);
                await _context.LogProcedureExecution(entity, nameof(TSalesDraftHeader), (int)ObjDraftHeader.DsalesId, Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);


                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                //Rollback on error
                await transaction.RollbackAsync();
                throw;
            }
        }
        public virtual async Task InsertInPatient(TSalesInPatientReturnHeader ObjTSalesReturnHeader, List<TSalesInPatientReturnDetail> ObjTSalesReturnDetail, List<TCurrentStock> ObjTCurrentStock, List<TSalesDetail> ObjTSalesDetail, List<TIpprescriptionReturnH> ObjTIpprescriptionReturnH, TIpprescriptionReturnD ObjTIpprescriptionReturnD, int CurrentUserId, string CurrentUserName)
        {
            // //Add header table records
            DatabaseHelper odal = new();
            string[] Entity = { "SalesReturnId", "Date", "Time", "SalesId", "OpIpId", "OpIpType", "TotalAmount", "VatAmount", "DiscAmount", "NetAmount", "PaidAmount", "BalanceAmount", "IsSellted", "IsPrint", "IsFree", "UnitId", "AddedBy", "StoreId", "Narration", "IsPurBill", "IsPrescriptionReturn" };
            var entity = ObjTSalesReturnHeader.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!Entity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            string vSalesReturnId = odal.ExecuteNonQuery("ps_insert_T_SalesInPatientReturnHeader_1", CommandType.StoredProcedure, "SalesReturnId", entity);
            ObjTSalesReturnHeader.SalesReturnId = Convert.ToInt32(vSalesReturnId);
            //await _context.LogProcedureExecution(entity, nameof(TSalesInPatientReturnHeader), (int)ObjTSalesReturnHeader.SalesReturnId, Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);
            _ = Task.Run(() => _context.LogProcedureExecution(entity, nameof(TSalesInPatientReturnHeader), (int)ObjTSalesReturnHeader.SalesReturnId, Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName));


            foreach (var item in ObjTSalesReturnDetail)
            {
                item.SalesReturnId = Convert.ToInt32(vSalesReturnId);

                string[] TEntity = { "SalesReturnId", "ItemId", "BatchNo", "BatchExpDate", "UnitMrp", "Qty", "TotalAmount", "VatPer", "VatAmount", "DiscPer", "DiscAmount", "GrossAmount", "LandedPrice", "TotalLandedAmount", "PurRate", "PurTot", "SalesId", "SalesDetId", "IsCashOrCredit", "Cgstper", "Cgstamt", "Sgstper", "Sgstamt", "Igstper", "Igstamt", "StkId" };
                var Aentity = item.ToDictionary();
                foreach (var rProperty in Aentity.Keys.ToList())
                {
                    if (!TEntity.Contains(rProperty))
                        Aentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_insert_T_SalesInPatientReturnDetails_1", CommandType.StoredProcedure, Aentity);
                _ = Task.Run(() => _context.LogProcedureExecution(Aentity, nameof(TSalesInPatientReturnDetail), (int)item.SalesReturnDetId, Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName));

            }


            foreach (var item in ObjTCurrentStock)
            {
                string[] REntity = { "ItemId", "IssueQty", "StoreId", "IstkId" };
                var Pentity = item.ToDictionary();
                foreach (var rProperty in Pentity.Keys.ToList())
                {
                    if (!REntity.Contains(rProperty))
                        Pentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_Update_T_CurStk_SalesReturn_Id_1", CommandType.StoredProcedure, Pentity);
                _ = Task.Run(() => _context.LogProcedureExecution(Pentity, nameof(TCurrentStock), (int)item.StockId, Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName));


            }

            foreach (var item in ObjTSalesDetail)
            {
                string[] REntity = { "SalesDetId", "ReturnQty" };
                var Pentity = item.ToDictionary();
                foreach (var rProperty in Pentity.Keys.ToList())
                {
                    if (!REntity.Contains(rProperty))
                        Pentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_Update_SalesInPatientReturnQty_SalesTbl_1", CommandType.StoredProcedure, Pentity);
                _ = Task.Run(() => _context.LogProcedureExecution(Pentity, nameof(TSalesDetail), (int)item.SalesDetId, Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName));


            }
            var SalesReturnIdObj = new
            {
                SalesReturnId = Convert.ToInt32(vSalesReturnId)
            };
            odal.ExecuteNonQuery("ps_Update_SalesInPatientRefundAmt_SalesInPatientHeader", CommandType.StoredProcedure, SalesReturnIdObj.ToDictionary());


            var SalesReturnsIdObj = new
            {
                SalesReturnId = Convert.ToInt32(vSalesReturnId)
            };
            odal.ExecuteNonQuery("ps_Cal_GSTAmount_SalesInPatientReturn", CommandType.StoredProcedure, SalesReturnsIdObj.ToDictionary());
            await _context.LogProcedureExecution(entity, nameof(TSalesInPatientReturnHeader), (int)ObjTSalesReturnHeader.SalesReturnId, Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);
            var SalesReturnObj = new
            {
                Id = Convert.ToInt32(vSalesReturnId),
                TypeId = 2

            };
            odal.ExecuteNonQuery("ps_Insert_ItemMovementReport_InpatientReturnCursor", CommandType.StoredProcedure, SalesReturnObj.ToDictionary());
            _ = Task.Run(() => _context.LogProcedureExecution(entity, nameof(TSalesInPatientReturnHeader), (int)ObjTSalesReturnHeader.SalesReturnId, Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName));

            foreach (var item in ObjTIpprescriptionReturnH)
            {
                string[] PEntity = { "PresReId", "PresDetailsId" };
                var Pentity = item.ToDictionary();
                foreach (var rProperty in Pentity.Keys.ToList())
                {
                    if (!PEntity.Contains(rProperty))
                        Pentity.Remove(rProperty);
                }
                Pentity["PresDetailsId"] = ObjTIpprescriptionReturnD.PresDetailsId;

                odal.ExecuteNonQuery("ps_IPPrescriptionReturnUpdate", CommandType.StoredProcedure, Pentity);
                _ = Task.Run(() => _context.LogProcedureExecution(entity, nameof(TIpprescriptionReturnH), (int)item.PresReId, Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName));


            }
        }
    }
}
