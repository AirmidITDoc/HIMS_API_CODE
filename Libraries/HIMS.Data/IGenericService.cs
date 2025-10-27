using HIMS.Core.Domain.Grid;
using System.Linq.Expressions;

namespace HIMS.Data
{
    public interface IGenericService<TModel>
    {
        Task<IPagedList<TModel>> GetAllPagedAsync(GridRequestModel objGrid, IQueryable<TModel> query = null, Func<IQueryable<TModel>, IQueryable<TModel>> func = null);
        Task<IEnumerable<TModel>> GetAll(Expression<Func<TModel, bool>>? where = null, params string[] includes);
        Task<TModel?> GetById(Expression<Func<TModel, bool>> predicateToGetId, params string[] includes);
        Task<TModel> Add(TModel dto, int UserId, string Username, params Expression<Func<TModel, object>>[] references);
        Task<List<TModel>> Add(List<TModel> entities, int UserId, string Username);
        Task<TModel> Update(TModel dto, int UserId, string Username, string[]? references);

        Task<bool> HardDelete(int id, int UserId, string Username, Expression<Func<TModel, bool>>? where = null);
        Task<bool> SoftDelete(TModel dto, int UserId, string Username);
        Task<bool> HardDeleteBulk(Expression<Func<TModel, bool>>? where, int UserId, string Username);
    }

}
