using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.DataAccessLayer.Infrastructure.IRepository
{
    public interface IRepository <T> where T:class
    {
        IEnumerable<T> GetAll();
        T GetT(Expression<Func<T, bool>>predicate);

        void Add(T Entity);
        void Delete(T Entity);
        void DeleteRange(IEnumerable<T> Entity);
    }
}
