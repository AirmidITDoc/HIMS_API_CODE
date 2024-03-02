using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Extensions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Net;
using HIMS.Core.Domain.Logging;
using Newtonsoft.Json;

namespace HIMS.Data.Models
{
    public partial class SSDB_JSS_WEB_25SepContext : DbContext, IContext
    {
        public SSDB_JSS_WEB_25SepContext(DbContextOptions<SSDB_JSS_WEB_25SepContext> options)
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }
        public virtual async Task<int> SaveChangesAsync(int UserId, string Username, CancellationToken cancellationToken = default)
        {
            if (UserId > 0)
                await AuditChanges(UserId, Username);
            return await base.SaveChangesAsync(cancellationToken);
        }
        private async Task AuditChanges(int UserId, string Username)
        {
            DateTime now = DateTime.Now;

            var entityEntries = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added ||
                            x.State == EntityState.Modified ||
                            x.State == EntityState.Deleted).ToList();

            foreach (EntityEntry entityEntry in entityEntries)
            {
                IncrementVersionNumber(entityEntry);
                await CreateAuditAsync(entityEntry, UserId, Username);
            }
        }
        private void IncrementVersionNumber(EntityEntry entityEntry)
        {
            if (entityEntry.Metadata.FindProperty("Version") != null)
            {
                var currentVersionNumber = Convert.ToInt32(entityEntry.CurrentValues["Version"]);
                entityEntry.CurrentValues["Version"] = currentVersionNumber + 1;
            }
        }

        private async Task CreateAuditAsync(EntityEntry entityEntry, int UserId, string Username)
        {
            AuditLog objAdd = new()
            {
                CreatedOn = DateTime.Now,
                ActionId = (int)LogAction.Add,
                ActionById = UserId,
                ActionByName = Username,
                Id = 0,
                LogSourceId = (int)LogSource.API,
                LogTypeId = (int)LogType.Audit,
                AdditionalInfo = "Audit",
                EntityId = entityEntry.Properties.Single(p => p.Metadata.IsPrimaryKey()).CurrentValue.ToInt(),
                EntityName = entityEntry.Entity.GetType().Name,
                Description = JsonConvert.SerializeObject(entityEntry.Properties.Select(p => new { p.Metadata.Name, p.CurrentValue }).ToDictionary(i => i.Name, i => i.CurrentValue))
            };
            await AuditLogs.AddAsync(objAdd);
        }

    }
}
