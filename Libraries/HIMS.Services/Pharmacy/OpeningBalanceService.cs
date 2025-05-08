using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Purchase;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
//using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pharmacy
{
    public  class OpeningBalanceService :IOpeningBalanceService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public OpeningBalanceService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<OpeningBalListDto>> GetOpeningBalanceList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<OpeningBalListDto>(model, "m_Rtrv_OpeningItemList");
        }
        public virtual async Task<IPagedList<OpeningBalanaceItemDetailListDto>> GetOPningBalItemDetailList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<OpeningBalanaceItemDetailListDto>(model, "m_Rtrv_OpeningItemDet");
        }



        public virtual async Task OpeningBalAsyncSp(TOpeningTransactionHeader ObjTOpeningTransactionHeader,List<TOpeningTransaction> ObjTOpeningTransaction, TOpeningTransactionHeader ObjTOpeningTransactionHeaders, int UserId, string UserName)
        {

            DatabaseHelper odal = new();

            string[] AEntity = { "OpeningDocNo", "UpdatedBy",  "OpeningHid", "OPeningHId" };

            var yentity = ObjTOpeningTransactionHeader.ToDictionary();
            foreach (var rProperty in AEntity)
            {
                yentity.Remove(rProperty);
            }
            string BOpeningHId = odal.ExecuteNonQuery("Insert_OpeningTransaction_header_1", CommandType.StoredProcedure, "OpeningHId",yentity);
            ObjTOpeningTransactionHeader.OpeningHid = Convert.ToInt32(BOpeningHId);
            ObjTOpeningTransactionHeaders.OpeningHid = Convert.ToInt32(BOpeningHId);


            foreach (var item in ObjTOpeningTransaction)
            {
                string[] rEntity = { " " };
                var entity = item.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                string TOpeningId = odal.ExecuteNonQuery("Insert_OpeningTransaction_1", CommandType.StoredProcedure, "OpeningId", entity);
             //   ObjTOpeningTransaction.OpeningId = Convert.ToInt32(TOpeningId);
            }
             
                string[] BillEntity = { "OpeningDocNo", "StoreId", "OpeningDate", "OpeningTime", "AddedBy" , "UpdatedBy",  "OPeningHId" };
                var Bentity = ObjTOpeningTransactionHeaders.ToDictionary();
                foreach (var rProperty in BillEntity)
                {
                    Bentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("Insert_Update_OpeningTran_ItemStock_1", CommandType.StoredProcedure, Bentity);
         
        }



    }
}
