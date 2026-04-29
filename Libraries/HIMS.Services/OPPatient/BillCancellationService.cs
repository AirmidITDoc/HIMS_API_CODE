using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System.Data;

namespace HIMS.Services.OPPatient
{
    public class BillCancellationService : IBillCancellationService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public BillCancellationService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task UpdateAsyncOp(Bill objOpBillCancellation, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            string[] AEntity = { "BillNo", "UserId", "DiscComments" };
            var entity = objOpBillCancellation.ToDictionary();

            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("ps_OP_BILL_CANCELLATION", CommandType.StoredProcedure, entity);
            await _context.LogProcedureExecution(entity, nameof(Bill), objOpBillCancellation.BillNo.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);
        }

        public virtual async Task UpdateAsyncLabBill(Bill objOpBillCancellation, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            string[] AEntity = { "BillNo", "UserId", "DiscComments" };
            var entity = objOpBillCancellation.ToDictionary();

            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("ps_LabBILL_CANCELLATION", CommandType.StoredProcedure, entity);
            await _context.LogProcedureExecution(entity, nameof(Bill), objOpBillCancellation.BillNo.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);
        }
        public virtual async Task UpdateAsyncIp(Bill objIPBillCancellation, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            string[] AEntity = { "BillNo", "UserId", "DiscComments" };
            var bentity = objIPBillCancellation.ToDictionary();

            foreach (var rProperty in bentity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    bentity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("ps_IP_BILL_CANCELLATION", CommandType.StoredProcedure, bentity);
            await _context.LogProcedureExecution(bentity, nameof(Bill), objIPBillCancellation.BillNo.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);

        }


    }
}
