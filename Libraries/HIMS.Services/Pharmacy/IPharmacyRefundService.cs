using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pharmacy
{
    public partial interface IPharmacyRefundService
    {
        Task Insert(TPhRefund ObjTPhRefund, TPhadvanceHeader ObjTPhadvanceHeader, List<TPhadvRefundDetail> ObjTPhadvRefundDetail, List<TPhadvanceDetail> ObjTPhadvanceDetail, PaymentPharmacy ObjPaymentPharmacy, List<TPaymentPharmacy> ObjTPaymentPharmacy, int UserId, string Username);


    }
}
