using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pathlogy
{
    public class LabApprovalService : ILabApprovalService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public LabApprovalService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<LabResultCompletedListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LabResultCompletedListDto>(model, "ps_Rtrv_LabResult_Completed_List");
        }
    }
}
