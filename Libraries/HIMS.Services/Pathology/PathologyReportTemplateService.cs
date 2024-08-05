using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Transactions;

namespace HIMS.Services.Pathology
{
    public class PathologyReportTemplateService : IPathologyReportTemplateService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public PathologyReportTemplateService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsyncSP(TPathologyReportTemplateDetails objpathology, int PathReportId, int PathTemplateId)
        {
            try
            {
                // //Add header table records
                DatabaseHelper odal = new();
                //SqlParameter[] para = new SqlParameter[7];
                //para[0] = new SqlParameter("@IndentDate", objIndent.IndentDate);
                //para[1] = new SqlParameter("@IndentTime", objIndent.IndentTime);
                //para[2] = new SqlParameter("@FromStoreId", objIndent.FromStoreId);
                //para[3] = new SqlParameter("@ToStoreId", objIndent.ToStoreId);
                //para[4] = new SqlParameter("@Addedby", objIndent.Addedby);
                //para[5] = new SqlParameter("@Comments", objIndent.Comments);
                //para[6] = new SqlParameter("@IndentId", SqlDbType.BigInt);
                //para[6].Direction = ParameterDirection.Output;
                //string indentNo = odal.ExecuteNonQuery("m_insert_IndentHeader_1", CommandType.StoredProcedure, "@IndentId", para);
                //objIndent.IndentId = Convert.ToInt32(indentNo);

                string[] rEntity = { "PathReportId", "PathTemplateId", "PathTemplateDetailsResult", "TemplateResultInHTML", "TestId" };
                var entity = objpathology.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                string PathReportId = odal.ExecuteNonQuery("m_insert_PathologyReportTemplateDetails_1", CommandType.StoredProcedure, "PathReportId", entity);
                objpathology.PathReportId = Convert.ToInt32(PathReportId);

                // Add details table records
                foreach (var objItem in objpathology.TPathologyReportTemplateDetails)
                {
                    objItem.PathReportId = objpathology.PathReportId;
                }
                _context.TIndentDetails.AddRange(objpathology.TPathologyReportTemplateDetails);
                await _context.SaveChangesAsync(UserId, Username);
            }
            catch (Exception ex)
            {
                // Delete header table realted records
                TIndentHeader objInd = await _context.TIndentHeaders.FindAsync(objIndent.IndentId);
                if (objInd != null)
                {
                    _context.TIndentHeaders.Remove(objInd);
                }

                // Delete details table realted records
                var lst = await _context.TIndentDetails.Where(x => x.IndentId == objIndent.IndentId).ToListAsync();
                if (lst.Count() > 0)
                {
                    _context.TIndentDetails.RemoveRange(lst);
                }
                await _context.SaveChangesAsync();
            }
        }

        public virtual async Task InsertAsync(TIndentHeader objIndent, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update store table records
                MStoreMaster StoreInfo = await _context.MStoreMasters.FirstOrDefaultAsync(x => x.StoreId == objIndent.FromStoreId);
                if (StoreInfo != null)
                {
                    StoreInfo.IndentNo = Convert.ToString(Convert.ToInt32(StoreInfo?.IndentNo ?? "0") + 1);
                    _context.MStoreMasters.Update(StoreInfo);
                    await _context.SaveChangesAsync();
                }

                // Add header & detail table records
                objIndent.IndentNo = (StoreInfo != null) ? StoreInfo.IndentNo : "0";
                _context.TIndentHeaders.Add(objIndent);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

    }
}
