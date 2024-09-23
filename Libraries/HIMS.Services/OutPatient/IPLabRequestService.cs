using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.OutPatient
{
    public class IPLabRequestService : IIPLabRequestService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public IPLabRequestService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsyncSP(THlabRequest objTHlabRequest, int currentUserId, string currentUserName)
        {

            //// OLD CODE With SP
            //DatabaseHelper odal = new();
            //string[] rEntity = { "RegNo", "UpdatedBy", "RegPrefix", "AnnualIncome", "IsIndientOrWeaker", "RationCardNo", "IsMember" };
            //var entity = objTHlabRequest.ToDictionary();
            //foreach (var rProperty in rEntity)
            //{
            //    entity.Remove(rProperty);
            //}
            //string RegId = odal.ExecuteNonQuery("m_insert_Registration_1", CommandType.StoredProcedure, "RegId", entity);
            //objTHlabRequest.RegId = Convert.ToInt32(RegId);
           
        }
    }
}