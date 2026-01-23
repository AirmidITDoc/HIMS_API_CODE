using DinkToPdf;
using DinkToPdf.Contracts;
using HIMS.API.Extensions;
using HIMS.API.PaymentGateway;
using HIMS.API.Utility;
using HIMS.Data;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Administration;
using HIMS.Services.Common;
using HIMS.Services.Dashboard;
using HIMS.Services.DoctorPayout;
using HIMS.Services.GastrologyService;
using HIMS.Services.Inventory;
using HIMS.Services.IPPatient;
using HIMS.Services.Marketing;
using HIMS.Services.Masters;
using HIMS.Services.MRD;
using HIMS.Services.Notification;
using HIMS.Services.Nursing;
using HIMS.Services.OPPatient;
using HIMS.Services.OTManagment;
using HIMS.Services.OutPatient;
using HIMS.Services.Pathlogy;
using HIMS.Services.Permissions;
using HIMS.Services.Pharmacy;
using HIMS.Services.Radiology;
using HIMS.Services.Report;
using HIMS.Services.Report.OPReports;
using HIMS.Services.Transaction;
using HIMS.Services.Users;
using HIMS.Services.Utilities;

namespace HIMS.API.Infrastructure
{
    /// <summary>
    /// Dependency registrar
    /// </summary>
    public static class DependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        public static void Register(IServiceCollection services)
        {
            // services.AddScoped<IDataProvider, MsSqlDataProvider>();
            //services.AddScoped(typeof(IRepository<>), typeof(EntityRepository<>));
            services.AddScoped<IFileUtility, FileUtility>();
            services.AddScoped<IMPathParaRangeWithAgeMasterService, MPathParaRangeWithAgeMasterService>();
            services.AddScoped<IClinicalCareService, ClinicalCareService>();

            services.AddScoped<IOTBookingRequestService, OTBookingRequestService>();
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
            services.AddScoped<IMParameterDescriptiveMasterService, MParameterDescriptiveMasterService>();
            services.AddScoped<IContext, HIMSDbContext>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<ISalesService, SalesService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<IGRNService, GRNService>();

            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IOPBillingReport, OPBillingReport>();
            services.AddTransient<IPdfUtility, PdfUtility>();
            services.AddScoped<IFavouriteService, FavouriteService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IIndentService, IndentService>();
            services.AddScoped<IIssueToDepService, IssueToDepService>();
            //services.AddScoped<IIssueToDeptIndentService, IssueToDeptIndentService>();
            services.AddScoped<IReportConfigService, ReportConfigService>();
            services.AddScoped<IOTService, OTService>();
            services.AddScoped<IEmergencyService, EmergencyService>();
            services.AddScoped<ITexpenseservice, Texpenseservice>();
            services.AddScoped<IRadilogyService, RadiologyService>();
            services.AddScoped<IQuestionMasterService, QuestionMasterService>();

            services.AddScoped<IPrescriptionOPTemplateService, PrescriptionOPTemplateService>();
            services.AddScoped<IGRNReturnService, GRNReturnService>();
            services.AddScoped<IStoreMasterService, StoreMasterService>();
            services.AddScoped<ISalesReturnService, SalesReturnService>();

            services.AddScoped<IOpeningBalanceService, OpeningBalanceService>();
            services.AddScoped<IWorkOrderService, WorkOrderService>();
            services.AddScoped<INursingConsentService, NursingConsentService>();


            services.AddScoped<ICampMasterService, CampMasterService>();


            services.AddScoped<ITestMasterServices, TestMasterService>();
            services.AddScoped<IPriscriptionReturnService, PriscriptionReturnService>();
            services.AddScoped<IPrefixService, PrefixService>();
            services.AddScoped<ILabRequestService, LabRequestService>();
            services.AddScoped<IMPrescriptionService, MPrescriptionService>();
            services.AddScoped<IStockAdjustmentService, StockAdjustmentService>();
            services.AddScoped<IMaterialConsumption, MaterialConsumptionService>();
            services.AddScoped<IItemMovementService, ItemMovementService>();
            services.AddScoped<IStockReportDayWiseService, StockReportDayWiseService>();
            services.AddScoped<ICurrentStockService, CurrentStockService>();
            services.AddScoped<IItemWiseService, ItemWiseService>();
            services.AddScoped<IAdministrationService, AdministrationService>();
            services.AddScoped<IReportTemplateService, ReportTemplateService>();
            services.AddScoped<IOPDPrescriptionMedicalService, OPDPrescriptionMedicalService>();
            services.AddScoped<IMenuMasterService, MenuMasterService>();
            services.AddScoped<IPaymentpharmacyService, paymentpharmacyService>();
            services.AddScoped<IMedicalRecordService, MedicalRecordService>();
            services.AddScoped<INursingNoteService, NursingNoteService>();
            services.AddScoped<IDoctorShareProcessService, DoctorShareProcessService>();
            services.AddScoped<IAdminTaskService, AdminTaskService>();
            services.AddScoped<ITemplateDescriptionConfigService, TemplateDescriptionConfigService>();
            services.AddScoped<IBedMasterService, BedMasterService>();
            services.AddScoped<IOTPreOperationService, OTPreOperationService>();
            services.AddScoped<IOTAnesthesiaService, OTAnesthesiaService>();
            services.AddScoped<IOTInOperationService, OTInOperationService>();
            services.AddScoped<IOTOperativeNotes, OTOperativeNotesService>();


