using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.OTManagement;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OTManagment
{
    public partial interface IBillTemplateService
    {
        Task<IPagedList<OtBillTemplateListDto>> GetListAsync(GridRequestModel objGrid);
        Task<MOtBillTemplateHeader> GetById(long Id);

        Task InsertAsync(MOtBillTemplateHeader ObjMOtBillTemplateHeader, int UserId, string Username);
        Task UpdateAsync(MOtBillTemplateHeader ObjMOtBillTemplateHeader, int UserId, string Username, string[]? ignoreColumns = null);


    }
}
