using Prism.Commands;
using Prism.Navigation;
using ProjetDevMobile.Models;
using ProjetDevMobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProjetDevMobile.ViewModels
{
    public class EnregistrementsViewModel : ViewModelBase
	{
        public static string imgCheck = "@drawable/check.png";
        public static string imgUnCheck = "@drawable/uncheck.png";

        private Boolean DrinkBool = true;
        private Boolean FoodBool = true;
        private Boolean ToSeeBool = true;

        private Boolean OrdreTri = true;

        private List<Enregistrement> AllEnregistrements;

        private Boolean _triHautEnable = false;
        public Boolean TriHautEnable
        {
            get { return _triHautEnable; }
            set { SetProperty(ref _triHautEnable, value); }
        }

        private Boolean _triBasEnable = true;
        public Boolean TriBasEnable
        {
            get { return _triBasEnable; }
            set { SetProperty(ref _triBasEnable, value); }
        }

        private string _imageAdresseDrink = imgCheck;
        public string ImageAdresseDrink
        {
            get { return _imageAdresseDrink; }
            set { SetProperty(ref _imageAdresseDrink, value); }
        }
        private string _imageAdresseFood = imgCheck;
        public string ImageAdresseFood
        {
            get { return _imageAdresseFood; }
            set { SetProperty(ref _imageAdresseFood, value); }
        }
        private string _imageAdresseToSee = imgCheck;
        public string ImageAdresseToSee
        {
            get { return _imageAdresseToSee; }
            set { SetProperty(ref _imageAdresseToSee, value); }
        }

        private ObservableCollection<Enregistrement> _enregistrements;
        public ObservableCollection<Enregistrement> Enregistrements
        {
            get { return _enregistrements; }
            set { SetProperty(ref _enregistrements, value); }
        }

        private string _textBar;
        public string TextBar
        {
            get { return _textBar; }
            set { SetProperty(ref _textBar, value); }
        }

        public DelegateCommand CommandChangeTagDrink { get; private set; }
        public DelegateCommand CommandChangeTagFood { get; private set; }
        public DelegateCommand CommandChangeTagToSee { get; private set; }
        public DelegateCommand<object> OnNavEnregistrement { get; private set; }
        
        public DelegateCommand CommandTriHaut { get; private set; }
        public DelegateCommand CommandTriBas { get; private set; }

        public DelegateCommand SearchCommand { get; private set; }
        public DelegateCommand CommandReset { get; private set; }

        private IEnregistrementService _enregistrementService;

        public EnregistrementsViewModel(INavigationService navigationService, IEnregistrementService enregistrementService)
            : base(navigationService)
        {
            Title = "Enregistrements";

            _enregistrementService = enregistrementService;
            AllEnregistrements = new List<Enregistrement>();

            CommandChangeTagDrink = new DelegateCommand(ChangeTagDrink);
            CommandChangeTagFood = new DelegateCommand(ChangeTagFood);
            CommandChangeTagToSee = new DelegateCommand(ChangeTagToSee);
            OnNavEnregistrement = new DelegateCommand<object>(NavEnregistrement);
            CommandTriHaut = new DelegateCommand(TriHaut);
            CommandTriBas = new DelegateCommand(TriBas);
            SearchCommand = new DelegateCommand(Search);
            CommandReset = new DelegateCommand(Reset);
        }

        private void Reset()
        {
            Enregistrements = new ObservableCollection<Enregistrement>(AllEnregistrements);
            TextBar = "";
            TriEnregistrements();
        }

        private void Search()
        {
            var l = AllEnregistrements.Where(c => c.Nom.ToLower().Contains(TextBar.ToLower()));
            ObservableCollection<Enregistrement> obsl = new ObservableCollection<Enregistrement>();
            foreach (Enregistrement e in l)
            {
                obsl.Add(e);
            }
            Enregistrements = obsl;
            TriEnregistrements();
        }

        private void TriHaut()
        {
            this.OrdreTri = true;
            TriBasEnable = true;
            TriHautEnable = false;
            TriEnregistrements();
        }

        private void TriBas()
        {
            this.OrdreTri = false;
            TriBasEnable = false;
            TriHautEnable = true;
            TriEnregistrements();
        }

        private void TriEnregistrements()
        {
            ObservableCollection<Enregistrement> liste = Enregistrements;
            if (OrdreTri)
            {
                var l = liste.OrderBy(item => DateTime.Now.Subtract(item.Date));
                liste = new ObservableCollection<Enregistrement>(l.ToList<Enregistrement>());
                Enregistrements = new ObservableCollection<Enregistrement>(liste);
            }
            else
            {
                var l = liste.OrderByDescending(item => DateTime.Now.Subtract(item.Date));
                liste = new ObservableCollection<Enregistrement>(l.ToList<Enregistrement>());
                Enregistrements = new ObservableCollection<Enregistrement>(liste);
            }  
        }

        private void ChangeTagDrink()
        {
            DrinkBool = !DrinkBool;
            if (DrinkBool)
                ImageAdresseDrink = imgCheck;
            else
                ImageAdresseDrink = imgUnCheck;
            updateEnregistrement();
        }

        private void ChangeTagFood()
        {
            FoodBool = !FoodBool;
            if (FoodBool)
                ImageAdresseFood = imgCheck;
            else
                ImageAdresseFood = imgUnCheck;
            updateEnregistrement();
        }

        private void ChangeTagToSee()
        {
            ToSeeBool = !ToSeeBool;
            if (ToSeeBool)
                ImageAdresseToSee = imgCheck;
            else
                ImageAdresseToSee = imgUnCheck;
            updateEnregistrement();
        }

        private void updateEnregistrement()
        {
            List<Enregistrement> liste = new List<Enregistrement>();
            if (DrinkBool)
                foreach (Enregistrement e in AllEnregistrements.Where(enr => enr.Tag.Equals("Drink")).ToList<Enregistrement>()){
                liste.Add(e);
            }
            if (FoodBool)
                foreach (Enregistrement e in AllEnregistrements.Where(enr => enr.Tag.Equals("Food")).ToList<Enregistrement>())
                {
                    liste.Add(e);
                }
            if (ToSeeBool)
                foreach (Enregistrement e in AllEnregistrements.Where(enr => enr.Tag.Equals("ToSee")).ToList<Enregistrement>())
                {
                    liste.Add(e);
                }
            Enregistrements = new ObservableCollection<Enregistrement>(liste);
            TriEnregistrements();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            AllEnregistrements = _enregistrementService.GetEnregistrements();
            updateEnregistrement();
        }

        public void NavEnregistrement(object enregistrement)
        {
            var navigationParam = new NavigationParameters();
            navigationParam.Add("Enregistrement", enregistrement);

            NavigationService.NavigateAsync("DetailEnregistrement", navigationParam);
        }
    }
}
