using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Caliburn.Micro;

namespace QuizaarDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private CategoriesViewModel _categoriesVM;

        public ShellViewModel(CategoriesViewModel categoriesVM)
        {
            _categoriesVM = categoriesVM;
            ActivateItemAsync(_categoriesVM, new CancellationToken());
        }

        public void ExitApplication()
        {
            TryCloseAsync();
        }
    }
}
