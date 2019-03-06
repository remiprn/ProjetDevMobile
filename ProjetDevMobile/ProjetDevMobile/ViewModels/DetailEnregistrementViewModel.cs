﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProjetDevMobile.Models;
using ProjetDevMobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace ProjetDevMobile.ViewModels
{
	public class DetailEnregistrementViewModel : ViewModelBase
    {
        private Enregistrement _enregistrement;

        private string _nom;
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
        private string _tag;
        public string Tag
        {
            get { return _tag; }
            set { SetProperty(ref _tag, value); }
        }

        public DelegateCommand CommandUpdateEnregistrement { get; private set; }
        public DelegateCommand CommandDeleteEnregistrement { get; private set; }

        private IEnregistrementService _enregistrementService;

        public DetailEnregistrementViewModel(INavigationService navigationService, IEnregistrementService enregistrementService)
            : base(navigationService)
        {
            Title = "Enregistrement";
            CommandUpdateEnregistrement = new DelegateCommand(UpdateEnregistrement);
            CommandDeleteEnregistrement = new DelegateCommand(DeleteEnregistrement);

            _enregistrementService = enregistrementService;
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            _enregistrement = parameters.GetValue<Enregistrement>("Enregistrement");
            this.Nom = _enregistrement.Nom;
            this.Tag = _enregistrement.Tag;
            this.Description = _enregistrement.Description;
        }

        private void UpdateEnregistrement()
        {
            throw new NotImplementedException();
        }

        private async void DeleteEnregistrement()
        {
            var reponse = await App.Current.MainPage.DisplayAlert("Suppression", "Etes-vous sûr de vouloir supprimer l'enregistrement ? Cette action n'est pas réversible.", "Confirmer", "Annuler");

            if (reponse)
            {
                _enregistrementService.DeleteEnregistrement(_enregistrement);
                await NavigationService.NavigateAsync("/Menu/NavigationPage/MainPage/Enregistrements");
            }
        }
    }
}