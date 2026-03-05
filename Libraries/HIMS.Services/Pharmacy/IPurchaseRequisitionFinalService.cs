using HIMS.Data.DTO.Purchase;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pharmacy
{
    public interface IPurchaseRequisitionFinalService
    {
        Task InsertAsync(TPrheader ObjTPrheader, List<TPurchaseRequisitionHeader> objPurchaseRequisitionHeader, int UserId, string Username);
    }
}
