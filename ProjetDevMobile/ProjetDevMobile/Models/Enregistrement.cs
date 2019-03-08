using Xamarin.Forms.Maps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace ProjetDevMobile.Models
{
    public class Enregistrement
    {
        public enum ETag { Drink, Food, ToSee }

        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public byte[] Image { get; set; }
        public string HeurePhoto { get; set; }
        public DateTime Date { get; set; }
        public Position Position { get; set; }
        public string Adresse { get; set; }

        public Enregistrement(string nom, string description, string tag, byte[] image, string heurePhoto, DateTime date, Position position, string adresse)
        {
            Nom = nom;
            Description = description;
            Tag = tag;
            Image = image;
            HeurePhoto = heurePhoto;
            Date = date;
            Position = position;
            Adresse = adresse;
        }

        public Enregistrement() { }

        public ImageSource GetImageSource()
        {
            return ImageSource.FromStream(() => new MemoryStream(Image));
        }

        public string GetPositionString()
        {
            if (Position != null)
                return Position.Latitude + " - " + Position.Longitude;
            else
                return "N/A";
        }

        public string GetAdresseString()
        {
            if(Adresse == null)
                return "";
            else
                return Adresse;
        }
    }
}
