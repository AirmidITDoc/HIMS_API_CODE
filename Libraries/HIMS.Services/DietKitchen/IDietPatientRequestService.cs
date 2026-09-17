using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.DietKitchen;
using HIMS.Data.DTO.OTManagement;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.DietKitchen
{
    public partial interface IDietPatientRequestService
    {
        Task InsertAsync(TDietPatientRequestHeader ObjTDietPatientRequestHeader, int UserId, string Username);
        Task UpdateAsync(TDietPatientRequestHeader ObjTDietPatientRequestHeader, int UserId, string Username, string[]? references);
        Task Cancel(TDietPatientRequestHeader ObjTDietPatientRequestHeader, int UserId, string Username);
        Task CancelD(TDietPatReqDetail ObjTDietPatReqDetail, int UserId, string Username);
        Task AcceptD(TDietPatReqDetail ObjTDietPatReqDetail, int UserId, string Username);
        Task<IPagedList<DietPatientRequestHeaderListDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<DietPatientRequestDetailsListDto>> GetListDetailsAsync(GridRequestModel objGrid);


        


    }
}

