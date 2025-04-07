using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Nursing;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static LinqToDB.Sql;
using static LinqToDB.SqlQuery.SqlPredicate;

namespace HIMS.Services.Nursing
{
    public class CanteenRequestService : ICanteenRequestService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public CanteenRequestService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<DoctorNoteListDto>> DoctorNoteList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DoctorNoteListDto>(model, "Rtrv_DoctorsNotesList");
        }
        public virtual async Task<IPagedList<TDoctorPatientHandoverListDto>> TDoctorPatientHandoverList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<TDoctorPatientHandoverListDto>(model, "m_Rtrv_T_Doctor_PatientHandoverList");
        }
        public virtual async Task<IPagedList<CanteenRequestListDto>> CanteenRequestsList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<CanteenRequestListDto>(model, "m_Rtrv_CanteenRequestDet");
        }
        public virtual async Task<IPagedList<CanteenRequestHeaderListDto>> CanteenRequestHeaderList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<CanteenRequestHeaderListDto>(model, "m_Rtrv_CanteenRequestListFromWard");
        }
        public virtual async Task InsertAsync(TCanteenRequestHeader objCanteen, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TCanteenRequestHeaders.Add(objCanteen);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        public virtual async Task<List<CanteenListDto>> GetItemList(string ItemName)
        {
            var qry = from s in _context.MCanItemMasters
                      where (ItemName == "" || s.ItemName.Contains(ItemName))
                                    
                      select new CanteenListDto()
                      {
                          ItemID = s.ItemId,
                          ItemName = s.ItemName,
                          //Price = s.Price,
                          //IsBatchRequired = s.IsBatchRequired,
                         
                      };
            return await qry.Take(50).ToListAsync();
        }
    }
}

