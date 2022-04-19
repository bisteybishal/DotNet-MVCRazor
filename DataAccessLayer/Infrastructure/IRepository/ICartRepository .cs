using MyApp.Models;

using MyAppModels.ViewModels;

namespace MyApp.DataAccessLayer.Infrastructure.IRepository
{
    public interface ICartRepository : IRepository<Cart>
    {
        public IEnumerable<Cart> GetAll(string userid);
        public int IncrementCartitem(Cart cart, int count);
        public int DecrementCartitem(Cart cart, int count);
    }
}
