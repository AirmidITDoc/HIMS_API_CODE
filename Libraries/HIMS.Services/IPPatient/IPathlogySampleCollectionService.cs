using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.IPPatient
{
    public partial interface IPathlogySampleCollectionService
    {
        Task UpdateAsyncSP(TPathologyReportHeader objTPathologyReportHeader, int UserId, string Username);
    }
}
