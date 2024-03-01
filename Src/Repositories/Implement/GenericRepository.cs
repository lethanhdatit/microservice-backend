using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using stepmedia_demo.EntityModels;
using stepmedia_demo.Extensions;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Helpers;

namespace stepmedia_demo.Repositories
{
    public class PaginationBase
    {
        public long TotalCount { get; set; }
        public long FilteredCount { get; set; }
    }

    public class PaginationQuery<TEntity> : PaginationBase
    {
        public IQueryable<TEntity> PagedData { get; set; }
    }

    public class PaginationResult<TEntity> : PaginationBase
    {
        public List<TEntity> PagedData { get; set; }
    }

    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal StepmediaDemoContext _context;
        internal DbSet<TEntity> _dbSet;

        public GenericRepository(StepmediaDemoContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual PaginationQuery<TEntity> Find(Expression<Func<TEntity, bool>> filter = null!,
                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null!, 
                                                string includeProperties = "", 
                                                int? page = null,
                                                int? pageSize = null)
        {
            IQueryable<TEntity> query = _dbSet;

            long totalCount = query.LongCount();
            long filteredCount = totalCount;

            if (filter != null)
            {
                query = query.Where(filter);
                filteredCount = query.LongCount();
            }

            if (includeProperties != null)
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return new PaginationQuery<TEntity>
            {
                TotalCount = totalCount,
                FilteredCount = filteredCount,
                PagedData = query,
            };
        }

        public virtual PaginationQuery<TEntity> Find(Expression<Func<TEntity, bool>> filter = null!,
                                                string orderBy = null!,
                                                SortDirection? orderDirection = SortDirection.Ascending,
                                                string includeProperties = "",
                                                int? page = null,
                                                int? pageSize = null)
        {
            IQueryable<TEntity> query = _dbSet;

            long totalCount = query.LongCount();
            long filteredCount = totalCount;

            if (filter != null)
            {
                query = query.Where(filter);
                filteredCount = query.LongCount();
            }

            if (includeProperties != null)
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                var propertyInfo = typeof(TEntity).GetProperty(orderBy, BindingFlags.IgnoreCase 
                                                                      | BindingFlags.Public 
                                                                      | BindingFlags.Instance);
                if(propertyInfo != null)
                {
                    orderDirection = orderDirection ?? SortDirection.Ascending;
                    switch (orderDirection)
                    {
                        case SortDirection.Ascending:
                            query = query.OrderBy(propertyInfo.Name);
                            break;
                        case SortDirection.Descending:
                            query = query.OrderByDescending(propertyInfo.Name);
                            break;
                    }
                }
            }

            if (page != null && pageSize != null)
            {
                if(page.Value <= 0)
                    throw new ArgumentException("'page' must be greater than 0");

                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return new PaginationQuery<TEntity>
            {
                TotalCount = totalCount,
                FilteredCount = filteredCount,
                PagedData = query,
            };
        }

        public virtual async Task<TEntity> FindByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }
        public virtual TEntity FindById(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }

            _dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
