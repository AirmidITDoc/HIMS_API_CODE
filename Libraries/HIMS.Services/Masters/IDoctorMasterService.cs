using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Models;

namespace HIMS.Services.Masters
{
    public partial interface IDoctorMasterService
    {
        Task InsertAsync(DoctorMaster objDoctor, List<MDoctorDepartmentDet> objDepartment, int CurrentUserId, string CurrentUserName);
        Task InsertAsyncSP(DoctorMaster objDoctor, List<MDoctorDepartmentDet> objDepartment, int CurrentUserId, string CurrentUserName);
        Task UpdateAsync(DoctorMaster objDoctor, List<MDoctorDepartmentDet> objDepartment, int CurrentUserId, string CurrentUserName);
        Task InsertWithPOAsync(DoctorMaster objDoctor,  List<MDoctorDepartmentDet> objDepartment, int CurrentUserId, string CurrentUserName);
        Task UpdateWithPOAsync(DoctorMaster objDoctor,  List<MDoctorDepartmentDet> objDepartment, int CurrentUserId, string CurrentUserName);
        //Task DeleteAsync(DoctorMaster objDoctor, int CurrentUserId, string CurrentUserName);
    }
}
