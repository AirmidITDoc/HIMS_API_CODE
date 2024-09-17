using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Models;

namespace HIMS.Services.Inventory
{
    public partial interface ISupplierPaymentService
    {
        Task InsertAsyncLoop(TGrnheader objGRN, int UserId, string Username);
        Task InsertAsync(TGrnheader objGRN, int UserId, string Username);
        Task UpdateAsync(TGrnheader objGRN, int UserId, string Username);
    }
}
