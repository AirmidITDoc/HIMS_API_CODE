using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Administration
{
    public partial interface IConfigService
    {
        Task<IPagedList<Smsconfigdetail>> GetSMSconfig(GridRequestModel objGrid);

        Task<IPagedList<EmailConfigurationdetailListDto>> GetEmailconfigdetail(GridRequestModel objGrid);


        //Task<IPagedList<EmailConfigurationdetailListDto>> GetWhatsAppconfig(GridRequestModel objGrid);
    }
}
