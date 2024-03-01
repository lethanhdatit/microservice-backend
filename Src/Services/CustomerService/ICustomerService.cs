using stepmedia_demo.EntityModels;
using stepmedia_demo.Repositories;
using System.Web.Helpers;

namespace stepmedia_demo.Services
{
    public interface ICustomerService : IBaseService<Customer, CustomerDto>
    {
        Task<Customer> CreateNewAsync(CustomerCreation input);
    }
}
