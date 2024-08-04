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

namespace HIMS.Services.Inventory
{
    public class PathologyTemplateService : IPathologyTemplateService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public PathologyTemplateService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
            public virtual async Task InsertAsyncSP(MTemplateMaster objTemplate, int UserId, string Username)
              {
            try
            {
                // //Add header table records
                DatabaseHelper odal = new();


                string[] rEntity = { "TemplateId", "UpdatedBy" };
                var entity = objTemplate.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                string TemplateNo = odal.ExecuteNonQuery("M_Insert_M_PathologyTemplateMaster", CommandType.StoredProcedure, null, entity);
                objTemplate.TemplateId = Convert.ToInt32(TemplateNo);

               
                await _context.SaveChangesAsync(UserId, Username);
            }
            catch (Exception ex)
            {
                //// Delete header table realted records
                //MTemplateMaster objTemp = await _context.MTemplateMaster.FindAsync(objTemp.TemplateId);
                //if (objTemp != null)
                //{
                //    _context.MTemplateMaster.Remove(objTemp);
                //}

                
                //await _context.SaveChangesAsync();
            }
        }

    }
}
