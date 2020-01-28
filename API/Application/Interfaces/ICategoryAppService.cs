using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.ViewModels;

namespace API.Application.Interfaces
{
    public interface ICategoryAppService
    {
        Task<CategoryViewModel> GetCategoryById(long id);
        Task<IEnumerable<CategoryViewModel>> GetCategories();
    }
}
