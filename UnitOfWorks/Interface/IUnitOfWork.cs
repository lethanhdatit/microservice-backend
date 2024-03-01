using stepmedia_demo.EntityModels;
using stepmedia_demo.Repositories;

namespace stepmedia_demo.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Customer> CustomerRepository { get; }
        IGenericRepository<Order> OrderRepository { get; }
        IGenericRepository<OrderDetail> OrderDetailRepository { get; }
        IGenericRepository<Product> ProductRepository { get; }
        IGenericRepository<Shop> ShopRepository { get; }

        Task<int> SaveChangesAsync();
        void SaveChanges();
    }
}
