using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.Inventory
{
    public class IssueToDepService : IIssueToDepService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public IssueToDepService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsyncSP(TIssueToDepartmentHeader objIssueToDepartment, int UserId, string Username)
        {
            try
            {
                // //Add header table records
                DatabaseHelper odal = new();


                string[] rEntity = { "IssueNo", "Receivedby", "Updatedby", "IsAccepted", "AcceptedBy", "AcceptedDatetime", "TIssueToDepartmentDetails" };
                var entity = objIssueToDepartment.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                string IssueId = odal.ExecuteNonQuery("v_Insert_IssueToDepartmentHeader_1_New", CommandType.StoredProcedure, "IssueId", entity);
                objIssueToDepartment.IssueId = Convert.ToInt32(IssueId);

                // Add details table records
                foreach (var objissue in objIssueToDepartment.TIssueToDepartmentDetails)
                {
                    objissue.IssueId = objIssueToDepartment.IssueId;
                }
                _context.TIssueToDepartmentDetails.AddRange(objIssueToDepartment.TIssueToDepartmentDetails);
                await _context.SaveChangesAsync(UserId, Username);

                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("v_upd_T_Curstk_issdpt_1", CommandType.StoredProcedure,  entity);
                  }
            catch (Exception)
            {
                // Delete header table realted records
                TIssueToDepartmentHeader? obissue = await _context.TIssueToDepartmentHeaders.FindAsync(objIssueToDepartment.IndentId);
                if (obissue != null)
                {
                    _context.TIssueToDepartmentHeaders.Remove(obissue);
                }

                // Delete details table realted records
                var lst = await _context.TIssueToDepartmentDetails.Where(x => x.IssueId == objIssueToDepartment.IssueId).ToListAsync();
                if (lst.Count > 0)
                {
                    _context.TIssueToDepartmentDetails.RemoveRange(lst);
                }
                await _context.SaveChangesAsync();
            }

        }
        
    }
}



