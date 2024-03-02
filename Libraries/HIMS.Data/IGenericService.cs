using HIMS.Core.Domain.Grid;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data
{
    public interface IGenericService<TModel>
    {
        Task<IPagedList<TModel>> GetAllPagedAsync(GridRequestModel objGrid, IQueryable<TModel> query = null, Func<IQueryable<TModel>, IQueryable<TModel>> func = null);
        Task<IEnumerable<TModel>> GetAll(Expression<Func<TModel, bool>>? where = null, params string[] includes);
        Task<TModel?> GetById(Expression<Func<TModel, bool>> predicateToGetId, params string[] includes);
        Task<TModel> Add(TModel dto, int UserId, string Username, params Expression<Func<TModel, object>>[] references);

        Task<TModel> Update(TModel dto, int UserId, string Username, params Expression<Func<TModel, object>>[] references);

        Task<bool> HardDelete(int id, int UserId, string Username, Expression<Func<TModel, bool>>? where = null);
        Task<bool> SoftDelete(int id, int UserId, string Username, Expression<Func<TModel, bool>>? where = null, string DelFlagColName = "IsActive");
    }

}
