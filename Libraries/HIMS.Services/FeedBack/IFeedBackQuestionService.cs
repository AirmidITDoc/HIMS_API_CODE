using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.FeedBack;
using HIMS.Data.DTO.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.FeedBack
{
    public partial  interface IFeedBackQuestionService
    {
        Task<IPagedList<FeedbackQuestionListDto>> GetListAsync(GridRequestModel objGrid);

    }
}
