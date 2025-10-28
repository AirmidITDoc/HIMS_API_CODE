using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Nursing;
using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Transactions;


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
            return await DatabaseHelper.GetGridDataBySp<DoctorNoteListDto>(model, "ps_Rtrv_DoctorsNotesList");
        }
        public virtual async Task<IPagedList<TDoctorPatientHandoverListDto>> TDoctorPatientHandoverList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<TDoctorPatientHandoverListDto>(model, "ps_Rtrv_T_Doctor_PatientHandoverList");
        }
        public virtual async Task<IPagedList<CanteenRequestListDto>> CanteenRequestsList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<CanteenRequestListDto>(model, "ps_Rtrv_CanteenRequestDet");
        }
        public virtual async Task<IPagedList<CanteenRequestHeaderListDto>> CanteenRequestHeaderList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<CanteenRequestHeaderListDto>(model, "ps_Rtrv_CanteenRequestListFromWard");
        }

        public virtual async Task
            InsertAsync(TCanteenRequestHeader objCanteen, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled);

            // Generate ReqNo before adding to DB
            var last = await _context.TCanteenRequestHeaders
                .OrderByDescending(x => x.ReqId)
                .FirstOrDefaultAsync();

            int lastNo = 0;
            if (last?.ReqNo != null)
            {
                var match = Regex.Match(last.ReqNo, @"\d+");
                if (match.Success) lastNo = int.Parse(match.Value);
            }
            objCanteen.ReqNo = (lastNo + 1).ToString();

            _context.TCanteenRequestHeaders.Add(objCanteen);
            await _context.SaveChangesAsync();

            scope.Complete();
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

        public virtual async Task<List<CanteenListDto>> GetItemListForCanteen(string ItemName)
        {
            var qry = (from MCanItemMaster in _context.MCanItemMasters
                       where MCanItemMaster.ItemName == "" || MCanItemMaster.ItemName.Contains(ItemName)

                       select new CanteenListDto
                       {
                           ItemID = MCanItemMaster.ItemId,
                           ItemName = MCanItemMaster.ItemName,
                           Price = MCanItemMaster.Price,
                           IsBatchRequired = MCanItemMaster.IsBatchRequired

                       });
            return await qry.Take(50).ToListAsync();
        }
        public virtual async Task CancelAsync(TCanteenRequestDetail ObjTCanteenRequestDetail, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header table records
                TCanteenRequestDetail ObPres = await _context.TCanteenRequestDetails.FindAsync(ObjTCanteenRequestDetail.ReqDetId);
                if (ObPres == null)
                    throw new Exception("Prescription not found.");
                ObPres.IsCancelled = true;
                _context.TCanteenRequestDetails.Update(ObPres);
                _context.Entry(ObPres).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

    }
}

