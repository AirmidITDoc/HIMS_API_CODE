using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.DocumentManagement;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using System.Linq.Expressions;

namespace HIMS.Services.DocumentManagement
{
    public interface IDocumentUploadService
    {
        Task<List<RegistrationAutoCompleteDto>> SearchRegistration(string str);
        Task<IPagedList<DocumentFile>> GetAllPagedAsync(GridRequestModel objGrid, IQueryable<DocumentFile> query = null, Func<IQueryable<DocumentFile>, IQueryable<DocumentFile>> func = null);
        Task<DocumentFile?> GetById(Expression<Func<DocumentFile, bool>> predicateToGetId, params string[] includes);
        Task<DocumentFile> Add(DocumentFile entity, int UserId, string Username, params Expression<Func<DocumentFile, object>>[] references);
        Task<DocumentFile> Update(DocumentFile entity, int UserId, string Username, string[]? ignoreColumns = null);
        Task<bool> SoftDelete(DocumentFile entity, int UserId, string Username);
    }
}
