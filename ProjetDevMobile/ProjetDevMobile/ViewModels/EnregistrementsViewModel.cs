using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProjetDevMobile.Models;
using ProjetDevMobile.Services;
using System;
using System.Collections.Generic;
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

        private List<Enregistrement> AllEnregistrements;

        private String _imageAdresseDrink = imgCheck;
        public String ImageAdresseDrink
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

        private List<Enregistrement> _enregistrements;
        public List<Enregistrement> Enregistrements
        {
            get { return _enregistrements; }
            set { SetProperty(ref _enregistrements, value); }
        }

        public DelegateCommand CommandChangeTagDrink { get; private set; }
        public DelegateCommand CommandChangeTagFood { get; private set; }
        public DelegateCommand CommandChangeTagToSee { get; private set; }

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
            Enregistrements = liste;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            AllEnregistrements = _enregistrementService.GetEnregistrements();
            updateEnregistrement();
        }
    }
}
