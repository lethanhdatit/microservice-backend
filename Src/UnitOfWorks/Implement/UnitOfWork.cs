using stepmedia_demo.EntityModels;
using stepmedia_demo.Repositories;

namespace stepmedia_demo.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private StepmediaDemoContext _context;

        public UnitOfWork(StepmediaDemoContext context)
        {
            _context = context;
            InitRepositories();
        }
        private void InitRepositories()
        {
            CustomerRepository = new GenericRepository<Customer>(_context);
            OrderRepository = new GenericRepository<Order>(_context);
            OrderDetailRepository = new GenericRepository<OrderDetail>(_context);
            ProductRepository = new GenericRepository<Product>(_context);
            ShopRepository = new GenericRepository<Shop>(_context);
        }
        private bool disposedValue;

        public IGenericRepository<Customer> CustomerRepository { get; private set; }
        public IGenericRepository<Order> OrderRepository { get; private set; }
        public IGenericRepository<OrderDetail> OrderDetailRepository { get; private set; }
        public IGenericRepository<Product> ProductRepository { get; private set; }
        public IGenericRepository<Shop> ShopRepository { get; private set; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~UnitOfWork()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
