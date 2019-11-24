using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Caliburn.Micro;

namespace QuizaarDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private CategoriesShowViewModel _categoriesSVM;

        public ShellViewModel(CategoriesShowViewModel categoriesSVM)
        {
            _categoriesSVM = categoriesSVM;
            ActivateItemAsync(_categoriesSVM, new CancellationToken());
        }

        public void ExitApplication()
        {
            TryCloseAsync();
        }
    }
}
