using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
            return await DatabaseHelper.GetGridDataBySp<GRNDetailsListDto>(model, "Retrieve_GrnItemList");
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

        public virtual async Task InsertWithPOAsync(TGrnheader objGRN, List<MItemMaster> objItems, List<TPurchaseDetail> objPurDetails, List<TPurchaseHeader> objPurHeaders, int UserId, string Username)
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

                // Update item master table records
                _context.MItemMasters.UpdateRange(objItems);
                await _context.SaveChangesAsync();

                // Update purchase details table records
                List<TPurchaseDetail> objPurDetailsList =  new();
                foreach (var objDet in objPurDetails)
                {
                    TPurchaseDetail DetailsInfo = await _context.TPurchaseDetails.FirstOrDefaultAsync(x => x.PurchaseId == objDet.PurchaseId && x.PurDetId == objDet.PurDetId);
                  //  if (DetailsInfo != null)
                 //   {
                        DetailsInfo.PobalQty = objDet.PobalQty;
                        DetailsInfo.IsClosed = objDet.IsClosed;
                        DetailsInfo.IsGrnQty = DetailsInfo.Qty - objDet.PobalQty;
                        objPurDetailsList.Add(DetailsInfo);
                
                }
                _context.TPurchaseDetails.UpdateRange(objPurDetailsList);
                _context.Entry(objPurDetailsList).State = EntityState.Modified;
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
                _context.Entry(objPurHeadersList).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
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



    


        public virtual async Task VerifyAsyncSp(TGrnheader objGRN, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "GrnNumber", "Grndate", "Grntime", "StoreId", "SupplierId","InvoiceNo","DeliveryNo","GateEntryNo","CashCreditType",
               "Grntype", "TotalAmount", "TotalDiscAmount", "TotalVatamount",  "NetAmount","InvDate",
                "Remark","ReceivedBy","IsClosed","IsPaymentProcess","PaymentPrcDate","ProcessDes","DebitNote","CreditNote","OtherCharge",
                "RoundingAmt","PaidAmount","BalAmount","TotCgstamt","TotSgstamt","AddedBy","UpdatedBy","IsCancelled","IsClosed","Prefix","TotIgstamt",
                "TranProcessId","TranProcessMode","BillDiscAmt","PaymentDate","EwayBillNo","EwayBillDate","TGrndetails","TSupPayDets"};
            var entity = objGRN.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("[m_Update_GRN_Verify_Status_1]", CommandType.StoredProcedure, entity);


        }

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
    }
}
