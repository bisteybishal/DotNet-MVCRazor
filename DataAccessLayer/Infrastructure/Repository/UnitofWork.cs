using MyApp.DataAccessLayer.Data;
using MyApp.DataAccessLayer.Infrastructure.IRepository;


namespace MyApp.DataAccessLayer.Infrastructure.Repository
{
    public class UnitofWork : IUnitofWork
    {
        private ApplicationDbContext _context;
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }

        public UnitofWork(ApplicationDbContext context) 
        {
            _context = context;
            Category = new CategoryRepository(context);
            Product = new ProductRepository(context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }



}
