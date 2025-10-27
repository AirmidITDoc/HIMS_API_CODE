using HIMS.Data.Models;

namespace HIMS.Services.Masters
{
    public partial interface IDoctorShareMasterService
    {
        Task InsertAsync(MDoctorPerMaster objMDoctorPerMaster, int UserId, string Username);
        Task UpdateAsync(MDoctorPerMaster objMDoctorPerMaster, int UserId, string Username);

    }
}
