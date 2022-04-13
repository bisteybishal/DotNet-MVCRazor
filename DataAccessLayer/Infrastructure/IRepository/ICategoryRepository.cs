using MyApp.Models;

using MyAppModels.ViewModels;

namespace MyApp.DataAccessLayer.Infrastructure.IRepository
{
    public interface ICategoryRepository:IRepository<Category>
    {
        void Update(Category category);
       
    }
}
