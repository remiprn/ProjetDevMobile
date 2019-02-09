using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetDevMobile.ViewModels
{
	public class NouveauViewModel : ViewModelBase
	{
        public NouveauViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Nouveau";
        }
	}
}
