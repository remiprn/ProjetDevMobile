using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetDevMobile.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        public DelegateCommand<string> NavigateCommand { get; private set; }

        public MenuViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void Navigate(string view)
        {
            NavigationService.NavigateAsync("NavigationPage/" + view);
        }
    }
}
