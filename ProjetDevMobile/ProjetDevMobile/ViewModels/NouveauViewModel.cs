using Xamarin.Forms.Maps;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProjetDevMobile.Models;
using ProjetDevMobile.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using static ProjetDevMobile.Models.Enregistrement;
using Plugin.Geolocator.Abstractions;
using Plugin.Geolocator;
using System.Diagnostics;

namespace ProjetDevMobile.ViewModels
{
	public class NouveauViewModel : ViewModelBase
	{
        private IEnregistrementService _enregistrementService;
        private IGeolocalisationService _geolocalisationService;
        private Enregistrement _enregistrement;

        public DelegateCommand CommandAjoutEnregistrement { get; private set; }
        public DelegateCommand CommandPrendrePhoto { get; private set; }

        private string _nom = "";
        public string Nom
        {
            get { return _nom; }
            set { SetProperty(ref _nom, value); }
        }
        private string _description = "";
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }
        private List<string> _listeTags = new List<string>();
        public List<string> ListeTags
        {
            get { return _listeTags; }
            set { SetProperty(ref _listeTags, value); }
        }
        private string _selectedTag = "";
        public string SelectedTag
        {
            get { return _selectedTag; }
            set { SetProperty(ref _selectedTag, value); }
        }
        private string position = "";
        public string Position
        {
            get { return position; }
            set { SetProperty(ref position, value); }
        }
        private string _adresse = "";
        public string Adresse
        {
            get { return _adresse; }
            set { SetProperty(ref _adresse, value); }
        }
        private Boolean _modeNouveau;
        public Boolean ModeNouveau
        {
            get { return _modeNouveau; }
            set { SetProperty(ref _modeNouveau, value); }
        }

        private ImageSource _sourceImage = null;
        public ImageSource SourceImage
        {
            get { return _sourceImage; }
            set { SetProperty(ref _sourceImage, value); }
        }
        private string _heurePhoto = "";
        public string HeurePhoto
        {
            get { return _heurePhoto; }
            set { SetProperty(ref _heurePhoto, value); }
        }

        private byte[] PhotoArray { get; set; }

        public NouveauViewModel(INavigationService navigationService, IEnregistrementService enregistrementService, IGeolocalisationService geolocalisationService)
            : base(navigationService)
        {
            CommandAjoutEnregistrement = new DelegateCommand(AjoutEnregistrement, CanAjout).ObservesProperty(() => Nom).ObservesProperty(() => Description).ObservesProperty(() => SourceImage).ObservesProperty(() => SelectedTag);
            CommandPrendrePhoto = new DelegateCommand(PrendrePhotoAsync);
            _enregistrementService = enregistrementService;
            _geolocalisationService = geolocalisationService;
            ListeTags.AddRange(Enum.GetNames(typeof(Enregistrement.ETag)));
        } 

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            _enregistrement = parameters.GetValue<Enregistrement>("Enregistrement");

            if (_enregistrement == null)
            {
                ModeNouveau = true;
                Title = "Nouveau";
            }
            else
            {
                ModeNouveau = false;
                Title = "Edition";
                Nom = _enregistrement.Nom;
                Description = _enregistrement.Description;
                SourceImage = _enregistrement.GetImageSource();
                HeurePhoto = _enregistrement.HeurePhoto;
                SelectedTag = _enregistrement.Tag;
                Position = _enregistrement.GetPositionString();
                Adresse = _enregistrement.GetAdresseString();
            }
        }

        private bool CanAjout()
        {
            if (Nom == "")
                return false;

            if (Description == "")
                return false;

            if (SelectedTag == "")
                return false;
            
            if (SourceImage == null)
                return false;

            return true;
        }
        private async void AjoutEnregistrement()
        {
            var reponse = await App.Current.MainPage.DisplayAlert("Confirmation", "Voulez-vous enregistrer ?", "Confirmer", "Annuler");

            if (reponse)
            {
                if (ModeNouveau)
                {
                    /* Version qui devrait fonctionner avec le service GeolocalisationService
                     * 
                    Plugin.Geolocator.Abstractions.Position position = _geolocalisationService.GetCurrentLocation().Result;
                    string adresse = "";
                    if (position != null)
                    {
                        Position posTemp = new Position(position.Latitude, position.Longitude);
                        adresse = _geolocalisationService.GetAddressForPositionAsync(posTemp).Result;
                    }*/
                    
                    IGeolocator locator = CrossGeolocator.Current;
                    Geocoder geocoder = new Geocoder();
                    Plugin.Geolocator.Abstractions.Position position = null;
                    string adresse = "";
                    try
                    {
                        position = await locator.GetLastKnownLocationAsync();
                        if (position == null)
                        {
                            if (CrossGeolocator.IsSupported && locator.IsGeolocationAvailable && locator.IsGeolocationEnabled)
                            {
                                position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);
                            }
                        }

                        if (position != null)
                        {
                            var adresses = await geocoder.GetAddressesForPositionAsync(new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude));
                            adresse = adresses.FirstOrDefault();
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Unable to get location or address: " + ex);
                    }

                    _enregistrement = new Enregistrement(Nom, Description, SelectedTag, PhotoArray, HeurePhoto, DateTime.Now, position, adresse);
                    _enregistrementService.AddEnregistrement(_enregistrement);
                }
                else
                {
                    _enregistrement.Nom = Nom;
                    _enregistrement.Description = Description;
                    _enregistrementService.UpdateEnregistrement(_enregistrement);
                }
                await NavigationService.NavigateAsync("/Menu/NavigationPage/MainPage/Enregistrements");
            }
        }

        private async void PrendrePhotoAsync()
        {
            var reponse = await App.Current.MainPage.DisplayAlert("Photo", "Souhaitez-vous prendre une nouvelle photo ou en sélectionner une de votre galerie ?", "Appareil photo", "Galerie");
            MediaFile photo;
            if (reponse)
            {
                photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
                {
                    PhotoSize = PhotoSize.Medium
                });
            }
            else
            {
                photo = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions()
                {
                    PhotoSize = PhotoSize.Small
                });
            }


            if (photo != null)
            {
                var now = DateTime.Now;
                HeurePhoto = "Photo enregistrée à : " + now.Hour + "h" + now.Minute;

                SourceImage = ImageSource.FromStream(photo.GetStream);
                using (var memoryStream = new MemoryStream())
                {
                    photo.GetStream().CopyTo(memoryStream);
                    PhotoArray = memoryStream.ToArray();
                }

            }     
        }
    }
}
