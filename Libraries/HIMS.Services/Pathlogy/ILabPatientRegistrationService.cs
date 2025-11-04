using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pathlogy
{
    public partial interface ILabPatientRegistrationService
    {
        Task<IPagedList<LabPatientRegistrationListDto>> GetListAsync(GridRequestModel objGrid);

        Task InsertAsync(TLabPatientRegistration ObjTLabPatientRegistration, int UserId, string Username);
        Task UpdateAsync(TLabPatientRegistration ObjTLabPatientRegistration, int UserId, string Username, string[]? references);
        Task InsertAsyncSP(TLabPatientRegistration ObjTLabPatientRegistration, List<TLabTestRequest> OBjTLabTestRequest, Bill objBill, Payment objPayment, List<AddCharge> ObjaddCharge, int CurrentUserId, string CurrentUserName);



    }
}
