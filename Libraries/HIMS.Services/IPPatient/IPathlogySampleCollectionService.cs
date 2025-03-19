using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.IPPatient
{
    public partial interface IPathlogySampleCollectionService
    {
        Task UpdateAsyncSP(List<TPathologyReportHeader> objTPathologyReportHeader, int UserId, string Username);
        Task<IPagedList<SampleCollectionPatientListDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<SampleCollectionTestListDto>> GetListAsyn(GridRequestModel objGrid);
        Task<IPagedList<LabOrRadRequestListDto>> LGetListAsync(GridRequestModel objGrid);
        Task<IPagedList<LabOrRadRequestDetailListDto>> LGetListAsync1(GridRequestModel objGrid);
        Task<IPagedList<PathRadServiceListDto>> GetListAsync1(GridRequestModel objGrid);



    }
}
