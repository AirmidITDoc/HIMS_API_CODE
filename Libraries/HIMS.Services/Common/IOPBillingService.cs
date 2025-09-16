﻿using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;

namespace HIMS.Services.Common
{
    public partial interface IOPBillingService
    {
        Task InsertAsyncSP(Bill objBill, Payment objPayment, List<AddCharge> ObjaddCharge, int CurrentUserId, string CurrentUserName);
        //Task InsertAsyncSP1(Bill objBill,int CurrentUserId, string CurrentUserName);
        Task InsertCreditBillAsyncSP(Bill objBill, int currentUserId, string currentUserName);

        Task<IPagedList<CertificateInformationListDto>> GetListAsync(GridRequestModel objGrid);
        Task InsertAsync(TCertificateInformation TCertificateInformation, int UserId, string Username);

        Task UpdateAsync(TCertificateInformation TCertificateInformation, int UserId, string Username);
    }
}
