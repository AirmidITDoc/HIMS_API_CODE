using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.GRN;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Purchase;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System.Data;

namespace HIMS.Services.Inventory
{
    public class SupplierPaymentStatusService : ISupplierPaymentStatusService
    {
        private readonly Data.Models.HIMSDbContext _context;

        public SupplierPaymentStatusService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<SupplierNamelistDto>> SupplierNamelist(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SupplierNamelistDto>(model, "m_Rtrv_SupplierName_list");

        }
        public virtual async Task<IPagedList<ItemListBysupplierNameDto>> GetItemListbysuppliernameAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<ItemListBysupplierNameDto>(model, "Rtrv_ItemList_by_Supplier_Name_For_GRNReturn");
        }
        public virtual async Task<IPagedList<SupplierPaymentStatusListDto>> GetSupplierPaymentStatusList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SupplierPaymentStatusListDto>(model, "Rtrv_GRNList_ForAccount_payment");
        }

        public virtual async Task<IPagedList<GetSupplierPaymentListDto>> GetSupplierPaymentList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<GetSupplierPaymentListDto>(model, "ps_Rtrv_GrnSupPayList");
        }


        public virtual void InsertSP(TGrnsupPayment ObjTGrnsupPayment, List<TGrnheader> ObjTGrnheader, List<TSupPayDet> ObjTSupPayDet, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "SupPayNo", "TSupPayDets" };
            var entity = ObjTGrnsupPayment.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string PSupPayId = odal.ExecuteNonQuery("insert_T_GRNSupPayment_1", CommandType.StoredProcedure, "SupPayId", entity);
            ObjTGrnsupPayment.SupPayId = Convert.ToInt32(PSupPayId);



            foreach (var item in ObjTGrnheader)
            {

                string[] pEntity = { "GrnNumber","Grndate","Grntime","StoreId","SupplierId","InvoiceNo","DeliveryNo","GateEntryNo","CashCreditType","Grntype","TotalAmount","TotalDiscAmount","TotalVatamount","NetAmount","Remark","ReceivedBy",
                "IsVerified","IsClosed","AddedBy","UpdatedBy","Prefix","IsCancelled","IsPaymentProcess","PaymentPrcDate","ProcessDes","InvDate","DebitNote","CreditNote","OtherCharge","RoundingAmt","TotCgstamt",
               "TotSgstamt","TotIgstamt","TranProcessId","TranProcessMode","BillDiscAmt","EwayBillNo","PaymentDate","EwayBillDate","TGrndetails","TSupPayDets"};
                var qentity = item.ToDictionary();
                foreach (var rProperty in pEntity)
                {
                    qentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("Update_TGRNHeader_PayStatus", CommandType.StoredProcedure, qentity);
            }

            foreach (var item in ObjTSupPayDet)
            {
                item.SupPayId = Convert.ToInt32(PSupPayId);

                string[] EEntity = { "SupTranId", "SupGrn", "SupPay" };
                var Rentity = item.ToDictionary();
                foreach (var rProperty in EEntity)
                {
                    Rentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("insert_T_SupPayDet_PayStatus", CommandType.StoredProcedure, Rentity);
            }

        }

    }

}
