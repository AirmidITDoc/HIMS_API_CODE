using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Transactions;

namespace HIMS.Services.Administration
{
    public class Texpenseservice : ITexpenseservice
    {
        private readonly Data.Models.HIMSDbContext _context;
        public Texpenseservice(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<DailyExpenceListtDto>> DailyExpencesList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DailyExpenceListtDto>(model, "ps_Rtrv_T_Expenses");
        }


        //public virtual async Task InsertAsync(TExpense ObjTExpense, int UserId, string Username)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //    {
        //        _context.TExpenses.Add(ObjTExpense);
        //        await _context.SaveChangesAsync();

        //        scope.Complete();
        //    }
        //}


        public virtual async Task InsertAsync(TExpense ObjTExpense, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            {
                // Get last SequenceNo (string type)
                var lastSeqNoStr = await _context.TExpenses
                    .OrderByDescending(x => x.SequenceNo)
                    .Select(x => x.SequenceNo)
                    .FirstOrDefaultAsync();

                int lastSeqNo = 0;
                if (!string.IsNullOrEmpty(lastSeqNoStr) && int.TryParse(lastSeqNoStr, out var parsed))
                    lastSeqNo = parsed;

                ObjTExpense.SequenceNo = (lastSeqNo + 1).ToString();

                _context.TExpenses.Add(ObjTExpense);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }



        public virtual async Task UpdateExpensesAsync(TExpense ObjTExpense, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.TExpenses.Update(ObjTExpense);
                _context.Entry(ObjTExpense).State = EntityState.Modified;

                // 2. Ignore specific columns
                if (ignoreColumns?.Length > 0)
                {
                    foreach (var column in ignoreColumns)
                    {
                        _context.Entry(ObjTExpense).Property(column).IsModified = false;
                    }
                }
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        public virtual void TExpenseCancel(TExpense ObjTExpense, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] AEntity = { "ExpId", "IsCancelledBy" };
            var entity = ObjTExpense.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("m_Update_T_Expenses_IsCancel", CommandType.StoredProcedure, entity);


        }
    }
}
