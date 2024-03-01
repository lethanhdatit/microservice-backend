using stepmedia_demo.EntityModels;

namespace stepmedia_demo.Services
{
    public interface IShopService : IBaseService<Shop, ShopDto>
    {
        Task<Shop> CreateNewAsync(ShopCreation input);
    }
}
