﻿using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Administration
{
    public partial interface IAdministrationService
    {
        Task<IPagedList<PaymentModeDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<RoleMasterListDto>> RoleMasterList(GridRequestModel objGrid);
        Task<IPagedList<BrowseOPDBillPagiListDto>> BrowseOPDBillPagiList(GridRequestModel objGrid);
        Task<IPagedList<BrowseIPAdvPayPharReceiptListDto>> BrowseIPAdvPayPharReceiptList(GridRequestModel objGrid);
        Task<IPagedList<ReportTemplateListDto>> BrowseReportTemplateList(GridRequestModel objGrid);

        Task InsertAsync(TExpense ObjTExpense, int UserId, string Username);
        Task UpdateExpensesAsync(TExpense ObjTExpense, int UserId, string Username, string[] strings);

        Task<IPagedList<DailyExpenceListtDto>> DailyExpencesList(GridRequestModel objGrid);
        Task TExpenseCancel(TExpense ObjTExpense, int UserId, string Username);
        Task DeleteAsync(Admission ObjAdmission, int UserId, string Username);
        Task UpdateAsync(Admission ObjAdmission, int UserId, string Username);

        Task PaymentUpdateAsync(Payment ObjPayment, int UserId, string Username);

        Task BilldateUpdateAsync(Bill ObjBill, int UserId, string Username);

        Task InsertAsync(MDoctorPerMaster ObjMDoctorPerMaster, int UserId, string Username);
        Task UpdateAsync(MDoctorPerMaster ObjMDoctorPerMaster, int UserId, string Username);
        Task DoctorShareInsertAsync(AddCharge ObjAddCharge, int UserId, string Username, DateTime FromDate, DateTime ToDate);


    }
}
