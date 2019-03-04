using System;
using System.Collections.Generic;
using System.Text;
using ProjetDevMobile.Models;

namespace ProjetDevMobile.Services
{
    class EnregistrementService : IEnregistrementService
    {
        private List<Enregistrement> _enregistrements;

        public EnregistrementService()
        {
            _enregistrements = new List<Enregistrement>();

            Enregistrement enregistrement = new Enregistrement("Best beers in town !", "Description 1", "Drink", "image 1", DateTime.Today);
            _enregistrements.Add(enregistrement);

            enregistrement = new Enregistrement("Amazing church", "Description 2", "ToSee", "image 2", new DateTime(2019, 2, 28));
            _enregistrements.Add(enregistrement);

            enregistrement = new Enregistrement("Mojito bar", "Description 3", "Drink", "image 3", new DateTime(2019, 2, 15));
            _enregistrements.Add(enregistrement);

            enregistrement = new Enregistrement("Sushi to go", "Description 4", "Food", "image 4", new DateTime(2018, 10, 20));
            _enregistrements.Add(enregistrement);
        }

        public void AddEnregistrement(Enregistrement enregistrement)
        {
            _enregistrements.Add(enregistrement);
        }

        public void DeleteEnregistrement(Enregistrement enregistrement)
        {
            _enregistrements.Remove(enregistrement);
        }

        public Enregistrement GetEnregistrement(int pos)
        {
            return _enregistrements[pos];
        }

        public List<Enregistrement> GetEnregistrements()
        {
            return _enregistrements;
        }
    }
}
