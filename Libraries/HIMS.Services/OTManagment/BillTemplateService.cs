using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.OTManagement;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OTManagment
{
    public class BillTemplateService : IBillTemplateService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public BillTemplateService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task<IPagedList<OtBillTemplateListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<OtBillTemplateListDto>(model, "ps_ot_billTemplelist");
        }
    }
}
