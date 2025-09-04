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
            return await DatabaseHelper.GetGridDataBySp<OpeningBalListDto>(model, "ps_Rtrv_OpeningItemList");
        }
        public virtual async Task<IPagedList<OpeningBalanaceItemDetailListDto>> GetOPningBalItemDetailList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<OpeningBalanaceItemDetailListDto>(model, "ps_Rtrv_OpeningItemDet");
        }


        public virtual async Task OpeningBalAsyncSp(TOpeningTransactionHeader ObjTOpeningTransactionHeader,List<TOpeningTransactionDetail> ObjTOpeningTransaction, int UserId, string UserName)
        {

            DatabaseHelper odal = new();

            string[] AEntity = { "OpeningDocNo", "CreatedDate", "ModifiedDate", "ModifiedBy" };

            var yentity = ObjTOpeningTransactionHeader.ToDictionary();
            foreach (var rProperty in AEntity)
            {
                yentity.Remove(rProperty);
            }
            string BOpeningHId = odal.ExecuteNonQuery("ps_Insert_OpeningTransaction_header_1", CommandType.StoredProcedure, "OpeningHid", yentity);
            ObjTOpeningTransactionHeader.OpeningHid = Convert.ToInt32(BOpeningHId);

            foreach (var item in ObjTOpeningTransaction)
            {
                item.OpeningId = ObjTOpeningTransactionHeader.OpeningHid;
                string[] rEntity = { "OpeningId" };
                var entity = item.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_Insert_OpeningTransaction_1", CommandType.StoredProcedure, entity);
            }
            var OpeningIdObj = new
            {
                OPeningHId = Convert.ToInt32(BOpeningHId)
            };
            odal.ExecuteNonQuery("Insert_Update_OpeningTran_ItemStock_1", CommandType.StoredProcedure, OpeningIdObj.ToDictionary());
        }

    }
}
