using stepmedia_demo.EntityModels;
using stepmedia_demo.Repositories;
using stepmedia_demo.Services;
using stepmedia_demo.UnitOfWork;

namespace stepmedia_demo.Services
{
    public class ShopService : BaseService<Shop, ShopDto>, IShopService
    {
        public ShopService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override IGenericRepository<Shop> _reponsitory => _unitOfWork.ShopRepository;

        public async Task<Shop> CreateNewAsync(ShopCreation input)
        {
            var newEntity = new Shop()
            {
                Name = input.Name,
                Location = input.Location,
                CreatedDate = DateTime.UtcNow
            };

            return await CreateAsync(newEntity);
        }
    }
}
