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

namespace ProjetDevMobile.ViewModels
{
	public class NouveauViewModel : ViewModelBase
	{
        private IEnregistrementService _enregistrementService;
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
        private List<String> _listeTags = new List<string>();
        public List<String> ListeTags
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

        private ImageSource _sourceImage;
        public ImageSource SourceImage
        {
            get { return _sourceImage; }
            set { SetProperty(ref _sourceImage, value); }
        }

        private byte[] PhotoArray { get; set; }

        public NouveauViewModel(INavigationService navigationService, IEnregistrementService enregistrementService)
            : base(navigationService)
        {
            Title = "Nouveau";
            CommandAjoutEnregistrement = new DelegateCommand(AjoutEnregistrement);
            CommandPrendrePhoto = new DelegateCommand(PrendrePhotoAsync);
            _enregistrementService = enregistrementService;
            ListeTags.AddRange(Enum.GetNames(typeof(Enregistrement.ETag)));
            SourceImage = "@drawable/uncheck.png";
        }

        private void AjoutEnregistrement()
        {
            Enregistrement e = new Enregistrement(Nom, Description, SelectedTag, PhotoArray, DateTime.Today);
            _enregistrementService.AddEnregistrement(e);
            NavigationService.NavigateAsync("/Menu/NavigationPage/MainPage/Enregistrements");
        }

        private async void PrendrePhotoAsync()
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() {
                PhotoSize = PhotoSize.Medium,
            });

            if (photo != null)
            {
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
