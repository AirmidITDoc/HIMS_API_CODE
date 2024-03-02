using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.Extensions
{
    public interface IContext : IAsyncDisposable, IDisposable
    {
        public DatabaseFacade Database { get; }
        public DbSet<TEntity> Set<TEntity>() where TEntity : class;
        public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        public Task<int> SaveChangesAsync(int UserId, string Username, CancellationToken cancellationToken = default);
    }
}
