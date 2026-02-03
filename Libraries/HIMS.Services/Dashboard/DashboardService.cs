using HIMS.Core.Domain.Dashboard;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO;
using HIMS.Data.Models;
using HIMS.Services.Masters;
using LinqToDB;
using Microsoft.Data.SqlClient;
using System.Data;
using static LinqToDB.Common.Configuration;

namespace HIMS.Services.Dashboard
{
    public class DashboardService : IDashboardService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public DashboardService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public async Task<List<DailyDashboardSummaryModel>> GetDailyDashboardSummary()
        {
            var query1 = (from M in _context.VisitDetails
                          where M.VisitDate.Value.Year == AppTime.Now.Year && M.VisitDate.Value.Month == AppTime.Now.Month && M.VisitDate.Value.Day == AppTime.Now.Day
                          select new DailyDashboardSummaryModel()
                          {
                              Title = "OPD",
                              Label = "Todays",
                              Count = _context.VisitDetails.Count(),
                              SelfLbl = "Self",
                              SelfCnt = _context.VisitDetails.Where(sub => sub.CompanyId == 0 && (sub.VisitDate.Value.Year == AppTime.Now.Year && sub.VisitDate.Value.Month == AppTime.Now.Month && sub.VisitDate.Value.Day == AppTime.Now.Day)).Count(),
                              CompanyLbl = "Company",
                              CompayCnt = _context.VisitDetails.Where(sub => sub.CompanyId != 0 && (sub.VisitDate.Value.Year == AppTime.Now.Year && sub.VisitDate.Value.Month == AppTime.Now.Month && sub.VisitDate.Value.Day == AppTime.Now.Day)).Count()
                          });

            var query2 = (from M in _context.Admissions
                          where M.IsDischarged == 0 && M.IsCancelled == 0
                          select new DailyDashboardSummaryModel()
                          {
                              Title = "IPD",
                              Label = "Currently Admitted",
                              Count = _context.Admissions.Count(),
                              SelfLbl = "Admission",
                              SelfCnt = _context.Admissions.Where(sub => sub.IsCancelled == 0 && (sub.AdmissionDate.Value.Year == AppTime.Now.Year && sub.AdmissionDate.Value.Month == AppTime.Now.Month && sub.AdmissionDate.Value.Day == AppTime.Now.Day)).Count(),
                              CompanyLbl = "Discharge",
                              CompayCnt = _context.Admissions.Where(sub => sub.IsCancelled != 0 && (sub.AdmissionDate.Value.Year == AppTime.Now.Year && sub.AdmissionDate.Value.Month == AppTime.Now.Month && sub.AdmissionDate.Value.Day == AppTime.Now.Day)).Count()
                          });


            var query3 = (from M in _context.TSalesHeaders
                          where M.Date.Value.Year == AppTime.Now.Year && M.Date.Value.Month == AppTime.Now.Month && M.Date.Value.Day == AppTime.Now.Day
                          select new DailyDashboardSummaryModel()
                          {
                              Title = "Pharmacy",
                              Label = "Pharmacy",
                              Count = _context.TSalesHeaders.Count(),
                              SelfLbl = "OP Patient",
                              SelfCnt = _context.TSalesHeaders.Where(sub => (sub.OpIpType == 0 || sub.OpIpType == 2) && (sub.Date.Value.Year == AppTime.Now.Year && sub.Date.Value.Month == AppTime.Now.Month && sub.Date.Value.Day == AppTime.Now.Day)).Count(),
                              CompanyLbl = "IP Patient",
                              CompayCnt = _context.TSalesHeaders.Where(sub => sub.OpIpType == 1 && (sub.Date.Value.Year == AppTime.Now.Year && sub.Date.Value.Month == AppTime.Now.Month && sub.Date.Value.Day == AppTime.Now.Day)).Count(),
                          });

            var cnt = (from M in _context.VisitDetails
                       join F in _context.MDepartmentMasters on M.DepartmentId equals F.DepartmentId
                       where M.VisitDate.Value.Year == AppTime.Now.Year && M.VisitDate.Value.Month == AppTime.Now.Month && M.VisitDate.Value.Day == AppTime.Now.Day //&& F.DepartmentId == 7
                       select M.VisitId).Count();

            var query4 = (from M in _context.VisitDetails
                          join F in _context.MDepartmentMasters on M.DepartmentId equals F.DepartmentId
                          where M.VisitDate.Value.Year == AppTime.Now.Year && M.VisitDate.Value.Month == AppTime.Now.Month && M.VisitDate.Value.Day == AppTime.Now.Day //&& F.DepartmentId == 7
                          select new DailyDashboardSummaryModel()
                          {
                              Title = "Dialysis",
                              Label = "Dialysis",
                              Count = cnt,//_context.VisitDetails.Count(),
                              SelfLbl = "OP Patient",
                              SelfCnt = cnt,// _context.VisitDetails.Count(),
                              CompanyLbl = "IP Patient",
                              CompayCnt = 0,
                          });

