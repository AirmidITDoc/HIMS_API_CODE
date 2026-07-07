using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.TrustMembershipRegistration
{
    public partial interface ITrustMembershipRegService
    {
        Task InsertAsync(TMembershipRegistration ObjTMembershipRegistration, int UserId, string Username);
        Task UpdateAsync(TMembershipRegistration ObjTMembershipRegistration, int UserId, string Username, string[]? references);
        Task<TMembershipRegistration> GetById(int Id);

    }
}
