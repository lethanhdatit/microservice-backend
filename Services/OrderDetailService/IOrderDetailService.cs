using stepmedia_demo.EntityModels;

namespace stepmedia_demo.Services
{
    public interface IOrderDetailService : IBaseService<OrderDetail, OrderDetailDto>
    {
        Task<OrderDetail> CreateNewAsync(OrderDetailCreation input);
    }
}
