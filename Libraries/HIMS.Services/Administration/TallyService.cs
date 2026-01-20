using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Administration
{
    public class TallyService : ITallyService
    {
        private readonly HIMSDbContext _context;
        public TallyService(HIMSDbContext context)
        {
            _context = context;
        }
    }
}
