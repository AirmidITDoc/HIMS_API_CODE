using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OutPatient
{
    public partial interface IDischargeServiceSP
    {
        void InsertSP(Discharge objDischarge, Admission objAdmission, int currentUserId, string currentUserName);
        void UpdateSP(Discharge objDischarge, Admission objAdmission, int currentUserId, string currentUserName);
        Task<IPagedList<DischargeDateListDto>> GetListAsync(GridRequestModel objGrid);

    }
}
