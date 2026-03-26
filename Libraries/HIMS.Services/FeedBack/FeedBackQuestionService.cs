using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.FeedBack;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.FeedBack
{
    public class FeedBackQuestionService : IFeedBackQuestionService
    {
        private readonly HIMSDbContext _context;
        public FeedBackQuestionService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<FeedbackQuestionListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<FeedbackQuestionListDto>(model, "ps_Rtrv_FeedbackQuestionList");
        }
    }
}
