using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Core.Interfaces.Repositories;

namespace Infrastructure.Data.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(QuizAppContext context) : base(context)
        {

        }
    }
}
