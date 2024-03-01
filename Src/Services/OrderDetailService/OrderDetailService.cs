using stepmedia_demo.EntityModels;
using stepmedia_demo.Repositories;
using stepmedia_demo.Services;
using stepmedia_demo.UnitOfWork;

namespace stepmedia_demo.Services
{
    public class OrderDetailService : BaseService<OrderDetail, OrderDetailDto>, IOrderDetailService
    {
        public OrderDetailService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override IGenericRepository<OrderDetail> _reponsitory => _unitOfWork.OrderDetailRepository;

        public async Task<OrderDetail> CreateNewAsync(OrderDetailCreation input)
        {
            var newEntity = new OrderDetail()
            {
                OrderId = input.OrderId,
                ProductId = input.ProductId,
                UnitPrice = input.UnitPrice,
                Quantity = input.Quantity,
            };

            return await CreateAsync(newEntity);
        }
    }
}
