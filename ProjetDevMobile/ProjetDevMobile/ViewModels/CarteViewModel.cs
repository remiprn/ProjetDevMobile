using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProjetDevMobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace ProjetDevMobile.ViewModels
{
	public class CarteViewModel : ViewModelBase
	{
        private IGeolocalisationService _geolocalisationService;

        private Map _carteView;
        public Map CarteView
        {
            get { return _carteView; }
            set { SetProperty(ref _carteView, value); }
        }

        public CarteViewModel(INavigationService navigationService, IGeolocalisationService geolocalisationService)
            : base(navigationService)
        {
            Title = "Carte";
            _geolocalisationService = geolocalisationService;

            Plugin.Geolocator.Abstractions.Position position = _geolocalisationService.GetCurrentLocation().Result;

            CarteView = new Map(
            MapSpan.FromCenterAndRadius(
                    new Position(position.Latitude, position.Longitude), Distance.FromMiles(1)))
                    {
                        IsShowingUser = true,
                        HeightRequest = 1000,
                        WidthRequest = 320,
                        VerticalOptions = LayoutOptions.FillAndExpand
                    };
        }
    }
}
