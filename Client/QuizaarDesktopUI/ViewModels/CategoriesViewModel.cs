using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using QuizaarDesktopUI.Library.Models;
using QuizaarDesktopUI.Library.Services;

namespace QuizaarDesktopUI.ViewModels
{
    public class CategoriesViewModel : Screen
    {
        private IApiClient _apiClient;

        public CategoriesViewModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadCategories();
        }

        private async Task LoadCategories()
        {
            var categoryList = await _apiClient.GetShallowCategories();
            Categories = new BindingList<CategoryShallowDTO>(categoryList);
            //Categories = new BindingList<CategoryShallowDTO>();
            //Categories.Add(new CategoryShallowDTO
            //{
            //    Id = 1,
            //    Name = "Cat1"
            //});
            //Categories.Add(new CategoryShallowDTO
            //{
            //    Id = 2,
            //    Name = "Cat2"
            //});
            //Categories.Add(new CategoryShallowDTO
            //{
            //    Id = 3,
            //    Name = "Cat3"
            //});
        }

        private BindingList<CategoryShallowDTO> _categories;

        public BindingList<CategoryShallowDTO> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                NotifyOfPropertyChange(() => Categories);
            }
        }

        private string _categoryName;

        public string CategoryName
        {
            get { return _categoryName; }
            set
            {
                _categoryName = value;
                NotifyOfPropertyChange(() => CategoryName);
                NotifyOfPropertyChange(() => CanAddCategory);
            }
        }

        public bool CanAddCategory
        {
            get
            {
                bool output = false;

                if (_categoryName?.Length > 0)
                {
                    output = true;
                }

                return output;
            }
        }

        public async Task AddCategory()
        {
            var category = new CategoryShallowDTO
            {
                Name = CategoryName
            };

            await _apiClient.PostCategory(category);

            CategoryName = string.Empty;

            Categories.Add(category);
        }
    }
}
