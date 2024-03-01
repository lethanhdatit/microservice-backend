using Microsoft.EntityFrameworkCore;
using stepmedia_demo.EntityModels;
using stepmedia_demo.Repositories;
using stepmedia_demo.Services;
using stepmedia_demo.UnitOfWork;
using System.Linq.Expressions;
using System.Web.Helpers;

namespace stepmedia_demo.Services
{
    public class ProductService : BaseService<Product, ProductDto>, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override IGenericRepository<Product> _reponsitory => _unitOfWork.ProductRepository;

        public async Task<Product> CreateNewAsync(ProductCreation input)
        {
            var newEntity = new Product()
            {
                ShopId = input.ShopId,
                Name = input.Name,
                UnitPrice = input.UnitPrice,
                CreatedDate = DateTime.UtcNow
            };

            return await CreateAsync(newEntity);
        }

        public async Task<PaginationResult<ProductDto>> GetListAsync(long? shopId,
                                           string? orderBy,
                                           SortDirection? orderDirection,
                                           int? page,
                                           int? pageSize,
                                           Expression<Func<Product, ProductDto>> mapping,
                                           string? search = null)
        {
            if (mapping == null)
                throw new ArgumentNullException(nameof(mapping));

            var needSearch = !string.IsNullOrWhiteSpace(search);

            Expression<Func<Product, bool>> filter = s => (shopId != null ? s.ShopId == shopId.Value : true)
                                                       && (needSearch ? s.Name.Contains(search) : true);
            
            var entities = _reponsitory.Find(filter, orderBy!, orderDirection, string.Empty, page, pageSize);

            return new PaginationResult<ProductDto>()
            {
                TotalCount = entities.TotalCount,
                FilteredCount = entities.FilteredCount,
                PagedData = await entities.PagedData.Select(mapping).ToListAsync(),
            };
        }
    }
}
