using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.NursingStation
{
    public class CanteenRequestService: ICanteenRequestService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public CanteenRequestService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
            


        }
        public virtual async Task InsertAsyncSP(TCanteenRequestHeader objCanteenRequestHeader, TCanteenRequestDetail objTCanteenRequestDetail, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "ReqNo", "UpdatedBy"};
            var entity = objCanteenRequestHeader.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string ReqId = odal.ExecuteNonQuery("v_insert_T_CanteenRequestHeader_1", CommandType.StoredProcedure, "ReqId", entity);
            objCanteenRequestHeader.ReqId = Convert.ToInt32(ReqId);
            


            string[] tEntity = { "ReqDetId", "IsBillGenerated", "IsCancelled" };
            var tntity = objTCanteenRequestDetail.ToDictionary();
            foreach (var rProperty in tEntity)
            {
                entity.Remove(rProperty);
            }
             odal.ExecuteNonQuery("v_insert_T_CanteenRequestDetails_1", CommandType.StoredProcedure,  entity);





            await _context.SaveChangesAsync(UserId, Username);

        }
        public virtual async Task InsertAsync(TCanteenRequestHeader objCanteenRequestHeader, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TCanteenRequestHeaders.Add(objCanteenRequestHeader);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
    }
}
