using Microsoft.EntityFrameworkCore;
using stepmedia_demo.EntityModels;
using stepmedia_demo.Repositories;
using stepmedia_demo.UnitOfWork;
using System.Linq.Expressions;
using System.Web.Helpers;

namespace stepmedia_demo.Services
{
    public class CustomerService : BaseService<Customer, CustomerDto>, ICustomerService
    {
        public CustomerService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override IGenericRepository<Customer> _reponsitory => _unitOfWork.CustomerRepository;

        public async Task<Customer> CreateNewAsync(CustomerCreation input)
        {
            var newEntity = new Customer()
            {
                FullName = input.FullName,
                Email = input.Email,
                DoB = DateTime.Parse(input.Dob).Date,
                CreatedDate = DateTime.UtcNow
            };

            return await CreateAsync(newEntity);
        }
    }
}
