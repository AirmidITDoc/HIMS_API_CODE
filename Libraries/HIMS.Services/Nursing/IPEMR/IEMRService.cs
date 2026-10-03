using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Nursing.IPEMR;
using HIMS.Data.DTO.OTManagement;
using HIMS.Data.Models;

namespace HIMS.Services.Nursing.IPEMR
{
    public partial interface IEMRService
    {


        Task<IPagedList<IPEMRDignosisHistoryListDto>> GetIPEMRDignosisHistoryAsync(GridRequestModel objGrid);
        Task<IPagedList<IPEMRFamilyMedicalHistoryListDto>> GetIPEMRFamilyMedicalHistoryAsync(GridRequestModel objGrid);
        Task<IPagedList<IPEMRDiagnosisInfoListDto>> GetIPEMRDiagnosisInfoAsync(GridRequestModel objGrid);
        Task<TIpEmrhistory?> GetByIdAsync(long id);
        Task InsertAsync(TIpEmrhistory objEMR, int userId, string username);
        Task UpdateAsync(TIpEmrhistory objEMR, int userId, string username, string[]? ignoreColumns = null);
        Task InsertFamilyHistoryAsync(TIpEmrfamilyMedicalHistory objFamilyHistory, int UserId, string Username);
        Task UpdateFamilyHistoryAsync(TIpEmrfamilyMedicalHistory objFamilyHistory, int UserId, string Username, string[]? ignoreColumns = null);
        Task<TIpEmrfamilyMedicalHistory> GetFamilyHistoryByIdAsync(int id);
    }
}