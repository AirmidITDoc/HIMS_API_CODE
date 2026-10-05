using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OTManagement;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Inventory
{
    public partial  interface ISurgeryMasterService
    {
       List<MOtSurgeryMaster> GetSurgeryNameBySurgeryType(int SiteDescId);
        Task InsertAsync(MOtSurgeryMaster ObjMOtSurgeryMaster, int UserId, string Username);
        Task UpdateAsync(MOtSurgeryMaster ObjMOtSurgeryMaster, int UserId, string Username, string[]? ignoreColumns = null);
        Task<IPagedList<SurgeryMasterListDto>> GetListAsync(GridRequestModel objGrid);




    }
}
