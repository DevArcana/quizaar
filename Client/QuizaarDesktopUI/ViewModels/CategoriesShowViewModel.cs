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
    public class CategoriesShowViewModel : Screen
    {
        private IApiClient _apiClient;
        private bool _shallowLoaded;

        public bool ShallowLoaded
        {
            get { return _shallowLoaded; }
            set
            {
                _shallowLoaded = value;
                NotifyOfPropertyChange(() => ShallowLoaded);
                NotifyOfPropertyChange(() => Categories);
                NotifyOfPropertyChange(() => Questions);
            }
        }

        public CategoriesShowViewModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
            ShallowLoaded = false;
        }

        public async Task LoadShallow()
        {
            var categoryList = await _apiClient.GetShallowCategories();
            Categories = new BindingList<CategoryShallowDTO>(categoryList);
            ShallowLoaded = true;
        }

        public async Task LoadFull()
        {
            //var categoryList = await _apiClient.GetFullCategories();
            //Categories = new BindingList<CategoryDTO>(categoryList);
            //ShallowLoaded = false;
        }

        private IEnumerable<ICategoryDTO> _categories;

        public IEnumerable<ICategoryDTO> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                NotifyOfPropertyChange(() => Categories);
            }
        }

        private IEnumerable<string> _questions;

        public IEnumerable<string> Questions
        {
            get { return _questions; }
            set
            {
                _questions = value;
                NotifyOfPropertyChange(() => Questions);
            }
        }
    }
}
