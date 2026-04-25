using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HIMS.Services.Inventory
{
    public  class DoseMasterService : IDoseMasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public DoseMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<List<DoseMasterListDto>> GetDoseMasterList(string DoseName)
        {
            var qry = _context.MDoseMasters
                .Where(dose =>
                    (string.IsNullOrEmpty(DoseName) || dose.DoseName.Contains(DoseName))
                    && (dose.IsActive == true)
                )
                .OrderBy(dose => dose.DoseId)
                .Select(dose => new DoseMasterListDto
                {
                    DoseId = dose.DoseId, 
                    DoseName = dose.DoseName ?? string.Empty,

                });

            return await qry.Take(50).ToListAsync();
        }
    }
}
