using stepmedia_demo.EntityModels;
using stepmedia_demo.Repositories;
using System.Linq.Expressions;
using System.Web.Helpers;

namespace stepmedia_demo.Services
{
    public interface IProductService : IBaseService<Product, ProductDto>
    {
        Task<Product> CreateNewAsync(ProductCreation input);
        Task<PaginationResult<ProductDto>> GetListAsync(long? shopId,
                                           string? orderBy,
                                           SortDirection? orderDirection,
                                           int? page,
                                           int? pageSize,
                                           Expression<Func<Product, ProductDto>> mapping,
                                           string? search = null);
    }
}
