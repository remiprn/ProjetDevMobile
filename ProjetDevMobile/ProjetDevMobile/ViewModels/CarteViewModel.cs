using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProjetDevMobile.Models;
using ProjetDevMobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace ProjetDevMobile.ViewModels
{
	public class CarteViewModel : ViewModelBase
	{
        private IGeolocalisationService _geolocalisationService;
        private IEnregistrementService _enregistrementService;
        private Position _pos = new Position();

        private Map _carteView;
        public Map CarteView
        {
            get { return _carteView; }
            set { SetProperty(ref _carteView, value); }
        }
        private ObservableCollection<Enregistrement> _enregistrements;
        public ObservableCollection<Enregistrement> Enregistrements
        {
            get { return _enregistrements; }
            set { SetProperty(ref _enregistrements, value); }
        }
        private double _sliderValue = 1;
        public double SliderValue
        {
            get { return _sliderValue; }
            set { SetProperty(ref _sliderValue, value); }
        }

        public DelegateCommand CommandOnSliderValueChanged { get; private set; }

        public CarteViewModel(INavigationService navigationService, IEnregistrementService enregistrementService, IGeolocalisationService geolocalisationService)
            : base(navigationService)
        {
            Title = "Carte";
            _geolocalisationService = geolocalisationService;
            _enregistrementService = enregistrementService;

            CommandOnSliderValueChanged = new DelegateCommand(OnSliderValueChanged);

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

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            CarteView.Pins.Clear();

            Enregistrements = new ObservableCollection<Enregistrement>(_enregistrementService.GetEnregistrements());

            foreach (Enregistrement e in Enregistrements)
            {
                if (e.Position != null)
                {
                    var pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = new Position(e.Position.Latitude, e.Position.Longitude),
                        Label = e.Nom,
                        Address = "#" + e.Tag
                    };
                    CarteView.Pins.Add(pin);
                }
            }

            _pos = parameters.GetValue<Position>("Position");
            if (_pos.Latitude != 0 && _pos.Longitude != 0)
                CarteView.MoveToRegion(MapSpan.FromCenterAndRadius(_pos, Distance.FromMiles(1)));
        }

        void OnSliderValueChanged()
        {
            var zoomLevel = SliderValue; // between 1 and 18
            var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
            if (CarteView.VisibleRegion != null)
                CarteView.MoveToRegion(new MapSpan(CarteView.VisibleRegion.Center, latlongdegrees, latlongdegrees));
        }
    }
}
