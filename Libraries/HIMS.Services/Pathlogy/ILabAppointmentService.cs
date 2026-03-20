using HIMS.Core.Domain.Grid;
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
    public partial interface ILabAppointmentService
    {
        Task InsertAsync(TLabAppointment ObjTLabAppointment, int UserId, string Username);
        Task UpdateAsync(TLabAppointment ObjTLabAppointment, int UserId, string Username, string[]? references);
        Task<IPagedList<LabAppointmentListDto>> GetListAsync(GridRequestModel objGrid);
        Task<List<TLabAppointment>> GetLabAppoinments(int DocId, DateTime FromDate, DateTime ToDate, int? CategoryId);




    }
}
