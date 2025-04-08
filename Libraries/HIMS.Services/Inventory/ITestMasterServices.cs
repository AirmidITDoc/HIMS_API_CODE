using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Inventory
{
    public partial interface ITestMasterServices
    {
        Task<IPagedList<PathTestListDto>> PetListAsync(GridRequestModel objGrid);
        Task<IPagedList<PathTestForUpdateListdto>> ListAsync(GridRequestModel objGrid);

        Task<IPagedList<SubTestMasterListDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<PathTemplateForUpdateListDto>> PathTemplateList(GridRequestModel objGrid);

        Task InsertAsync(MPathTestMaster objTest, int UserId, string Username);
        Task InsertAsyncSP(MPathTestMaster objTest, List<MPathTemplateDetail> ObjMPathTemplateDetail, List<MPathTestDetailMaster> ObjMPathTestDetailMaster ,int UserId, string Username);
        Task UpdateAsyncSP(MPathTestMaster objTest, MPathTemplateDetail ObjMPathTemplateDetail, MPathTestDetailMaster ObjMPathTestDetailMaster, int UserId, string Username);

        Task UpdateAsync(MPathTestMaster objTest, int UserId, string Username);


    }
}
