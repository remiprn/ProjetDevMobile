using System;
using System.Collections.Generic;
using System.Text;
using ProjetDevMobile.Client;
using ProjetDevMobile.Models;
using Xamarin.Forms.Maps;

namespace ProjetDevMobile.Services
{
    class EnregistrementService : IEnregistrementService
    {
        private List<Enregistrement> _enregistrements;

        private ILiteDBClient _liteDBClient;
        private string _dbCollectionEnregistrement = "collectionEnregistrement";


        public EnregistrementService(ILiteDBClient liteDBClient)
        {
            _liteDBClient = liteDBClient;

            //Mock enregistrement
            byte[] img = new byte[] { };
            Enregistrement enregistrement = new Enregistrement("Best beers in town !", "Description 1", "Drink", img, "Photo prise à : 12h15", new DateTime(2019,1,1), new Position(49.108013, 6.167201), "Adresse 1");
            Enregistrement enregistrement2 = new Enregistrement("Best cookie in town !", "Description 2", "Food", img, "Photo prise à : 12h47", new DateTime(2019, 1, 15), new Position(49.132553, 6.207610), "Adresse 2");
            //_liteDBClient.CleanCollection(_dbCollectionEnregistrement);

            //_liteDBClient.InsertObjectInDB<Enregistrement>(enregistrement, _dbCollectionEnregistrement);
            //_liteDBClient.InsertObjectInDB<Enregistrement>(enregistrement2, _dbCollectionEnregistrement);


            _enregistrements = _liteDBClient.GetCollectionFromDB<Enregistrement>(_dbCollectionEnregistrement);
            //_enregistrements.Add(enregistrement);

            /*enregistrement = new Enregistrement("Amazing church", "Description 2", "ToSee", img, new DateTime(2019, 2, 28));
            _enregistrements.Add(enregistrement);

            enregistrement = new Enregistrement("Mojito bar", "Description 3", "Drink", img, new DateTime(2019, 2, 15));
            _enregistrements.Add(enregistrement);

            enregistrement = new Enregistrement("Sushi to go", "Description 4", "Food", img, new DateTime(2018, 10, 20));
            _enregistrements.Add(enregistrement);*/
        }

        public void AddEnregistrement(Enregistrement enregistrement)
        {
            _liteDBClient.InsertObjectInDB<Enregistrement>(enregistrement, _dbCollectionEnregistrement);
            _enregistrements.Add(enregistrement);
        }

        public void UpdateEnregistrement(Enregistrement enregistrement)
        {
            _liteDBClient.UpdateObjectInDB<Enregistrement>(enregistrement, _dbCollectionEnregistrement);
        }

        public void DeleteEnregistrement(Enregistrement enregistrement)
        {
            _liteDBClient.RemoveObjectFromDB<Enregistrement>(enregistrement.Id, _dbCollectionEnregistrement);
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