            //var allResults = query1;
            var allResults = query1.Concat(query2).Concat(query3).Concat(query4);
            return await allResults.ToListAsync();
        }

        public async Task<List<OPDepartmentRangeChartModel>> GetOPDepartmentRangeChart(OPDepartmentRangeChartRequestModel model)
        {
            int daysInMonth = DateTime.DaysInMonth(AppTime.Now.Year, AppTime.Now.Month);
            var firstDay = new DateTime(AppTime.Now.Year, AppTime.Now.Month, 1);
            var lastDay = new DateTime(AppTime.Now.Year, AppTime.Now.Month, daysInMonth);

            DateTime StartDate = firstDay.AddMonths(-1);
            DateTime EndDate = lastDay.AddMonths(-1);
            DateTime TodaysDate = AppTime.Now;

            DateTime WeekStartDate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday).AddDays(-7);
            DateTime WeekEndDate = WeekStartDate.AddDays(6);

            var query = (IQueryable<OPDepartmentRangeChartModel>)null;
            if (model.DateRange == "Todays")
            {
                query = (from M in _context.VisitDetails
                         join F in _context.MDepartmentMasters on M.DepartmentId equals F.DepartmentId
                         where M.VisitDate.Value.Year == TodaysDate.Year && M.VisitDate.Value.Month == TodaysDate.Month && M.VisitDate.Value.Day == TodaysDate.Day
                         group F by F.DepartmentName into pg
                         select new OPDepartmentRangeChartModel()
                         {
                             Name = pg.FirstOrDefault().DepartmentName,
                             Value = pg.Count(),
                             TotalCount = _context.VisitDetails.Count(),
                             DischargeCount = 0
                         });
            }
            else if (model.DateRange == "Last Weeks")
            {
                query = (from M in _context.VisitDetails
                         join F in _context.MDepartmentMasters on M.DepartmentId equals F.DepartmentId
                         where M.VisitDate >= WeekStartDate && M.VisitDate <= WeekEndDate
                         group F by F.DepartmentName into pg
                         select new OPDepartmentRangeChartModel()
                         {
                             Name = pg.FirstOrDefault().DepartmentName,
                             Value = pg.Count(),
                             TotalCount = _context.VisitDetails.Where(M => M.VisitDate >= WeekStartDate && M.VisitDate <= WeekEndDate).Count(),
                             DischargeCount = 0
                         });
            }
            else if (model.DateRange == "Last Months")
            {
                query = (from M in _context.VisitDetails
                         join F in _context.MDepartmentMasters on M.DepartmentId equals F.DepartmentId
                         where M.VisitDate >= StartDate && M.VisitDate <= EndDate
                         group F by F.DepartmentName into pg
                         select new OPDepartmentRangeChartModel()
                         {
                             Name = pg.FirstOrDefault().DepartmentName,
                             Value = pg.Count(),
                             TotalCount = _context.VisitDetails.Where(M => M.VisitDate >= StartDate && M.VisitDate <= EndDate).Count(),
                             DischargeCount = 0
                         });
            }
            return await query.ToListAsync();
        }

        public IPAdemissionDischargeCountModel GetIPAdemissionDischargeCount()
        {
            IPAdemissionDischargeCountModel res = new()
            {
                AppointmentCount = _context.VisitDetails.Where(M => M.VisitDate.Value.Year == AppTime.Now.Year && M.VisitDate.Value.Month == AppTime.Now.Month && M.VisitDate.Value.Day == AppTime.Now.Day).Count(),
                SelfPatient = _context.Admissions.Where(M => M.IsDischarged == 0 && M.IsCancelled == 0 && M.PatientTypeId == 1).Count(),
                CompnayPatient = _context.Admissions.Where(M => M.IsDischarged == 0 && M.IsCancelled == 0 && M.PatientTypeId > 1).Count(),
                TodayAdmittedPatient = _context.Admissions.Where(M => M.AdmissionDate.Value.Year == AppTime.Now.Year && M.AdmissionDate.Value.Month == AppTime.Now.Month && M.AdmissionDate.Value.Day == AppTime.Now.Day && M.IsCancelled == 0).Count(),
                TodayDischargePatient = _context.Discharges.Where(M => M.DischargeDate.Value.Year == AppTime.Now.Year && M.DischargeDate.Value.Month == AppTime.Now.Month && M.DischargeDate.Value.Day == AppTime.Now.Day && M.IsCancelled == 0).Count(),
                TodaySelfPatient = _context.Admissions.Where(M => M.AdmissionDate.Value.Year == AppTime.Now.Year && M.AdmissionDate.Value.Month == AppTime.Now.Month && M.AdmissionDate.Value.Day == AppTime.Now.Day && M.IsCancelled == 0 && M.PatientTypeId == 1).Count(),
                TodayOtherPatient = _context.Admissions.Where(M => M.AdmissionDate.Value.Year == AppTime.Now.Year && M.AdmissionDate.Value.Month == AppTime.Now.Month && M.AdmissionDate.Value.Day == AppTime.Now.Day && M.IsCancelled == 0 && M.PatientTypeId > 1).Count()
            };
            res.TotalAdmittedPatientCount = res.AppointmentCount + res.SelfPatient + res.CompnayPatient + res.TodayAdmittedPatient + res.TodayDischargePatient + res.TodaySelfPatient + res.TodayOtherPatient;
            return res;
        }

        public OPVisitCountList GetOPVisitCount(OPVisitCountRequestModel model)
        {
            OPVisitCountList res = new()
            {
                NewPatientCount = _context.VisitDetails.Where(M => M.VisitDate >= model.FromDate && M.VisitDate <= model.ToDate && M.PatientOldNew == 0).Count(),
                OldPatientCount = _context.VisitDetails.Where(M => M.VisitDate >= model.FromDate && M.VisitDate <= model.ToDate && M.PatientOldNew == 1).Count(),
                CompanyPatientCount = _context.VisitDetails.Where(M => M.VisitDate >= model.FromDate && M.VisitDate <= model.ToDate && M.CompanyId > 1).Count(),
                CrossConsultantPatCount = _context.VisitDetails.Where(M => M.VisitDate >= model.FromDate && M.VisitDate <= model.ToDate && M.CrossConsulFlag == 1).Count()
            };
            res.TotalVisitCount = res.NewPatientCount + res.OldPatientCount + res.CompanyPatientCount + res.CrossConsultantPatCount;
            return res;
        }

        public async Task<PathologyDashboard> GetPathologyDashboard(int UnitId, DateTime FromDate, DateTime ToDate)
        {
            DatabaseHelper sql = new();
            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@UnitId", SqlDbType.Int) { Value = UnitId };
            para[1] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
            para[2] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };

            var data = await sql.Get2ResultsFromSp<PathologyCountSummary, PathologyValume, DailyTestCount, PathologyReport, PathologyOrderedTest, PathologyWorkload>("ps_rtrv_PathologyDashBoard", para);
            return new PathologyDashboard()
            {
                CountSummary = data.Item1.FirstOrDefault() ?? new PathologyCountSummary(),
                PathologyValumes = data.Item2 ?? new List<PathologyValume>(),
                DailyTestCounts = data.Item3 ?? new List<DailyTestCount>(),
                RecentPathologyReports = data.Item4 ?? new List<PathologyReport>(),
                MostOrderedTests = data.Item5 ?? new List<PathologyOrderedTest>(),
                PathologyWorkloads = data.Item6 ?? new List<PathologyWorkload>()
            };
        }
        public async Task<FinancialDashboard> GetFinancialDashBoard(int UnitId, DateTime FromDate, DateTime ToDate)
        {
            DatabaseHelper sql = new();
            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@UnitId", SqlDbType.Int) { Value = UnitId };
            para[1] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
            para[2] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };

            var data = await sql.Get7ResultsFromSp<PatientcountWardWiseCountSummary, FinancialCount, FinancialTestCount, FinancialOPPayment, FinancialIPPayment, FinancialVisit, FinancialCollectionPayMode, FinancialAdvance>("ps_rtrv_FinancialDashBoard", para);
            return new FinancialDashboard()
            {
                CountSummary = data.Item1.FirstOrDefault() ?? new PatientcountWardWiseCountSummary(),
                FinancialCount = data.Item2 ?? new List<FinancialCount>(),
                FinancialTestCount = data.Item3 ?? new List<FinancialTestCount>(),
                FinancialOPPayment = data.Item4 ?? new List<FinancialOPPayment>(),
                FinancialIPPayment = data.Item5 ?? new List<FinancialIPPayment>(),
                FinancialVisit = data.Item6 ?? new List<FinancialVisit>(),
                FinancialCollectionPayMode = data.Item7 ?? new List<FinancialCollectionPayMode>(),
                FinancialAdvance = data.Item8 ?? new List<FinancialAdvance>(),
                //FinancialPharmacyReturn = data.Item9 ?? new List<FinancialPharmacyReturn>(),
                //FinancialRefund = data.Item10 ?? new List<FinancialRefund>(),
                //FinancialDoctorWisePatientCountSummary = data.Item11 ?? new List<FinancialDoctorWisePatientCountSummary>(),
                //FinancialModeWiseCollection = data.Item12 ?? new List<FinancialModeWiseCollection>(),
                //FinancialOPExistingPatientCount = data.Item13 ?? new List<FinancialOPExistingPatientCount>(),
                //FinancialIPExistingPatientCount = data.Item14 ?? new List<FinancialIPExistingPatientCount>(),
                //FinancialOPDPatientSale = data.Item15 ?? new List<FinancialOPDPatientSale>(),
                //FinancialIPDPatientSale = data.Item16 ?? new List<FinancialIPDPatientSale>(),
                //FinancialAdvanceBalance = data.Item17 ?? new List<FinancialAdvanceBalance>(),
                //FinancialOutStandingOPIP = data.Item18 ?? new List<FinancialOutStandingOPIP>(),
                //FinancialInsuranceCaverageAdequacy = data.Item19 ?? new List<FinancialInsuranceCaverageAdequacy>()


            };
        }

    }
}
