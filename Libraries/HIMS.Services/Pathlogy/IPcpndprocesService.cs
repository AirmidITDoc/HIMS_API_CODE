using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pathlogy
{
    public partial interface IPcpndprocesService
    {
        Task InsertAsync(TPcpndprocess ObjTPcpndprocess, int UserId, string Username);
        Task UpdateAsync(TPcpndprocess ObjTPcpndprocess, int UserId, string Username, string[]? ignoreColumns = null);
        Task<IPagedList<RadioPcpndtListDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<IndicationListDto>> GetList(GridRequestModel objGrid);




    }
}
