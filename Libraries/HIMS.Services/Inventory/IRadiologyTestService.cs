using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LinqToDB.Reflection.Methods.LinqToDB.Insert;

namespace HIMS.Services.Inventory
{
    public partial interface IRadiologyTestService
    {
        Task<IPagedList<RadiologyListDto>> GetListAsync(GridRequestModel objGrid);

        Task InsertAsyncSP(MRadiologyTestMaster objRadio, int UserId, string Username);
        Task InsertAsync(MRadiologyTestMaster objRadio, int UserId, string Username);
        Task CancelAsync(MRadiologyTestMaster objRadio, int CurrentUserId, string CurrentUserName);
        Task UpdateAsync(MRadiologyTestMaster objRadio, int UserId, string Username);
        Task<List<MRadiologyTestMaster>> GetAllRadiologyTest();
        //Task<MRadiologyTestMaster> GetByIdRadiologyTest(long Id);
    }
}
