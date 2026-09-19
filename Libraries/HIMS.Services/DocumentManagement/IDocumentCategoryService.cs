using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.DocumentManagement;
using HIMS.Data.Models;
using System.Linq.Expressions;

namespace HIMS.Services.DocumentManagement
{
    public interface IDocumentCategoryService
    {
        Task<IPagedList<DocumentCategory>> GetAllPagedAsync(GridRequestModel objGrid, IQueryable<DocumentCategory> query = null, Func<IQueryable<DocumentCategory>, IQueryable<DocumentCategory>> func = null);
        Task<DocumentCategory?> GetById(Expression<Func<DocumentCategory, bool>> predicateToGetId, params string[] includes);
        Task<DocumentCategory> Add(DocumentCategory entity, int UserId, string Username, params Expression<Func<DocumentCategory, object>>[] references);
        Task<DocumentCategory> Update(DocumentCategory entity, int UserId, string Username, string[]? ignoreColumns = null);
        Task<bool> SoftDelete(DocumentCategory entity, int UserId, string Username);
        Task<List<DocumentCategoryDto>> GetTreeAsync();
    }
}
