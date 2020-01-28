using API.Application.Interfaces;
using Core.Interfaces.Repositories;

namespace API.Application.Services
{
    public class CategoryAppService : ICategoryAppService
    {
        private readonly ICategoryRepository _categories;

        public CategoryAppService(ICategoryRepository categories)
        {
            _categories = categories;
        }
    }
}
