using Microsoft.AspNetCore.Mvc;
using stepmedia_demo.EntityModels;
using stepmedia_demo.Repositories;
using stepmedia_demo.Services;
using System.Web.Helpers;

namespace stepmedia_demo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShopsController : ControllerBase
    {
        private readonly ILogger<ShopsController> _logger;
        private readonly IShopService _shopService;

        public ShopsController(ILogger<ShopsController> logger, IShopService shopService)
        {
            _logger = logger;
            _shopService = shopService;
        }

        [HttpGet]
        public async Task<PaginationResult<ShopDto>> GetList(string? orderBy,
                                                     SortDirection? orderDirection,
                                                     int? page,
                                                     int? pageSize)
        {
            return await _shopService.GetListAsync(orderBy, orderDirection, page, pageSize, s => new ShopDto
            {
                Id = s.Id,
                Name = s.Name,
                Location = s.Location,
                CreatedDate = s.CreatedDate,
            });
        }

        [HttpPost]
        public async Task<ShopResponse> Create(ShopCreation model)
        {
            var res = new ShopResponse() { Status = "failed" };
            if (!ModelState.IsValid)
                return res;

            var newShop = await _shopService.CreateNewAsync(model);
            if (newShop != null && newShop.Id > 0)
            {
                res.ID = newShop.Id;
                res.Status = "success";
                res.TS = newShop.CreatedDate.ToString();
            }

            return res;
        }
    }
}