using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pharmacy
{
    public partial interface IPharmacyAdvanceService
    {
        Task Insert(TPhadvanceHeader ObjTPhadvanceHeader, TPhadvanceDetail ObjTPhadvanceDetail, PaymentPharmacy ObjPaymentPharmacy, List<TPaymentPharmacy> ObjTPaymentPharmacy, int UserId, string Username);
        Task Update(TPhadvanceHeader ObjTPhadvanceHeader, TPhadvanceDetail ObjTPhadvanceDetail, PaymentPharmacy ObjPaymentPharmacy, List<TPaymentPharmacy> ObjTPaymentPharmacy, int UserId, string Username);


    }
}
