using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pathlogy
{
    public partial interface ILabPatientRegistrationService
    {
        Task<IPagedList<LabPatientRegistrationListDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<LabResultListDto>> LabResultListAsync(GridRequestModel objGrid);
        Task<IPagedList<LabResultDetailsListDto>> LabResultDetailsListAsynch(GridRequestModel objGrid);

        Task<IPagedList<LabSampleCollectionListDto>> GetSamColListAsync(GridRequestModel objGrid);

        Task<IPagedList<LabSampleCollectionDetailListDto>> GetSamColListDetailAsync(GridRequestModel objGrid);

        Task<IPagedList<LabregBilldetailListDto>> GetBillDetailListAsync(GridRequestModel objGrid);

        Task InsertAsync(TLabPatientRegistration ObjTLabPatientRegistration, int UserId, string Username);
        Task UpdateAsync(TLabPatientRegisteredMaster ObjTLabPatientRegistration, int UserId, string Username, string[]? references);
        Task InsertAsyncSP(TLabPatientRegistration ObjTLabPatientRegistration, Bill objBill, Payment objPayment, List<AddCharge> ObjaddCharge,List<TPayment> ObjTPayment, int CurrentUserId, string CurrentUserName);
        Task InsertPaidBillAsync(TLabPatientRegistration ObjTLabPatientRegistration, Bill objBill, Payment objPayment, List<AddCharge> ObjaddCharge, List<TPayment> ObjTPayment, int CurrentUserId, string CurrentUserName);

        //Task<List<TLabPatientRegistration>> SearchlabRegistration(string str);

        List<LabVisitDetailsListSearchDto> SearchlabRegistration(string Keyword, long UnitId);
        Task<IPagedList<PrevDrVisistListDto>> GeOPPreviousDrVisitListAsync(GridRequestModel objGrid);
        Task<List<TLabPatientRegistration>> SearchLabRegistration(string str);


    }
}
