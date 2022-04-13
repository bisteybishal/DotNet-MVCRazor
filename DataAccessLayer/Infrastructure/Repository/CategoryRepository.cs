﻿using Infrastructure.Repository;
using MyApp.DataAccessLayer.Data;
using MyApp.DataAccessLayer.Infrastructure.IRepository;
using MyApp.Models;

namespace  MyApp.DataAccessLayer.Infrastructure.IRepository
{ 
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }

        

        public void Update(Category category)
        {
            var categoryDb = _context.Categories.FirstOrDefault(x => x.Id == category.Id);
            if (categoryDb != null)
            {
                categoryDb.Name = category.Name;
                categoryDb.DisplayOrder = category.DisplayOrder;
            }
        
        }
    }
}

