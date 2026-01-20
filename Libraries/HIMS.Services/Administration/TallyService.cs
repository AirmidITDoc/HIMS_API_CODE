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
    public class TallyService : ITallyService
    {
        private readonly HIMSDbContext _context;
        public TallyService(HIMSDbContext context)
        {
            _context = context;
        }

        public virtual async Task<IPagedList<TallyListDto>> OPBillCashCounterListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<TallyListDto>(model, "PS_Tally_OPBillList_CashCounter");
        }
    }
}
