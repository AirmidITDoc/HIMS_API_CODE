using HIMS.Core.Domain.Grid;
using HIMS.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HIMS.Data
{

    public class GenericService<TModel> : IGenericService<TModel> where TModel : class
    {
        private readonly IContext _dbContext;

        public GenericService(IContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<IPagedList<TModel>> GetAllPagedAsync(GridRequestModel objGrid, IQueryable<TModel> query = null, Func<IQueryable<TModel>, IQueryable<TModel>> func = null)
        {
            query ??= _dbContext.Set<TModel>().AsQueryable();

            query = func != null ? func(query) : query;
            return await query.BuildPredicate(objGrid);
        }
        public async Task<IEnumerable<TModel>> GetAll(Expression<Func<TModel, bool>>? where = null,
            params string[] includes)
        {
            var query = GenericService<TModel>.ApplyIncludes(_dbContext.Set<TModel>(), includes);

            if (where != null)
            {
                query = query.Where(where);
            }

            return await query.ToListAsync();
        }

        public async Task<TModel?> GetById(Expression<Func<TModel, bool>> predicateToGetId,
            params string[] includes)
        {
            var query = GenericService<TModel>.ApplyIncludes(_dbContext.Set<TModel>(), includes);

            return await query.FirstOrDefaultAsync(predicateToGetId);
        }


        public async Task<TModel> Add(TModel entity, int UserId, string Username, params Expression<Func<TModel, object>>[] references)
        {
            _dbContext.Set<TModel>().Add(entity);

            await LoadReferences(entity, references);
            await _dbContext.SaveChangesAsync(UserId, Username);

            return entity;
        }
        public async Task<List<TModel>> Add(List<TModel> entities, int UserId, string Username)
        {
            _dbContext.Set<TModel>().AddRange(entities);

            await _dbContext.SaveChangesAsync(UserId, Username);

            return entities;
        }

        public async Task<TModel> Update(TModel entity, int UserId, string Username, string[]? ignoreColumns = null)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            if ((ignoreColumns?.Length ?? 0) > 0)
            {
                foreach (var column in ignoreColumns)
                {
                    _dbContext.Entry(entity).Property(column).IsModified = false;
                }
            }
            await _dbContext.SaveChangesAsync(UserId, Username);
            return entity;

        }

        public async Task<bool> HardDelete(int id, int UserId, string Username, Expression<Func<TModel, bool>>? where = null)
        {
            var query = _dbContext.Set<TModel>().AsQueryable();
            if (where != null)
            {
                query = query.Where(where);
            }
            var entity = await query.FirstOrDefaultAsync();
            if (entity == null)
            {
                return false;
            }

            _dbContext.Set<TModel>().Remove(entity);
            await _dbContext.SaveChangesAsync(UserId, Username);

            return true;
        }
        public async Task<bool> HardDeleteBulk(Expression<Func<TModel, bool>>? where, int UserId, string Username)
        {
            var query = _dbContext.Set<TModel>().AsQueryable();
            if (where != null)
            {
                query = query.Where(where);
            }
            var entities = await query.ToListAsync();
            if (entities.Count > 0)
            {
                _dbContext.Set<TModel>().RemoveRange(entities);
                await _dbContext.SaveChangesAsync(UserId, Username);
            }

            return true;
        }
        public async Task<bool> SoftDelete(TModel entity, int UserId, string Username)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(UserId, Username, true);
            return true;
        }

        private static IQueryable<TModel> ApplyIncludes(IQueryable<TModel> query, IEnumerable<string> includes)
        {
            return includes.Aggregate(query, (current, include) => current.Include(include));
        }

        private async Task LoadReferences(TModel entity, IEnumerable<Expression<Func<TModel, object>>> references)
        {
            foreach (var reference in references)
            {
                await _dbContext.Entry(entity).Reference(reference!).LoadAsync();
            }
        }
    }
}
