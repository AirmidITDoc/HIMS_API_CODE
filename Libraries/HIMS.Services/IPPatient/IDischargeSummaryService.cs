using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Models;

namespace HIMS.Services.IPPatient
{
    public partial interface IDischargeSummaryService
    {
        Task InsertAsync(DischargeSummary OBJDischargeSummary, int UserId, string Username);
        Task UpdateAsync(DischargeSummary OBJDischargeSummary, int UserId, string Username);
    }
}
