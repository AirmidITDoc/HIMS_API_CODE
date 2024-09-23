using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Data.DataProviders;
using System.Data;
using System.Transactions;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Cells.Drawing;

namespace HIMS.Services.OutPatient
{
    public class IPLabRequestService : IIPLabRequestService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public IPLabRequestService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsyncSP(TPathologyReportHeader objTHlabRequest, int currentUserId, string currentUserName)
        {

            // OLD CODE With SP
            DatabaseHelper odal = new();
            string[] rEntity = { "RegNo", "UpdatedBy", "RegPrefix", "AnnualIncome", "IsIndientOrWeaker", "RationCardNo", "IsMember" };
            var entity = objTHlabRequest.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
          odal.ExecuteNonQuery("m_insert_Registration_1", CommandType.StoredProcedure, entity);
            
        }
    }
}