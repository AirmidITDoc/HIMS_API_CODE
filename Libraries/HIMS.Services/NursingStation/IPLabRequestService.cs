using HIMS.Data.Models;

namespace HIMS.Services.NursingStation
{
    public class IPLabRequestService : IIPLabRequestService
    {
        private readonly HIMSDbContext _context;
        public IPLabRequestService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsyncSP(THlabRequest objTHlabRequest,int currentUserId, string currentUserName)
        {

            // OLD CODE With SP
            //DatabaseHelper odal = new();
            //string[] rEntity = { "RegNo", "UpdatedBy", "RegPrefix", "AnnualIncome", "IsIndientOrWeaker", "RationCardNo", "IsMember" };
            //var entity = objTHlabRequest.ToDictionary();
            //foreach (var rProperty in rEntity)
            //{
            //    entity.Remove(rProperty);
            //}
            //odal.ExecuteNonQuery("insert_T_HLabRequest_1", CommandType.StoredProcedure, entity);

            //_context.THlabRequest.Add(objTHlabRequest);
            await _context.SaveChangesAsync();

            //_context.TDlabRequest.Add(objTDlabRequest);
            //objTDlabRequest.RequestId = objTHlabRequest.RequestId;
            await _context.SaveChangesAsync();
        }
    }
}