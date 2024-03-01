using stepmedia_demo.EntityModels;

namespace stepmedia_demo.Services
{
    public interface IOrderService : IBaseService<Order, OrderDto>
    {
        Task<Order> CreateNewAsync(OrderCreation input);
    }
}
