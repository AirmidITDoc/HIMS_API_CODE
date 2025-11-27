using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.DTO.OTManagement;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services
{
    public partial interface IOTPreOperationService
    {
        Task InsertAsync(TOtPreOperationHeader ObjTOtPreOperationHeader, int UserId, string Username);
        Task UpdateAsync(TOtPreOperationHeader ObjTOtReservationHeader, int UserId, string Username, string[]? references);
        Task<IPagedList<perOperationsurgeryListDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<PreOperationAttendentListDto>> preOperationAttendentListAsync(GridRequestModel objGrid);
        Task<List<OtpreOperationDiagnosisListDto>> PreOperationDiagnosisListAsync(string descriptionType);
        //Task<List<OtpreOperationDiagnosisListDto>> PreOperationDiagnosisListAsync(string descriptionType);






    }
}
