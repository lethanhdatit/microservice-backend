using System.Linq.Expressions;
using System.Web.Helpers;

namespace stepmedia_demo.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        PaginationQuery<TEntity> Find(Expression<Func<TEntity, bool>> filter = null!,
                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null!,
                                                string includeProperties = "",
                                                int? page = null,
                                                int? pageSize = null);

        PaginationQuery<TEntity> Find(Expression<Func<TEntity, bool>> filter = null!,
                                                string orderBy = null!,
                                                SortDirection? orderDirection = SortDirection.Ascending,
                                                string includeProperties = "",
                                                int? page = null,
                                                int? pageSize = null);
        Task<TEntity> FindByIdAsync(object id);
        TEntity FindById(object id);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
    }
}
