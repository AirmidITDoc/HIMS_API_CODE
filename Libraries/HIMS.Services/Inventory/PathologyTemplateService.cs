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
                DatabaseHelper odal = new();

                string[] rEntity = {"UpdatedBy" };
                var entity = objTemplate.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }

                string TemplateId = odal.ExecuteNonQuery("M_Insert_M_PathologyTemplateMaster", CommandType.StoredProcedure, "TemplateId", entity);
                objTemplate.TemplateId = Convert.ToInt32(TemplateId);

                await _context.SaveChangesAsync(UserId, Username);

        }

    }
}
