using Infrastructure.Repository;
using MyApp.DataAccessLayer.Data;
using MyApp.DataAccessLayer.Infrastructure.IRepository;
using MyAppModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.DataAccessLayer.Infrastructure.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Product product)
        {
           var productDb=_context.Products.FirstOrDefault(x=>x.Id==product.Id);
            if (productDb != null)
            {
                productDb.ProductName = product.ProductName;
                productDb.Price = product.Price;
                productDb.Description=product.Description;
                if (product.imageurl != null)
                {
                    productDb.imageurl=product.imageurl;
                }
            }
        }
    }
}
