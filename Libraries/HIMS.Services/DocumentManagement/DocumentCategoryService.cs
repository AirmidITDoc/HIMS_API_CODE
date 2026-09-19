using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.DocumentManagement;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HIMS.Services.DocumentManagement
{
    public class DocumentCategoryService : IDocumentCategoryService
    {
        private readonly HIMSDbContext _context;

        public DocumentCategoryService(HIMSDbContext context)
        {
            _context = context;
        }
        public virtual async Task<IPagedList<DocumentCategory>> GetAllPagedAsync(GridRequestModel objGrid, IQueryable<DocumentCategory> query = null, Func<IQueryable<DocumentCategory>, IQueryable<DocumentCategory>> func = null)
        {
            query ??= _context.DocumentCategories.AsQueryable();

            query = func != null ? func(query) : query;
            return await query.BuildPredicate(objGrid);
        }
        public async Task<DocumentCategory?> GetById(Expression<Func<DocumentCategory, bool>> predicateToGetId, params string[] includes)
        {
            var query = ApplyIncludes(_context.DocumentCategories, includes);
            return await query.FirstOrDefaultAsync(predicateToGetId);
        }
        public async Task<DocumentCategory> Add(DocumentCategory entity, int UserId, string Username, params Expression<Func<DocumentCategory, object>>[] references)
        {
            _context.DocumentCategories.Add(entity);
            await LoadReferences(entity, references);
            await _context.SaveChangesAsync(UserId, Username);

            return entity;
        }
        public async Task<DocumentCategory> Update(DocumentCategory entity, int UserId, string Username, string[]? ignoreColumns = null)
        {
            _context.Entry(entity).State = EntityState.Modified;
            if ((ignoreColumns?.Length ?? 0) > 0)
            {
                foreach (var column in ignoreColumns)
                {
                    _context.Entry(entity).Property(column).IsModified = false;
                }
            }
            await _context.SaveChangesAsync(UserId, Username);
            return entity;

        }
        public async Task<bool> SoftDelete(DocumentCategory entity, int UserId, string Username)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(UserId, Username, true);
            return true;
        }
        private async Task LoadReferences(DocumentCategory entity, IEnumerable<Expression<Func<DocumentCategory, object>>> references)
        {
            foreach (var reference in references)
            {
                await _context.Entry(entity).Reference(reference!).LoadAsync();
            }
        }
        private static IQueryable<DocumentCategory> ApplyIncludes(IQueryable<DocumentCategory> query, IEnumerable<string> includes)
        {
            return includes.Aggregate(query, (current, include) => current.Include(include));
        }
        public async Task<List<DocumentCategoryDto>> GetTreeAsync()
        {
            var list = await _context.DocumentCategories.Where(x => x.IsActive && !x.IsDeleted).OrderBy(x => x.SortOrder).Select(x => new DocumentCategoryDto
            {
                Id = x.Id,
                ParentId = x.ParentId,
                DocCategory = x.DocCategory,
                Icon = x.Icon,
                DocumentCount = 0
            }).ToListAsync();
            return BuildTree(list);
        }

        private static List<DocumentCategoryDto> BuildTree(List<DocumentCategoryDto> source)
        {
            var lookup = source.ToDictionary(x => x.Id);
            var roots = new List<DocumentCategoryDto>();
            foreach (var item in source)
            {
                if (item.ParentId == null)
                {
                    roots.Add(item);
                }
                else if (lookup.ContainsKey(item.ParentId.Value))
                {
                    lookup[item.ParentId.Value].Children.Add(item);
                }
            }
            return roots;
        }
    }
}
