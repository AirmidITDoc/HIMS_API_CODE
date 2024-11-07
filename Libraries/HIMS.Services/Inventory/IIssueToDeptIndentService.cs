using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Models;

namespace HIMS.Services.Inventory
{
    public partial interface IIssueToDeptIndentService
    {
        Task InsertAsync(TIssueToDepartmentHeader objIssueToDeptIndent, int UserId, string Username);
        //Task UpdateAsync(TIssueToDepartmentHeader objIssueToDeptIndent, int UserId, string Username);
    }

}
