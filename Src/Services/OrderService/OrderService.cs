using stepmedia_demo.EntityModels;
using stepmedia_demo.Repositories;
using stepmedia_demo.Services;
using stepmedia_demo.UnitOfWork;

namespace stepmedia_demo.Services
{
    public class OrderService : BaseService<Order, OrderDto>, IOrderService
    {
        public OrderService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override IGenericRepository<Order> _reponsitory => _unitOfWork.OrderRepository;

        public async Task<Order> CreateNewAsync(OrderCreation input)
        {
            var newEntity = new Order()
            {
                CustomerId = input.CustomerId,
                ShopId = input.ShopId,
                CreatedDate = DateTime.UtcNow,
                OrderDetails = input.Items.Select(s => new OrderDetail
                {
                    ProductId = s.ProductId,
                    UnitPrice = s.UnitPrice,
                    Quantity = s.Quantity,
                }).ToList(),
            };

            return await CreateAsync(newEntity);
        }
    }
}
