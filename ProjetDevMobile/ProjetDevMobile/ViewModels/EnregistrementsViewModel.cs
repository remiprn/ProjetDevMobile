using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetDevMobile.ViewModels
{
	public class EnregistrementsViewModel : ViewModelBase
	{
        public EnregistrementsViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Enregistrements";
        }
	}
}
