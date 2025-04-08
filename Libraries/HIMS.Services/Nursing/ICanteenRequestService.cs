using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Nursing;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Nursing
{
    public partial  interface ICanteenRequestService
    {
        
            Task InsertAsync(TCanteenRequestHeader objCanteen, int UserId, string Username);
            Task<IPagedList<CanteenRequestListDto>> CanteenRequestsList(GridRequestModel objGrid);
           Task<IPagedList<CanteenRequestHeaderListDto>> CanteenRequestHeaderList(GridRequestModel objGrid);
           Task<IPagedList<DoctorNoteListDto>> DoctorNoteList(GridRequestModel objGrid);
          Task<IPagedList<TDoctorPatientHandoverListDto>> TDoctorPatientHandoverList(GridRequestModel objGrid);

        Task<List<CanteenListDto>> GetItemList(string ItemName);
      

    }
}
