using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Inventory
{
    public partial  interface IIssueToDepService
    {
        Task InsertAsyncSP(TIssueToDepartmentHeader objIssueToDepartment, int UserId, string Username);
        Task InsertAsync(TIssueToDepartmentHeader objIssueToDepartment, int UserId, string Username);
        Task UpdateAsync(TIssueToDepartmentHeader objIssueToDepartment, int UserId, string Username);
        Task UpdateAsync(TIssueToDepartmentHeader objIssueToDepartment, TCurrentStock objCurrentStock, int UserId, string Username);

        Task updateStock(TCurrentStock objCurrentStock, int UserId, string Username);




    }
}
