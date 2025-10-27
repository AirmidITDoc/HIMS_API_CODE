using HIMS.Data.Models;

namespace HIMS.Services.IPPatient
{
    public partial interface IMedicalRecordService
    {
        Task InsertAsync(TIpmedicalRecord objTIpmedicalRecord, int UserId, string Username);
        Task UpdateAsync(TIpmedicalRecord objTIpmedicalRecord, int UserId, string Username);
    }
}
