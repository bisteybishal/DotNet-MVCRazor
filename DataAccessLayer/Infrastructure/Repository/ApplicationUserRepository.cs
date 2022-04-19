using Infrastructure.Repository;
using MyApp.DataAccessLayer.Data;
using MyApp.DataAccessLayer.Infrastructure.IRepository;
using MyApp.Models;
using MyAppModels.ViewModels;

namespace MyApp.DataAccessLayer.Infrastructure.IRepository
{
    public class ApplicationUserRepository : Repository<ApplicationUserRepository>, IApplicationUser
    {
        private ApplicationDbContext _context;

        public ApplicationUserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }
    }
}

        

      

