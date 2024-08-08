using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Inventory
{
    public partial interface ISupplierService
    {
        Task InsertAsyncSP(MSupplierMaster objSupplier, MAssignSupplierToStore objAssignSupplier, int currentUserId, string currentUserName);

    }
}
