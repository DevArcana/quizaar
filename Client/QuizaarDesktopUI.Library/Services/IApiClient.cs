using System.Collections.Generic;
using System.Threading.Tasks;
using QuizaarDesktopUI.Library.Models;

namespace QuizaarDesktopUI.Library.Services
{
    public interface IApiClient
    {
        Task<List<CategoryShallowDTO>> GetShallowCategories();
        Task<List<CategoryDTO>> GetFullCategories();
        Task<IEnumerable<ICategoryDTO>> GetCategories(bool shallow);
        Task PostCategory(CategoryShallowDTO category);
        // Task PostDummyQuestions();
    }
}