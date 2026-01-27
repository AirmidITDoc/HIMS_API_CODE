using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Masters;
using LinqToDB;
using Microsoft.Data.SqlClient;

namespace HIMS.Services.Audit
{
    public class AuditService : IAuditService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public AuditService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<AuditLog>> GetAllPagedAsync(GridRequestModel objGrid)
        {
            List<string> Entities = new();
            List<string> EntityIds = new();
            var EntityFilter = objGrid.Filters?.FirstOrDefault(x => x.FieldName == "EntityName");
            var EntityIdFilter = objGrid.Filters?.FirstOrDefault(x => x.FieldName == "EntityId");
            if (EntityFilter != null && !string.IsNullOrEmpty(EntityFilter.FieldValue))
            {
                objGrid.Filters.Remove(EntityFilter);
                Entities = EntityFilter.FieldValue.Split(',').ToList();
            }
            if (EntityIdFilter != null && !string.IsNullOrEmpty(EntityIdFilter.FieldValue))
            {
                objGrid.Filters.Remove(EntityIdFilter);
                EntityIds = EntityIdFilter.FieldValue.Split(',').ToList();
            }
            var qry = from p in _context.AuditLogs
                      where (EntityFilter == null || string.IsNullOrEmpty(EntityFilter.FieldValue) || Entities.Contains(p.EntityName))
                      && (EntityIdFilter == null || string.IsNullOrEmpty(EntityIdFilter.FieldValue) || EntityIds.Contains(p.EntityId.ToString()))
                      select new AuditLog()
                      {
                          ActionById = p.ActionId,
                          Id = p.Id,
                          ActionByName = p.ActionByName,
                          ActionId = p.ActionId,
                          CreatedOn = p.CreatedOn,
                          AdditionalInfo = p.AdditionalInfo,
                          Description = p.Description,
                          EntityId = p.EntityId,
                          EntityName = p.EntityName,
                          LogSourceId = p.LogSourceId,
                          LogTypeId = p.LogTypeId
                      };
            return await qry.BuildPredicate(objGrid);
        }
    }
}
