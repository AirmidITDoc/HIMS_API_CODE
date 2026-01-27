using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pathlogy
{
    public class BranchService: IBranchService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public BranchService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
    }
}
