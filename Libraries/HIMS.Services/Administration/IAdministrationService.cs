using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;

namespace HIMS.Services.Administration
{
    public partial interface IAdministrationService
    {
        Task<IPagedList<PaymentModeDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<RoleMasterListDto>> RoleMasterList(GridRequestModel objGrid);
        Task<IPagedList<BrowseOPDBillPagiListDto>> BrowseOPDBillPagiList(GridRequestModel objGrid);
        Task<IPagedList<BrowseIPAdvPayPharReceiptListDto>> BrowseIPAdvPayPharReceiptList(GridRequestModel objGrid);
        Task<IPagedList<ReportTemplateListDto>> BrowseReportTemplateList(GridRequestModel objGrid);
        Task DeleteAsync(Admission ObjAdmission, int UserId, string Username);
        void Update(Admission ObjAdmission, int UserId, string Username);

        void PaymentUpdate(Payment ObjPayment, int UserId, string Username);

        Task BilldateUpdateAsync(Bill ObjBill, int CurrentUserId, string CurrentUserName);

        //Task DoctorShareInsertAsync(AddCharge ObjAddCharge, int UserId, string Username, DateTime FromDate, DateTime ToDate);
        void Insert(List<MAutoServiceList> ObjMAutoServiceList, int currentUserId, string currentUserName);
        Task InsertListAsync(List<MAutoServiceList> ObjMAutoServiceList, int currentUserId, string currentUserName);






    }
}
