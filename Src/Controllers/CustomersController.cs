using Microsoft.AspNetCore.Mvc;
using stepmedia_demo.EntityModels;
using stepmedia_demo.Repositories;
using stepmedia_demo.Services;
using System.Web.Helpers;

namespace stepmedia_demo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ICustomerService _customerService;

        public CustomersController(ILogger<CustomersController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpGet] 
        public async Task<PaginationResult<CustomerDto>> GetList(string? orderBy,
                                                     SortDirection? orderDirection,
                                                     int? page,
                                                     int? pageSize)
        {
            return await _customerService.GetListAsync(orderBy, orderDirection, page, pageSize, s => new CustomerDto
            {
                Id = s.Id,
                FullName = s.FullName,
                Email = s.Email,
                Dob = s.DoB.HasValue ? s.DoB.Value.ToString("dd/MM/yyyy") : "-",
                CreatedDate = s.CreatedDate,
            });
        }

        [HttpPost]
        public async Task<CustomerResponse> Create(CustomerCreation model)
        {
            var res = new CustomerResponse() { Status = "failed" };
            if (!ModelState.IsValid)
                return res;

            var newCustomer = await _customerService.CreateNewAsync(model);
            if(newCustomer != null && newCustomer.Id > 0)
            {
                res.ID = newCustomer.Id;
                res.Status = "success";
                res.TS = newCustomer.CreatedDate.ToString();
            }

            return res;
        }
    }
}