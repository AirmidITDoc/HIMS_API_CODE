using Aspose.Cells;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Common;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                          where M.VisitDate.Value.Year == DateTime.Now.Year && M.VisitDate.Value.Month == DateTime.Now.Month && M.VisitDate.Value.Day == DateTime.Now.Day
                          select new DailyDashboardSummaryModel()
                          {
                              Title = "OPD",
                              Label = "Todays",
                              Count = _context.VisitDetails.Count(),
                              SelfLbl = "Self",
                              SelfCnt = _context.VisitDetails.Where(sub => sub.CompanyId == 0 && (sub.VisitDate.Value.Year == DateTime.Now.Year && sub.VisitDate.Value.Month == DateTime.Now.Month && sub.VisitDate.Value.Day == DateTime.Now.Day)).Count(),
                              CompanyLbl = "Company",
                              CompayCnt = _context.VisitDetails.Where(sub => sub.CompanyId != 0 && (sub.VisitDate.Value.Year == DateTime.Now.Year && sub.VisitDate.Value.Month == DateTime.Now.Month && sub.VisitDate.Value.Day == DateTime.Now.Day)).Count()
                          });

            var query2 = (from M in _context.Admissions
                          where M.IsDischarged == 0 && M.IsCancelled == 0
                          select new DailyDashboardSummaryModel()
                          {
                              Title = "IPD",
                              Label = "Currently Admitted",
                              Count = _context.Admissions.Count(),
                              SelfLbl = "Admission",
                              SelfCnt = _context.Admissions.Where(sub => sub.IsCancelled == 0 && (sub.AdmissionDate.Value.Year == DateTime.Now.Year && sub.AdmissionDate.Value.Month == DateTime.Now.Month && sub.AdmissionDate.Value.Day == DateTime.Now.Day)).Count(),
                              CompanyLbl = "Discharge",
                              CompayCnt = _context.Admissions.Where(sub => sub.IsCancelled != 0 && (sub.AdmissionDate.Value.Year == DateTime.Now.Year && sub.AdmissionDate.Value.Month == DateTime.Now.Month && sub.AdmissionDate.Value.Day == DateTime.Now.Day)).Count()
                          });


            var query3 = (from M in _context.TSalesHeaders
                          where M.Date.Value.Year == DateTime.Now.Year && M.Date.Value.Month == DateTime.Now.Month && M.Date.Value.Day == DateTime.Now.Day
                          select new DailyDashboardSummaryModel()
                          {
                              Title = "Pharmacy",
                              Label = "Pharmacy",
                              Count = _context.TSalesHeaders.Count(),
                              SelfLbl = "OP Patient",
                              SelfCnt = _context.TSalesHeaders.Where(sub => (sub.OpIpType == 0 || sub.OpIpType == 2) && (sub.Date.Value.Year == DateTime.Now.Year && sub.Date.Value.Month == DateTime.Now.Month && sub.Date.Value.Day == DateTime.Now.Day)).Count(),
                              CompanyLbl = "IP Patient",
                              CompayCnt = _context.TSalesHeaders.Where(sub => sub.OpIpType == 1 && (sub.Date.Value.Year == DateTime.Now.Year && sub.Date.Value.Month == DateTime.Now.Month && sub.Date.Value.Day == DateTime.Now.Day)).Count(),
                          });

            var cnt = (from M in _context.VisitDetails
                       join F in _context.MDepartmentMasters on M.DepartmentId equals F.DepartmentId
                       where M.VisitDate.Value.Year == DateTime.Now.Year && M.VisitDate.Value.Month == DateTime.Now.Month && M.VisitDate.Value.Day == DateTime.Now.Day //&& F.DepartmentId == 7
                       select M.VisitId).Count();

            var query4 = (from M in _context.VisitDetails
                          join F in _context.MDepartmentMasters on M.DepartmentId equals F.DepartmentId
                          where M.VisitDate.Value.Year == DateTime.Now.Year && M.VisitDate.Value.Month == DateTime.Now.Month && M.VisitDate.Value.Day == DateTime.Now.Day //&& F.DepartmentId == 7
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
            int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            var firstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var lastDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, daysInMonth);

            DateTime StartDate = firstDay.AddMonths(-1);
            DateTime EndDate = lastDay.AddMonths(-1);
            DateTime TodaysDate = DateTime.Now;

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
            else if (model.DateRange == "Last Weeks")
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
            IPAdemissionDischargeCountModel res = new IPAdemissionDischargeCountModel();
            res.AppointmentCount = _context.VisitDetails.Where(M => M.VisitDate.Value.Year == DateTime.Now.Year && M.VisitDate.Value.Month == DateTime.Now.Month && M.VisitDate.Value.Day == DateTime.Now.Day).Count();
            res.SelfPatient = _context.Admissions.Where(M => M.IsDischarged == 0 && M.IsCancelled == 0 && M.PatientTypeId == 1).Count();
            res.CompnayPatient = _context.Admissions.Where(M => M.IsDischarged == 0 && M.IsCancelled == 0 && M.PatientTypeId > 1).Count();
            res.TodayAdmittedPatient = _context.Admissions.Where(M => M.AdmissionDate.Value.Year == DateTime.Now.Year && M.AdmissionDate.Value.Month == DateTime.Now.Month && M.AdmissionDate.Value.Day == DateTime.Now.Day && M.IsCancelled == 0).Count();
            res.TodayDischargePatient = _context.Discharges.Where(M => M.DischargeDate.Value.Year == DateTime.Now.Year && M.DischargeDate.Value.Month == DateTime.Now.Month && M.DischargeDate.Value.Day == DateTime.Now.Day && M.IsCancelled == 0).Count();
            res.TodaySelfPatient = _context.Admissions.Where(M => M.AdmissionDate.Value.Year == DateTime.Now.Year && M.AdmissionDate.Value.Month == DateTime.Now.Month && M.AdmissionDate.Value.Day == DateTime.Now.Day && M.IsCancelled == 0 && M.PatientTypeId == 1).Count();
            res.TodayOtherPatient = _context.Admissions.Where(M => M.AdmissionDate.Value.Year == DateTime.Now.Year && M.AdmissionDate.Value.Month == DateTime.Now.Month && M.AdmissionDate.Value.Day == DateTime.Now.Day && M.IsCancelled == 0 && M.PatientTypeId > 1).Count();
            res.TotalAdmittedPatientCount = res.AppointmentCount + res.SelfPatient + res.CompnayPatient + res.TodayAdmittedPatient + res.TodayDischargePatient + res.TodaySelfPatient + res.TodayOtherPatient;
            return res;
        }

        public OPVisitCountList GetOPVisitCount(OPVisitCountRequestModel model)
        {
            OPVisitCountList res = new OPVisitCountList();
            res.NewPatientCount = _context.VisitDetails.Where(M => M.VisitDate >= model.FromDate && M.VisitDate <= model.ToDate && M.PatientOldNew == 0).Count(); 
            res.OldPatientCount = _context.VisitDetails.Where(M => M.VisitDate >= model.FromDate && M.VisitDate <= model.ToDate && M.PatientOldNew == 1).Count();
            res.CompanyPatientCount = _context.VisitDetails.Where(M => M.VisitDate >= model.FromDate && M.VisitDate <= model.ToDate && M.CompanyId > 1).Count();
            res.CrossConsultantPatCount = _context.VisitDetails.Where(M => M.VisitDate >= model.FromDate && M.VisitDate <= model.ToDate && M.CrossConsulFlag == 1).Count();
            res.TotalVisitCount = res.NewPatientCount + res.OldPatientCount + res.CompanyPatientCount + res.CrossConsultantPatCount;
            return res;
        }

    }
}
