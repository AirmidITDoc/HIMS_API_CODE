using HIMS.Core.Infrastructure;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Pathology;
using System.Data;
using HIMS.Services.Utilities;


namespace HIMS.Services.Pathlogy
{
    public  class HomeCollectionService:IHomeCollectionService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public HomeCollectionService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<homeCollectionDetListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<homeCollectionDetListDto>(model, "ps_Rtrv_homeCollectionDetList");

        }
        public virtual async Task<IPagedList<HomeCollectionRegistrationInfoListDto>> HomeCollectionListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<HomeCollectionRegistrationInfoListDto>(model, "ps_HomeCollectionRegistrationInfoList");

        }
        public virtual async Task InsertAsync(THomeCollectionRegistrationInfo ObjTHomeCollectionRegistrationInfo, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            {
                var presNoList = await _context.THomeCollectionRegistrationInfos
                .Where(x => x.HomeSeqNo != null && x.HomeSeqNo != "")
                .Select(x => x.HomeSeqNo)
                .ToListAsync();

                int lastPresNo = presNoList
                    .Select(p => int.TryParse(p, out var n) ? n : 0)
                    .DefaultIfEmpty(0)
                    .Max();

                //  Increment & assign
                ObjTHomeCollectionRegistrationInfo.HomeSeqNo = (lastPresNo + 1).ToString();

                ObjTHomeCollectionRegistrationInfo.CreatedBy = UserId;
                ObjTHomeCollectionRegistrationInfo.CreatedDate = AppTime.Now;

                _context.THomeCollectionRegistrationInfos.Add(ObjTHomeCollectionRegistrationInfo);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync(THomeCollectionRegistrationInfo ObjTHomeCollectionRegistrationInfo, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);


            {
                long reservationId = ObjTHomeCollectionRegistrationInfo.HomeCollectionId;

                //Delete related details first
                var lstAttend = await _context.THomeCollectionServiceDetails
                    .Where(x => x.HomeCollectionId == reservationId)
                    .ToListAsync();
                if (lstAttend.Any())
                    _context.THomeCollectionServiceDetails.RemoveRange(lstAttend);

                await _context.SaveChangesAsync();

                // Then attach and update header
                _context.Attach(ObjTHomeCollectionRegistrationInfo);
                _context.Entry(ObjTHomeCollectionRegistrationInfo).State = EntityState.Modified;

                _context.Entry(ObjTHomeCollectionRegistrationInfo).Property(x => x.CreatedBy).IsModified = false;
                _context.Entry(ObjTHomeCollectionRegistrationInfo).Property(x => x.CreatedDate).IsModified = false;
                _context.Entry(ObjTHomeCollectionRegistrationInfo).Property(x => x.HomeSeqNo).IsModified = false;

                ObjTHomeCollectionRegistrationInfo.ModifiedBy = UserId;
                ObjTHomeCollectionRegistrationInfo.ModifiedDate = AppTime.Now;

                if (ignoreColumns?.Length > 0)
                {
                    foreach (var column in ignoreColumns)
                        _context.Entry(ObjTHomeCollectionRegistrationInfo).Property(column).IsModified = false;
                }

                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }
        public virtual async Task Cancel(THomeCollectionRegistrationInfo objTHomeCollectionRegistrationInfo, int UserId, string Username)
        {
            //throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] Entity = { "HomeCollectionId", "IsCancelledBy" };
            var HEntity = objTHomeCollectionRegistrationInfo.ToDictionary();
            foreach (var rProperty in HEntity.Keys.ToList())
            {
                if (!Entity.Contains(rProperty))
                    HEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("PS_Cancel_HomeCollection", CommandType.StoredProcedure, HEntity);
        }

        
    }
}
