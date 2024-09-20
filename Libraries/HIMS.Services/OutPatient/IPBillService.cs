using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OutPatient
{
    public class IPBillService:IIPBillService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public IPBillService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public Task InsertAsyncSP(Registration objRegistration, VisitDetail objVisitDetail, int currentUserId, string currentUserName)
        {
            throw new NotImplementedException();
        }
    }
}