            services.AddScoped<IPaymentModeService, PaymentModeService>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IOPAddchargesService, OPAddchargesService>();
            services.AddScoped<IOPPayment, OPPayment>();
            services.AddScoped<IOPCreditBillService, OPCreditBillService>();
            services.AddScoped<IOPSettlementCreditService, OPSettlementCreditService>();
            services.AddScoped<IIPBIllwithpaymentService, IPBIllwithpaymentService>();
            services.AddScoped<IIPBillwithCreditService, IPBillwithCreditService>();
            services.AddScoped<IAdvanceService, AdvanceService>();
            services.AddScoped<IParameterMasterService, ParameterMasterService>();
            services.AddScoped<IPathlogyService, PathlogyService>();
            services.AddScoped<ISubTPACompanyService, SubTPACompanyService>();
            services.AddScoped<ICompanyMasterService, CompanyMasterService>();
            services.AddScoped<IPrescriptionService, PrescriptionServise>();
            services.AddScoped<IWardMasterService, WardMasterService>();
            services.AddScoped<ITprocessOtpService, TprocessOtpService>();
            services.AddScoped<IDoctorPayService, DoctorPayService>();
            services.AddScoped<IPharmacyReorderService, PharmacyReorderService>();
            services.AddScoped<IPharmacyItemSummaryService, PharmacyItemSummaryService>();


            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IItemMasterService, ItemMasterServices>();
            services.AddScoped<IRadiologyTestService, RadiologyTestService>();
            services.AddScoped<IsmsConfigService, smsConfigService>();
            services.AddScoped<ISupplierPaymentStatusService, SupplierPaymentStatusService>();
            services.AddScoped<IMarketingService, MarketingService>();
            services.AddScoped<IInPatientService, InPatientService>();
            services.AddScoped<IGastrologyEMRService, GastrologyEMRService>();
            services.AddScoped<ITallyService, TallyService>();
            services.AddScoped<IEstimasteService, EstimateSerive>();







            services.AddScoped<IBillingService, BillingService>();
            services.AddScoped<ILoginService, LoginService>();

            services.AddScoped<IOPRefundOfBillService, OPRefundOfBillService>();

            services.AddScoped<IBedTransferService, BedTransferService>();
            services.AddScoped<IIPrescriptionService, IPPrescriptionService>();
            services.AddScoped<IDischargeServiceSP, DischargeServiceSP>();
            services.AddScoped<IOPSettlementService, OPSettlementService>();
            services.AddScoped<IOPBillShilpaService, OPBillShilpaService>();
            services.AddScoped<ILabPatientRegistrationService, LabPatientRegistrationService>();
            services.AddScoped<ILabBrowseListService, LabBrowseListService>();


            services.AddScoped<IDischargeSummaryService, DischargeSummaryService>();
            services.AddScoped<IIPBillService, IPBIllService>();
            services.AddScoped<IIPBILLCreditService, IPBILLCreditService>();
            services.AddScoped<IIPDraftBillSerive, IPDraftBillSerive>();
            services.AddScoped<IIPDraftBillSerive, IPDraftBillSerive>();
            services.AddScoped<IIPInterimBillSerive, IPInterimBillSerive>();


            services.AddScoped<IVisitDetailsService, VisitDetailsService>();
            services.AddScoped<IAdmissionService, AdmissionService>();
            services.AddScoped<IPathlogySampleCollectionService, PathlogySampleCollectionService>();
            services.AddScoped<IDoctorSharePerCalculationService, DoctorSharePerCalculationService>();
            services.AddScoped<ICanteenRequestService, CanteenRequestService>();
            services.AddScoped<IPhoneAppointment2Service, PhoneAppointment2Service>();

            services.AddScoped<IDoctorMasterService, DoctorMasterService>();
            services.AddScoped<IDoctorShareMasterService, DoctorShareMasterService>();
            services.AddScoped<IHospitalMasterService, HospitalMasterService>();
            services.AddScoped<INotificationUtility, NotificationUtility>();

            services.AddScoped<I_MRDCertificate, MRDService>();
            services.AddScoped<IOPBillingService, OPBillingService>();
            services.AddScoped<IBillCancellationService, BillCancellationService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IConsRefDoctorService, ConsRefDoctorService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IBarcodeConfigService, BarcodeConfigService>();
            services.AddScoped<IWhatsAppEmailService, WhatsAppEmailService>();
            services.AddScoped<IConfigService, ConfigService>();

            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddHttpClient<MpesaAuthService>();
            services.AddHttpClient<MpesaStkService>();

            services.AddScoped<DinkToPdfService>();
            services.AddHttpContextAccessor();
            services.AddMemoryCache();
        }
    }
}

