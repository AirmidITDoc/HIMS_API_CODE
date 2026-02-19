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


        public virtual async Task  InsertSP(TGrnsupPayment ObjTGrnsupPayment, List<TGrnheader> ObjTGrnheader, List<TSupPayDet> ObjTSupPayDet, int CurrentUserId, string CurrentUserName)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "SupPayId", "SupPayDate", "SupPayTime", "GrnId", "CashPayAmt", "ChequePayAmt", "CardPayDate", "ChequePayDate", "ChequeBankName", "CardBankName", "CardNo", "ChequeNo", "Remarks", "IsAddedBy", "IsUpdatedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDatetime", "PartyReceiptNo", "NeftpayAmount", "Neftno", "NeftbankMaster", "Neftdate", "PayTmamount", "PayTmtranNo", "PayTmdate", "CardPayAmt"};
            var entity = ObjTGrnsupPayment.ToDictionary();

            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            string PSupPayId = odal.ExecuteNonQuery("insert_T_GRNSupPayment_1", CommandType.StoredProcedure, "SupPayId", entity);
            ObjTGrnsupPayment.SupPayId = Convert.ToInt32(PSupPayId);
            _ = Task.Run(() => _context.LogProcedureExecution(entity, nameof(TGrnsupPayment), ObjTGrnsupPayment.SupPayId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName));

            foreach (var item in ObjTGrnheader)
            {

                string[] pEntity = { "Grnid","PaidAmount","BalAmount"};
                var qentity = item.ToDictionary();

                foreach (var rProperty in qentity.Keys.ToList())
                {
                    if (!pEntity.Contains(rProperty))
                        qentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("Update_TGRNHeader_PayStatus", CommandType.StoredProcedure, qentity);
                _ = Task.Run(() => _context.LogProcedureExecution(qentity, nameof(TGrnheader), item.Grnid.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName));

            }

            foreach (var item in ObjTSupPayDet)
            {
                item.SupPayId = Convert.ToInt32(PSupPayId);

                string[] EEntity = { "SupPayId", "SupGrnId"};
                var Rentity = item.ToDictionary();

                foreach (var rProperty in Rentity.Keys.ToList())
                {
                    if (!EEntity.Contains(rProperty))
                        Rentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("insert_T_SupPayDet_PayStatus", CommandType.StoredProcedure, Rentity);
                _ = Task.Run(() => _context.LogProcedureExecution(Rentity, nameof(TSupPayDet), item.SupTranId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName));

            }

        }

    }

}
