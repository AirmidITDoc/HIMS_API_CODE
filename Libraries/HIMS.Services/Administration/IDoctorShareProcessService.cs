using HIMS.Data.Models;

namespace HIMS.Services.Administration
{
    public partial interface IDoctorShareProcessService
    {
        void DoctorShareInsert(AddCharge ObjAddCharge, int UserId, string Username, DateTime FromDate, DateTime ToDate);
        Task InsertAsync(MDoctorPerMaster ObjMDoctorPerMaster, int UserId, string Username);
        Task UpdateAsync(MDoctorPerMaster ObjMDoctorPerMaster, int UserId, string Username);




    }
}
