using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services
{
    public partial  interface IOTService
    {
        Task<IPagedList<OTBookinglistDto>> GetListAsync(GridRequestModel objGrid);
        Task InsertAsync(TOtbooking OBJTOtbooking, int UserId, string Username);
        Task UpdateAsync(TOtbooking OBJTOtbooking, int UserId, string Username);

    }
}
