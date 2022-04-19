using Infrastructure.Repository;
using MyApp.DataAccessLayer.Data;
using MyApp.DataAccessLayer.Infrastructure.IRepository;
using MyApp.Models;
using MyAppModels.ViewModels;

namespace MyApp.DataAccessLayer.Infrastructure.IRepository
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }

        public int DecrementCartitem(Cart cart, int count)
        {
         cart.Count -= count;
            return cart.Count;
        }

        public IEnumerable<Cart> GetAll(string userid)
        {
            return _context.Carts.Where(x => x.ApplicationUserId == userid).ToList();   
        }

        public int IncrementCartitem(Cart cart, int count)
        {
            cart.Count += count;
            return cart.Count;
        }
    }
}
