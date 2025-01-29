using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.IPPatient
{
    public partial interface IOTService
    {
      //  Task<IPagedList<TOtbookingRequest>> GetListAsync(GridRequestModel objGrid);
        Task InsertAsync(TOtbookingRequest objOTBooking, int UserId, string Username);
        Task UpdateAsync(TOtbookingRequest objOTBooking, int UserId, string Username);
        Task CancelAsync(TOtbookingRequest objOTBooking, int UserId, string Username);
     //   Task<TOtbookingRequest> GetById(int Id);


    }
}
