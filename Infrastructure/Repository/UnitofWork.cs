using Infrastructure.IRepository;
using Infrastructure.Repository;
using MyAppWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.DataAccessLayer.Infrastructure.Repository
{
    public class UnitofWork : IUnitofWork
    {
        private ApplicationDbContext _context;
        public ICategoryRepository Category { get; private set; }

        public UnitofWork(ApplicationDbContext context) 
        {
            _context = context;
            Category = new CategoryRepository(context);
        }
        
        public void save()
        {
            _context.SaveChanges();
        }
    }



}
