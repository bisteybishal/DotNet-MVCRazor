using Infrastructure.IRepository;
using Microsoft.EntityFrameworkCore;
using MyAppWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbset;
   
        public Repository(ApplicationDbContext context)
        {
            _context = context;
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
          return  _dbset.ToList();
        }

        public T GetT(Expression<Func<T, bool>> predicate)
        {
            return _dbset.Where(predicate).FirstOrDefault();
        }
    }

    
}

