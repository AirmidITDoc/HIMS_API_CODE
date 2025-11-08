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
    public class ConfigService : IConfigService
    {
        private readonly HIMSDbContext _context;
        public ConfigService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

      
        public virtual async Task<IPagedList<Smsconfigdetail>> GetSMSconfig(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<Smsconfigdetail>(model, "ps_Rtrv_T_SMSConfigurationlist");
        }

        public virtual async Task<IPagedList<EmailConfigurationdetailListDto>> GetEmailconfigdetail(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<EmailConfigurationdetailListDto>(model, "ps_Rtrv_T_EmailConfigurationlist");
        }


        public virtual async Task<IPagedList<AuditlogDtoList>> GetAuditlog(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<AuditlogDtoList>(model, "ps_Rtrv_T_Auditloglist");
        }


        //public Task<IPagedList<EmailConfigurationdetailListDto>> GetWhatsAppconfig(GridRequestModel objGrid)
        //{
        //    throw new NotImplementedException();
        //}



        //public Task<IPagedList<WhatsAppsendOutListDto>> GetWhatsAppconfig(GridRequestModel objGrid)
        //{
        //    return await DatabaseHelper.GetGridDataBySp<EmailSendoutListDto>(model, "ps_Rtrv_T_Emailoutlist");
        //}
    }
}
