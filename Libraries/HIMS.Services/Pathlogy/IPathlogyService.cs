using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pathlogy
{
    public partial interface IPathlogyService
    {
        Task<IPagedList<PathTestForUpdateListdto>> PathTestForUpdateList(GridRequestModel objGrid);
        Task<IPagedList<PathTemplateForUpdateListDto>> PathTemplateForUpdateList(GridRequestModel objGrid);
        Task<IPagedList<PathParaFillListDto>> PathParaFillList(GridRequestModel objGrid);
        Task<IPagedList<PathSubtestFillListDto>> PathSubtestFillList(GridRequestModel objGrid);
        //Task<IPagedList<PathologyTestListDto>> PathologyTestList(GridRequestModel objGrid);
        Task<IPagedList<PathResultEntryListDto>> PathResultEntry(GridRequestModel objGrid);
        Task<IPagedList<PatientTestListDto>> PGetListAsync(GridRequestModel objGrid);
        Task<IPagedList<LabOrRadRequestListDto>> LGetListAsync(GridRequestModel objGrid);
        Task InsertAsyncResultEntry(TPathologyReportDetail ObjPathologyReportDetail, TPathologyReportHeader ObjTPathologyReportHeader,int  UserId, string Username);

        Task<IPagedList<TestListDTo>> GetPathTestListAsync(GridRequestModel objGrid);



    }
}
