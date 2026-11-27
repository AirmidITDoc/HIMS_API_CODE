using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pathlogy
{
    public interface IDoctorAppointmentSlotsService
    {
        Task<IPagedList<DoctorAppointmentSlotsListDto>> GetDoctorAppointmentSlotsListAsync(GridRequestModel model);
        Task Insert(TDoctorAppointmentSlot Obj, int SlotDuration, int CurrentUserId, string CurrentUserName);
        Task Cancel(TDoctorAppointmentSlot Obj, int CurrentUserId, string CurrentUserName);
    }
}
