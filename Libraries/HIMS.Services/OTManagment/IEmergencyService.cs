using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;

namespace HIMS.Services.OTManagment
{
    public partial interface IEmergencyService
    {
        Task<IPagedList<EmergencyListDto>> GetListAsyn(GridRequestModel objGrid);
        void InsertSP(TEmergencyAdm objTEmergencyAdm, int UserId, string Username);
        void UpdateSP(TEmergencyAdm objTEmergencyAdm, int UserId, string UserName);
        void CancelSP(TEmergencyAdm objTEmergencyAdm, int UserId, string UserName);
        void Update(AddCharge ObjAddCharge, int UserId, string UserName, long EmgId, long NewAdmissionId);
        Task<List<EmergencyAutoCompleteDto>> SearchRegistration(string str);

    }
}
