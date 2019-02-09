using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetDevMobile.ViewModels
{
	public class BonusViewModel : ViewModelBase
    {
        public BonusViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Bonus";
        }
	}
}
