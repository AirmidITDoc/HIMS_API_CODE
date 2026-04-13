using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Administration
{
    public class HelpdeskPatientComplaintService : IHelpdeskPatientComplaintService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public HelpdeskPatientComplaintService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<ComplaintListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<ComplaintListDto>(model, "ps_Rtrv_ComplaintList");
        }
    }
}
