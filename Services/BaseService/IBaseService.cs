using stepmedia_demo.Repositories;
using System.Linq.Expressions;
using System.Web.Helpers;

namespace stepmedia_demo.Services
{
    public interface IBaseService<TEntity, DtoEntity>
        where TEntity : class
    {
        Task<TEntity> CreateAsync(TEntity dto);

        Task<TEntity> UpdateAsync(TEntity dto);

        Task DeleteAsync(object keyValues);

        Task<TEntity> FindByIdAsync(object keyValues);

        TEntity FindById(object keyValues);

        Task<PaginationResult<DtoEntity>> GetListAsync(string? orderBy,
                                            SortDirection? orderDirection,
                                            int? page,
                                            int? pageSize,
                                            Expression<Func<TEntity, DtoEntity>> mapping);
    }
}
