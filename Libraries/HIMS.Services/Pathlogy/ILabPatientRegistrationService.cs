using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
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
       


        Task<IPagedList<LabResultDetailsListDto>> LabResultDetailsListAsynch(GridRequestModel objGrid);

        Task<IPagedList<LabSampleCollectionListDto>> GetSamColListAsync(GridRequestModel objGrid);

        Task<IPagedList<LabSampleCollectionDetailListDto>> GetSamColListDetailAsync(GridRequestModel objGrid);

        Task<IPagedList<LabregBilldetailListDto>> GetBillDetailListAsync(GridRequestModel objGrid);

        Task InsertAsync(TLabPatientRegistration ObjTLabPatientRegistration, int UserId, string Username);
        Task UpdateAsync(TLabPatientRegisteredMaster ObjTLabPatientRegistration, int UserId, string Username, string[]? references);
        Task InsertAsyncSP(TLabPatientRegistration ObjTLabPatientRegistration, Bill objBill, Payment objPayment, List<AddCharge> ObjaddCharge,List<TPayment> ObjTPayment, int CurrentUserId, string CurrentUserName);
        Task InsertPaidBillAsync(TLabPatientRegistration ObjTLabPatientRegistration, Bill objBill, Payment objPayment, List<AddCharge> ObjaddCharge, List<TPayment> ObjTPayment, int CurrentUserId, string CurrentUserName);

        //Task<List<TLabPatientRegistration>> SearchlabRegistration(string str);

        List<LabVisitDetailsListSearchDto> SearchlabRegistration(long UnitId ,string Keyword );
        Task<IPagedList<PrevDrVisistListDto>> GeOPPreviousDrVisitListAsync(GridRequestModel objGrid);
        Task<List<TLabPatientRegistration>> SearchLabRegistration(string str);

        Task<IPagedList<WhatsAppsendOutListDto>> GetLabPatientWhatsAppconfig(GridRequestModel objGrid);

        Task<IPagedList<EmailSendoutListDto>> GetLabPatientEmailSconfig(GridRequestModel objGrid);


      
        Task<IPagedList<LabResultListDto>> LabResultListAsync(GridRequestModel objGrid);

        Task<IPagedList<LabResultDetailsListDto>> LabApprovalResultListAsync(GridRequestModel objGrid);


        //Task<IPagedList<PatientEstimateListDto>> GetPatientEstimate(GridRequestModel objGrid);
        //Task<IPagedList<PatientEstimateDetailsListDto>> GetPatientEstimateDetail(GridRequestModel objGrid);
        Task<IPagedList<LabDiscountDetailListDto>> LabDiscountDetailListAsync(GridRequestModel model);

        Task<IPagedList<LabPaymentDetailListDto>> LabPaymentDetailListAsync(GridRequestModel objGrid);
        Task<IPagedList<LabCreditDetailDto>> LabCreditDetailListAsync(GridRequestModel objGrid);
        List<CompanyComboDto> CompanyComboList(string keywoard);



    }

}

