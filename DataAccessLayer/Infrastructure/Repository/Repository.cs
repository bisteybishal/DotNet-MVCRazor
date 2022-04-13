using Microsoft.EntityFrameworkCore;
using MyApp.DataAccessLayer.Data;
using MyApp.DataAccessLayer.Infrastructure.IRepository;
using System;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private  ApplicationDbContext _context;
        private  DbSet<T> _dbset;
        
        public Repository(ApplicationDbContext context)
        {
            _context = context;
         //   _context.Products.Include(x => x.category);
            _dbset = context.Set<T>();

        }

        public void Add(T Entity)
        {
            _dbset.Add(Entity);
        }

        public void Delete(T Entity)
        {
            _dbset.Remove(Entity);

        }

        public void DeleteRange(IEnumerable<T> Entity)
        {
            _dbset.RemoveRange(Entity);
        }

        public IEnumerable<T> GetAll()
        {
            
            return _dbset.ToList();
        }

        public T GetT(Expression<Func<T, bool>> predicate)
        {
           

            return _dbset.Where(predicate).FirstOrDefault();
        }

        
    }

    
}

