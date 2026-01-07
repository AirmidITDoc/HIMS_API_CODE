using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OutPatient
{
    public  class GastrologyEMRService: IGastrologyEMRService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public GastrologyEMRService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

    }
}
