using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.Administration
{
    public  class Texpenseservice: ITexpenseservice
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


        public virtual async Task InsertAsync(TExpense ObjTExpense, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TExpenses.Add(ObjTExpense);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        public virtual async Task UpdateExpensesAsync(TExpense ObjTExpense, int UserId, string Username, string[] strings)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.TExpenses.Update(ObjTExpense);
                _context.Entry(ObjTExpense).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        public virtual async Task TExpenseCancel(TExpense ObjTExpense, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] AEntity = { "ExpDate", "ExpTime", "ExpType", "ExpAmount", "PersonName", "Narration", "IsAddedby", "IsCancelled", "VoucharNo", "ExpHeadId" };
            var entity = ObjTExpense.ToDictionary();
            foreach (var rProperty in AEntity)
            {
                entity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("m_Update_T_Expenses_IsCancel", CommandType.StoredProcedure, entity);

        }
    }
}
