using HIMS.Core.Domain.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Report
{
    public partial interface IReportService
    {
        string GetReportSetByProc(ReportRequestModel model);
        string GetNewReportSetByProc(ReportNewRequestModel model);
    }
}
