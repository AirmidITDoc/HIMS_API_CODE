using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Administration
{
    public partial  interface ITexpenseservice
    {
        Task InsertAsync(TExpense ObjTExpense, int UserId, string Username);
        Task UpdateExpensesAsync(TExpense ObjTExpense, int UserId, string Username, string[]? references);
        Task TExpenseCancel(TExpense ObjTExpense, int UserId, string Username);
        Task<IPagedList<DailyExpenceListtDto>> DailyExpencesList(GridRequestModel objGrid);


    }
}
