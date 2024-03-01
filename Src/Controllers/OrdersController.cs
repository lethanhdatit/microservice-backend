using Microsoft.AspNetCore.Mvc;
using stepmedia_demo.EntityModels;
using stepmedia_demo.Repositories;
using stepmedia_demo.Services;
using System.Web.Helpers;

namespace stepmedia_demo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IOrderService _orderService;

        public OrdersController(ILogger<OrdersController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<PaginationResult<OrderDto>> GetList(string? orderBy,
                                                     SortDirection? orderDirection,
                                                     int? page,
                                                     int? pageSize)
        {
            return await _orderService.GetListAsync(orderBy, orderDirection, page, pageSize, s => new OrderDto
            {
                Id = s.Id,
                ShopId = s.ShopId,
                CustomerId = s.CustomerId,
                CreatedDate = s.CreatedDate,
                CustomerName = s.Customer.FullName!,
                ShopName = s.Shop.Name,
                Items = s.OrderDetails.Select(x => new OrderDetailDto
                {
                    Id = x.Id,
                    OrderId = x.OrderId,
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    UnitPrice = x.UnitPrice,
                    Quantity = x.Quantity,
                }).ToList()
            });
        }

        [HttpPost]
        public async Task<OrderResponse> Create(OrderCreation model)
        {
            var res = new OrderResponse() { Status = "failed" };
            if (!ModelState.IsValid)
                return res;

            var newOrder = await _orderService.CreateNewAsync(model);
            if (newOrder != null && newOrder.Id > 0)
            {
                res.ID = newOrder.Id;
                res.Status = "success";
                res.TS = newOrder.CreatedDate.ToString();
            }

            return res;
        }
    }
}