using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Linq;
using System.Transactions;

namespace HIMS.Services.Pharmacy
{
    public class GRNService : IGRNService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public GRNService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<ItemDetailsForGRNUpdateListDto>> GRNUpdateList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<ItemDetailsForGRNUpdateListDto>(model, "m_Rtrv_ItemDetailsForGRNUpdate");
        }
        public virtual async Task<IPagedList<GRNListDto>> GRNHeaderList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<GRNListDto>(model, "m_Rtrv_GRNList_by_Name");
        }
        public virtual async Task<IPagedList<GRNDetailsListDto>> GRNDetailsList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<GRNDetailsListDto>(model, "ps_Rtrv_GrnItemList");
        }

        public virtual async Task<IPagedList<DirectPOListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DirectPOListDto>(model, "Rtrv_DirectPOList_by_Name");
        }

        public virtual async Task<IPagedList<InvoiceNoChecListDto>> InvoiceNoChecList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<InvoiceNoChecListDto>(model, "ps_m_grnInvoiceno_check");
        }
        public virtual async Task<IPagedList<PoDetailListDto>> GetListAsync1(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PoDetailListDto>(model, "m_Rtrv_ItemList_by_Supplier_Name");
        }

        public virtual async Task<TGrnheader> GetById(int Id)
        {
            return await this._context.TGrnheaders.Include(x => x.TGrndetails).FirstOrDefaultAsync(x => x.Grnid == Id);

        }
        public virtual async Task InsertAsyncSP(TGrnheader objGRN, List<MItemMaster> objItems, int UserId, string Username)
        {
            // Add header table records
            DatabaseHelper odal = new();
            SqlParameter[] para = new SqlParameter[32];
            para[0] = new SqlParameter("@GRNDate", objGRN.Grndate);
            para[1] = new SqlParameter("@GRNTime", objGRN.Grntime);
            para[2] = new SqlParameter("@StoreId", objGRN.StoreId);
            para[3] = new SqlParameter("@SupplierID", objGRN.SupplierId);
            para[4] = new SqlParameter("@InvoiceNo", objGRN.InvoiceNo);
            para[5] = new SqlParameter("@DeliveryNo", objGRN.DeliveryNo);
            para[6] = new SqlParameter("@GateEntryNo", objGRN.GateEntryNo);
            para[7] = new SqlParameter("@Cash_CreditType", objGRN.CashCreditType);
            para[8] = new SqlParameter("@GRNType", objGRN.Grntype);
            para[9] = new SqlParameter("@TotalAmount", objGRN.TotalAmount);
            para[10] = new SqlParameter("@TotalDiscAmount", objGRN.TotalDiscAmount);

            para[11] = new SqlParameter("@TotalVATAmount", objGRN.TotalVatamount);
            para[12] = new SqlParameter("@NetAmount", objGRN.NetAmount);
            para[13] = new SqlParameter("@Remark", objGRN.Remark);
            para[14] = new SqlParameter("@ReceivedBy", objGRN.ReceivedBy);
            para[15] = new SqlParameter("@IsVerified", objGRN.IsVerified);
            para[16] = new SqlParameter("@IsClosed", objGRN.IsClosed);
            para[17] = new SqlParameter("@AddedBy", objGRN.AddedBy ?? 0);
            para[18] = new SqlParameter("@InvDate", objGRN.InvDate);
            para[19] = new SqlParameter("@DebitNote", objGRN.DebitNote);
            para[20] = new SqlParameter("@CreditNote", objGRN.CreditNote);

            para[21] = new SqlParameter("@OtherCharge", objGRN.OtherCharge);
            para[22] = new SqlParameter("@RoundingAmt", objGRN.RoundingAmt);
            para[23] = new SqlParameter("@TotCGSTAmt", objGRN.TotCgstamt);
            para[24] = new SqlParameter("@TotSGSTAmt", objGRN.TotSgstamt);
            para[25] = new SqlParameter("@TotIGSTAmt", objGRN.TotIgstamt);
            para[26] = new SqlParameter("@TranProcessId", objGRN.TranProcessId);
            para[27] = new SqlParameter("@TranProcessMode", objGRN.TranProcessMode);
            para[28] = new SqlParameter("@BillDiscAmt", objGRN.BillDiscAmt);
            para[29] = new SqlParameter("@EwayBillNo", objGRN.EwayBillNo);
            para[30] = new SqlParameter("@EwayBillDate", objGRN.EwayBillDate);
            para[31] = new SqlParameter("@GRNID", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.Output
            };

            string grnNo = odal.ExecuteNonQuery("m_insert_GRNHeader_PurNo_1_New", CommandType.StoredProcedure, "@GRNID", para);
            objGRN.Grnid = Convert.ToInt32(grnNo);

            // Add details table records
            foreach (var objItem in objGRN.TGrndetails)
            {
                objItem.Grnid = objGRN.Grnid;
            }
            _context.TGrndetails.AddRange(objGRN.TGrndetails);

            //// Update item master table records
            //_context.MItemMasters.UpdateRange(objItems);


          
            await _context.SaveChangesAsync();
        }

        public virtual async Task InsertAsync(TGrnheader objGRN, List<MItemMaster> objItems, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update store table records
                MStoreMaster StoreInfo = await _context.MStoreMasters.FirstOrDefaultAsync(x => x.StoreId == objGRN.StoreId);
                StoreInfo.GrnNo = Convert.ToString(Convert.ToInt32(StoreInfo.GrnNo) + 1);
                _context.MStoreMasters.Update(StoreInfo);
                await _context.SaveChangesAsync();

                // Add header & detail table records
                objGRN.GrnNumber = StoreInfo.GrnNo;
                _context.TGrnheaders.Add(objGRN);
                await _context.SaveChangesAsync();

                //// Update item master table records
                //_context.MItemMasters.UpdateRange(objItems);
                //await _context.SaveChangesAsync();
                //scope.Complete();

                // Update item master table records (only specific fields)
                foreach (var item in objItems)
                {
                    _context.MItemMasters.Attach(item);

                    _context.Entry(item).Property(x => x.Hsncode).IsModified = true;
                    _context.Entry(item).Property(x => x.ConversionFactor).IsModified = true;
                    _context.Entry(item).Property(x => x.Cgst).IsModified = true;
                    _context.Entry(item).Property(x => x.Sgst).IsModified = true;
                    _context.Entry(item).Property(x => x.Igst).IsModified = true;
                }

                await _context.SaveChangesAsync();
                scope.Complete();


            }
        }

        public virtual async Task UpdateAsync(TGrnheader objGRN, List<MItemMaster> objItems, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Delete details table realted records
                var lst = await _context.TGrndetails.Where(x => x.Grnid == objGRN.Grnid).ToListAsync();
                _context.TGrndetails.RemoveRange(lst);

                // Update header & detail table records
                _context.TGrnheaders.Update(objGRN);
                _context.Entry(objGRN).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        //public virtual async Task InsertWithPOAsync(TGrnheader objGRN, List<MItemMaster> objItems, List<TPurchaseDetail> objPurDetails, List<TPurchaseHeader> objPurHeaders, int UserId, string Username)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //    {
        //        // Update store table records
        //        MStoreMaster StoreInfo = await _context.MStoreMasters.FirstOrDefaultAsync(x => x.StoreId == objGRN.StoreId);
        //        StoreInfo.GrnNo = Convert.ToString(Convert.ToInt32(StoreInfo.GrnNo) + 1);
        //        _context.MStoreMasters.Update(StoreInfo);
        //        await _context.SaveChangesAsync();

        //        // Add header & detail table records
        //        objGRN.GrnNumber = StoreInfo.GrnNo;
        //        _context.TGrnheaders.Add(objGRN);
        //        await _context.SaveChangesAsync();

        //        // Update item master table records
        //        _context.MItemMasters.UpdateRange(objItems);
        //        await _context.SaveChangesAsync();

        //        // Update purchase details table records
        //        List<TPurchaseDetail> objPurDetailsList = new();
        //        foreach (var objDet in objPurDetails)
        //        {
        //            TPurchaseDetail DetailsInfo = await _context.TPurchaseDetails.FirstOrDefaultAsync(x => x.PurchaseId == objDet.PurchaseId && x.PurDetId == objDet.PurDetId);
        //            //  if (DetailsInfo != null)
        //            //   {
        //            DetailsInfo.PobalQty = objDet.PobalQty;
        //            DetailsInfo.IsClosed = objDet.IsClosed;
        //            DetailsInfo.IsGrnQty = DetailsInfo.Qty - objDet.PobalQty;
        //            objPurDetailsList.Add(DetailsInfo);

        //        }
        //        _context.TPurchaseDetails.UpdateRange(objPurDetailsList);
        //        _context.Entry(objPurDetailsList).State = EntityState.Modified;
        //        await _context.SaveChangesAsync();

        //        // Update purchase header table records
        //        List<TPurchaseHeader> objPurHeadersList = new();
        //        foreach (var objHed in objPurHeaders)
        //        {
        //            TPurchaseHeader HeaderInfo = await _context.TPurchaseHeaders.FirstOrDefaultAsync(x => x.PurchaseId == objHed.PurchaseId);
        //            HeaderInfo.Isclosed = objHed.Isclosed;
        //            objPurHeadersList.Add(HeaderInfo);
        //        }
        //        _context.TPurchaseHeaders.UpdateRange(objPurHeadersList);
        //        _context.Entry(objPurHeadersList).State = EntityState.Modified;
        //        await _context.SaveChangesAsync();

        //        scope.Complete();
        //    }
        //}
        public virtual async Task InsertWithPOAsync(TGrnheader objGRN, List<MItemMaster> objItems, List<TPurchaseDetail> objPurDetails, List<TPurchaseHeader> objPurHeaders, int UserId, string Username)

            {        
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            // Update store table records
            MStoreMaster StoreInfo = await _context.MStoreMasters.FirstOrDefaultAsync(x => x.StoreId == objGRN.StoreId);
            StoreInfo.GrnNo = Convert.ToString(Convert.ToInt32(StoreInfo.GrnNo) + 1);
            _context.MStoreMasters.Update(StoreInfo);
            await _context.SaveChangesAsync();

            // Add header & detail table records
            objGRN.GrnNumber = StoreInfo.GrnNo;
            _context.TGrnheaders.Add(objGRN);
            await _context.SaveChangesAsync();


            // Update item master table records
            _context.MItemMasters.UpdateRange(objItems);
            await _context.SaveChangesAsync();
            // 4️⃣ Update purchase details
            var objPurDetailsList = new List<TPurchaseDetail>();
            foreach (var objDet in objPurDetails)
            {
                var detailsInfo = await _context.TPurchaseDetails
                    .FirstOrDefaultAsync(x => x.PurchaseId == objDet.PurchaseId && x.PurDetId == objDet.PurDetId);

                if (detailsInfo != null)
                {
                    detailsInfo.PobalQty = objDet.PobalQty;
                    detailsInfo.IsClosed = objDet.IsClosed;
                    detailsInfo.IsGrnQty = detailsInfo.Qty - objDet.PobalQty;
                    objPurDetailsList.Add(detailsInfo);
                }
            }

            if (objPurDetailsList.Any())
                _context.TPurchaseDetails.UpdateRange(objPurDetailsList);

            // 5️⃣ Update purchase headers
            var objPurHeadersList = new List<TPurchaseHeader>();
            foreach (var objHed in objPurHeaders)
            {
                var headerInfo = await _context.TPurchaseHeaders
                    .FirstOrDefaultAsync(x => x.PurchaseId == objHed.PurchaseId);

                if (headerInfo != null)
                {
                    headerInfo.Isclosed = objHed.Isclosed;
                    objPurHeadersList.Add(headerInfo);
                }
            }

            if (objPurHeadersList.Any())
                _context.TPurchaseHeaders.UpdateRange(objPurHeadersList);
            await _context.SaveChangesAsync();

            scope.Complete();
        }


        public virtual async Task UpdateWithPOAsync(TGrnheader objGRN, List<MItemMaster> objItems, List<TPurchaseDetail> objPurDetails, List<TPurchaseHeader> objPurHeaders, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Delete details table realted records
                var lst = await _context.TGrndetails.Where(x => x.Grnid == objGRN.Grnid).ToListAsync();
                _context.TGrndetails.RemoveRange(lst);

                // Update header & detail table records
                _context.TGrnheaders.Update(objGRN);
                _context.Entry(objGRN).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                // Update item master table records
                _context.MItemMasters.UpdateRange(objItems);
                await _context.SaveChangesAsync();

                // Update purchase details table records
                List<TPurchaseDetail> objPurDetailsList = new();
                foreach (var objDet in objPurDetails)
                {
                    TPurchaseDetail DetailsInfo = await _context.TPurchaseDetails.FirstOrDefaultAsync(x => x.PurchaseId == objDet.PurchaseId && x.PurDetId == objDet.PurDetId);
                    DetailsInfo.PobalQty = objDet.PobalQty;
                    DetailsInfo.IsClosed = objDet.IsClosed;
                    DetailsInfo.IsGrnQty = DetailsInfo.Qty - objDet.PobalQty;
                    objPurDetailsList.Add(DetailsInfo);
                }
                _context.TPurchaseDetails.UpdateRange(objPurDetailsList);
                await _context.SaveChangesAsync();

                // Update purchase header table records
                List<TPurchaseHeader> objPurHeadersList = new();
                foreach (var objHed in objPurHeaders)
                {
                    TPurchaseHeader HeaderInfo = await _context.TPurchaseHeaders.FirstOrDefaultAsync(x => x.PurchaseId == objHed.PurchaseId);
                    HeaderInfo.Isclosed = objHed.Isclosed;
                    objPurHeadersList.Add(HeaderInfo);
                }
                _context.TPurchaseHeaders.UpdateRange(objPurHeadersList);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }





        //Changes Done By Ashutosh 19 May 2025 
        public virtual async Task VerifyAsyncSp(TGrnheader objGRN, int CurrentUserId, string CurrentUserName)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "Grnid", "VerifiedBy"};
            var entity = objGRN.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            entity["VerifiedBy"] = CurrentUserId;
            odal.ExecuteNonQuery("m_Update_GRN_Verify_Status_1", CommandType.StoredProcedure, entity);
            await _context.LogProcedureExecution(entity, nameof(TGrnheader), objGRN.Grnid.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);



        }

        public virtual async Task<List<BatchListDTO>> GetExisitingBatchList(int StoreId, int ItemId, string BatchNo)
        {
            var qry = (from cs in _context.TCurrentStocks
                       where (BatchNo == "" || cs.BatchNo.Contains(BatchNo))
                             && cs.ItemId == ItemId
                             && cs.StoreId == StoreId
                             && cs.BalanceQty > 0
                       select new BatchListDTO()
                       {
                           BatchNo = cs.BatchNo,
                           BatchExpDate = cs.BatchExpDate,
                           UnitMRP = cs.UnitMrp,
                           UnitPurRate=cs.PurUnitRateWf,
                           UnitLandedRate=cs.LandedRate,
                           GST=cs.VatPercentage,
                           CGSTPer = cs.Cgstper,
                           SGSTPer = cs.Sgstper,
                           IGSTPer = cs.Igstper,
                           FormattedText = cs.BatchNo + " "
                               + cs.BatchExpDate + " "
                               + cs.PurUnitRateWf
                       });

            var sql = qry.ToQueryString();
            Console.WriteLine(sql);
            return await qry.ToListAsync();

        }

        public virtual async Task UpdateAsyncsp(TGrnheader objGRN, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "GrnNumber",  "StoreId","DeliveryNo","GateEntryNo","CashCreditType",
               "Grntype", "TotalAmount", "TotalDiscAmount", "TotalVatamount",  "NetAmount",
                "Remark","ReceivedBy","IsClosed","IsPaymentProcess","PaymentPrcDate","ProcessDes","DebitNote","CreditNote","OtherCharge",
                "RoundingAmt","PaidAmount","BalAmount","TotCgstamt","TotSgstamt","AddedBy","UpdatedBy","IsVerified","IsCancelled","IsClosed","Prefix","TotIgstamt",
                "TranProcessId","TranProcessMode","BillDiscAmt","PaymentDate","EwayBillNo","EwayBillDate","TGrndetails","TSupPayDets"};
            var entity = objGRN.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_UpdateGrnSupplierDatails", CommandType.StoredProcedure, entity);


        }

        public virtual async Task UpdateAsyncSP(TCurrentStock ObjTCurrentStock, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "OpeningBalance",  "ReceivedQty","IssueQty","BalanceQty","UnitMrp",
               "PurchaseRate", "LandedRate", "VatPercentage", "BatchNo",  "BatchExpDate",
                "PurUnitRate","PurUnitRateWf","Cgstper","Sgstper","Igstper","IstkId","GrnRetQty","IssDeptQty"};
            var entity = ObjTCurrentStock.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_UpdateCurrentStockBarcodeSeqNo", CommandType.StoredProcedure, entity);


        }


        //public virtual async Task<List<BatchListDTO>> GetGSTList(string GSTNo)
        //{
        //    //var gstTypesQuery = from cs in _context.MConstants
        //    //                    where (string.IsNullOrEmpty(GSTNo) || cs.Value.Contains(GSTNo))
        //    //                          && cs.ConstantType == "GST_TYPES"
        //    //                    select new BatchListDTO
        //    //                    {
        //    //                        GST = decimal.TryParse(cs.Value, out var gst) ? gst : 0, // or handle fallback cs.Value,
        //    //                    };

        //    //var sql = gstTypesQuery.ToQueryString();
        //    //Console.WriteLine(sql);

        //    //var gstTypes = await gstTypesQuery.ToListAsync();
        //    //return gstTypes;
        //}
        //public virtual async Task<List<ServiceMasterDTO>> GetExisitingBatchList(int TariffId, int ClassId, string ServiceName)
        //{
        //    var qry = (from s in _context.ServiceMasters
        //               join d in _context.ServiceDetails.Where(x => (x.TariffId == TariffId || TariffId == 0) && (x.ClassId == ClassId || ClassId == 0)) on s.ServiceId equals d.ServiceId
        //               //on s.ServiceId equals d.ServiceId
        //               join x in _context.ServiceWiseCompanyCodes on s.ServiceId equals x.ServiceId
        //               where (s.IsActive == true)
        //                     && (ServiceName == "" || s.ServiceName.Contains(ServiceName))
        //               select new ServiceMasterDTO()
        //               {
        //                   ServiceId = s.ServiceId,
        //                   GroupId = s.GroupId,
        //                   ServiceShortDesc = s.ServiceShortDesc,
        //                   ServiceName = s.ServiceName,
        //                   ClassRate = d.ClassRate ?? 0,
        //                   TariffId = d.TariffId ?? 0,
        //                   ClassId = d.ClassId ?? 0,
        //                   IsEditable = s.IsEditable,
        //                   CreditedtoDoctor = s.CreditedtoDoctor,
        //                   IsPathology = s.IsPathology,
        //                   IsRadiology = s.IsRadiology,
        //                   IsActive = s.IsActive,
        //                   PrintOrder = s.PrintOrder,
        //                   IsPackage = s.IsPackage,
        //                   DoctorId = s.DoctorId,
        //                   IsDocEditable = s.IsDocEditable,
        //                   CompanyCode = x.CompanyCode ?? "",
        //                   CompanyServicePrint = x.CompanyServicePrint ?? "",
        //                   IsInclusionOrExclusion = x.IsInclusionOrExclusion
        //               });
        //    var sql = qry.Take(50).ToQueryString();
        //    Console.WriteLine(sql);
        //    return await qry.Take(50).ToListAsync();

        //}

        //public virtual async Task VerifyAsync(TGrndetail objGRN, int UserId, string Username)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //    {
        //        // Update header table records
        //        TGrnheader objGrnHeader = await _context.TGrnheaders.FindAsync(objGRN.Grnid);
        //        objGrnHeader.IsVerified = objGRN.IsVerified;
        //        foreach (var objDet in objGrnHeader.TGrndetails)
        //        {
        //            objDet.IsVerified = objGRN.IsVerified;
        //            objDet.IsVerifiedUserId = objGRN.IsVerifiedUserId;
        //            objDet.IsVerifiedDatetime = objGRN.IsVerifiedDatetime;
        //        }
        //        _context.TGrnheaders.Update(objGrnHeader);
        //        _context.Entry(objGrnHeader).State = EntityState.Modified;
        //        await _context.SaveChangesAsync();

        //        scope.Complete();
        //    }
        //}



        public virtual async Task AsyncSp(TGrnheader objGRN, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "Grnid","GrnNumber","IsVerified", "Grndate", "Grntime","DeliveryNo","GateEntryNo","CashCreditType",
               "Grntype", "TotalAmount", "TotalDiscAmount", "TotalVatamount",  "NetAmount","InvDate",
                "Remark","ReceivedBy","IsClosed","IsPaymentProcess","PaymentPrcDate","ProcessDes","DebitNote","CreditNote","OtherCharge",
                "RoundingAmt","PaidAmount","BalAmount","TotCgstamt","TotSgstamt","AddedBy","UpdatedBy","IsCancelled","IsClosed","Prefix","TotIgstamt",
                "TranProcessId","TranProcessMode","BillDiscAmt","PaymentDate","EwayBillNo","EwayBillDate","TGrndetails","TSupPayDets"};
            var entity = objGRN.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
              odal.ExecuteNonQuery("ps_m_grnInvoiceno_check", CommandType.StoredProcedure, entity);


        }

        //public virtual async Task InsertSp(TGrnheader objGRN, int UserId, string UserName)
        //{

        //    DatabaseHelper odal = new();

        //    string[] AEntity = { "UpdatedBy", "Prefix", "IsCancelled", "IsPaymentProcess", "PaymentPrcDate", "ProcessDes", "PaymentDate", "PaidAmount", "GrnNumber", "TGrndetails", "BalAmount" };
        //    var yentity = objGRN.ToDictionary();
        //    foreach (var rProperty in AEntity) 

        //    {
        //        yentity.Remove(rProperty);
        //    }
        //var Grnid =  odal.ExecuteNonQuery("m_insert_GRNHeader_PurNo_1_New", CommandType.StoredProcedure, "Grnid", yentity);

        ////    string[] rEntity = { "IsCancelled", "PbillNo", "AdvanceUsedAmount", "IsBillCheck", "IsBillShrHold", "ChTotalAmt", "ChConcessionAmt", "ChNetPayAmt", "CreatedDate", "ModifiedBy", "ModifiedDate", "BillPrefix", "BillMonth", "BillYear", "PrintBillNo", "AddCharges", "BillDetails" };
        ////    var entity = ObjBill.ToDictionary();
        ////    foreach (var rProperty in rEntity)
        ////    {
        ////        entity.Remove(rProperty);
        ////    }
        ////    string vBillNo = odal.ExecuteNonQuery("ps_insert_Bill_CashCounter_1", CommandType.StoredProcedure, "BillNo", entity);
        ////    ObjBill.BillNo = Convert.ToInt32(vBillNo);
        ////    //    ObjBillDetails.BillNo = Convert.ToInt32(vBillNo);
        ////    Objpayment.BillNo = Convert.ToInt32(vBillNo);

        ////    foreach (var item in ObjBillDetails)
        ////    {
        ////        item.BillNo = Convert.ToInt32(vBillNo);
        ////        string[] BillEntity = { "BillDetailId", "BillNoNavigation" };
        ////        var Bentity = item.ToDictionary();
        ////        foreach (var rProperty in BillEntity)
        ////        {
        ////            Bentity.Remove(rProperty);
        ////        }
        ////        odal.ExecuteNonQuery("ps_insert_BillDetails_1", CommandType.StoredProcedure, Bentity);
        ////    }
        ////    string[] pEntity = { "PaymentId", "IsSelfOrcompany", "CashCounterId", "CompanyId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount", "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode" };
        ////    var entity1 = Objpayment.ToDictionary();
        ////    foreach (var rProperty in pEntity)
        ////    {
        ////        entity1.Remove(rProperty);
        ////    }
        ////    odal.ExecuteNonQuery("ps_insert_Payment_1", CommandType.StoredProcedure, entity1);
        ////}
        ////public virtual async Task IPDraftBillAsync(TDrbill ObjTDrbill, List<TDrbillDet> ObjTDrbillDetList, int UserId, string UserName)
        ////{

        ////    DatabaseHelper odal = new();
        ////    string[] rEntity = { "IsCancelled", "PbillNo", "CashCounterId", "AdvanceUsedAmount" };
        ////    var entity = ObjTDrbill.ToDictionary();
        ////    foreach (var rProperty in rEntity)
        ////    {
        ////        entity.Remove(rProperty);
        ////    }
        ////    string vDRBNo = odal.ExecuteNonQuery("ps_insert_DRBill_1", CommandType.StoredProcedure, "Drbno", entity);
        ////    ObjTDrbill.Drbno = Convert.ToInt32(vDRBNo);


        ////    foreach (var item in ObjTDrbillDetList)
        ////    {
        ////        item.Drno = Convert.ToInt32(vDRBNo);
        ////        string[] TEntity = { "DrbillDetId", };
        ////        var Dentity = item.ToDictionary();
        ////        foreach (var rProperty in TEntity)
        ////        {
        ////            Dentity.Remove(rProperty);
        ////        }
        ////        odal.ExecuteNonQuery("ps_insert_T_DRBillDet_1", CommandType.StoredProcedure, Dentity);
        ////    }

        //}


    }
}
