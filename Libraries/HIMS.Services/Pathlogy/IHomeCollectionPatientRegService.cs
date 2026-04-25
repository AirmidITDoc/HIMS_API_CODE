using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using HIMS.Services.Pharmacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pathlogy
{
    public partial interface IHomeCollectionPatientRegService
    {
        Task InsertAsync(THomeCollectionPatientRegistartion ObjTHomeCollectPatientRegistartion, int UserId, string Username);
        Task<IPagedList<HomeCollectionPatientRegistartionListDto>> GetListAsync(GridRequestModel objGrid);



    }
}
