using ProjetDevMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetDevMobile.Services
{
    public interface IEnregistrementService
    {   
        List<Enregistrement> GetEnregistrements();

        Enregistrement GetEnregistrement(int pos);

        void AddEnregistrement(Enregistrement enregistrement);

        void DeleteEnregistrement(Enregistrement enregistrement);
    }
}
