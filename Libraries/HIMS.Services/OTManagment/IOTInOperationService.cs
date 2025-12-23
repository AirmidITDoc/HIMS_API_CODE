using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.DTO.OTManagement;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OTManagment
{
    public partial interface IOTInOperationService
    {
        Task InsertAsync(TOtInOperationHeader ObjTOtInOperationHeader, int UserId, string Username);
        Task UpdateAsync(TOtInOperationHeader ObjTOtInOperationHeader, int UserId, string Username, string[]? references);
        Task<IPagedList<InOperationAttendingDetailsListDto>> InOperationAttengingDetailsAsync(GridRequestModel objGrid);
        Task<IPagedList<InOperationSurgeryDetailsDto>> InOperationSurgeryDetailsAsync(GridRequestModel objGrid);
        Task<List<TOtInOperationPostOperDiagnosisDto>> InOperationPostOperDiagnosisListAsync(string descriptionType);
        Task<List<TOtInOperationDiagnosisDto>> InOperationDiagnosisListAsync(string descriptionType);





    }
}
