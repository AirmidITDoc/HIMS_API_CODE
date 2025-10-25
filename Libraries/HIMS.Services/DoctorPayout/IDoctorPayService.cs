using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;
using HIMS.Services.Pharmacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.DoctorPayout
{
    public partial interface IDoctorPayService
    {
        Task InsertAsync(TAdditionalDocPay ObjTAdditionalDocPay, int CurrentUserId, string CurrentUserName);
        Task<IPagedList<DoctorPayListDto>> GetList(GridRequestModel objGrid);

        Task<IPagedList<DoctorBilldetailListDto>> GetBillDetailList(GridRequestModel objGrid);

        Task<IPagedList<DoctorPaysummarydetailListDto>> GetDoctorsummaryDetailList(GridRequestModel objGrid);


        Task<IPagedList<DcotorpaysummaryListDto>> GetDoctroSummaryList(GridRequestModel objGrid);
        Task UpdateAsync(List<AddCharge> ObjAddCharge, int CurrentUserId, string CurrentUserName);

    }
}
