using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace ProjetDevMobile.Models
{
    public class Enregistrement
    {
        public enum ETag { Drink,Food,ToSee}

        public int Id { get; set; }
        public String Nom { get; set; }
        public String Description { get; set; }
        public String Tag { get; set; }
        public byte[] Image { get; set; }
        public DateTime Date { get; set; }
        public Position Position { get; set; }

        public Enregistrement(String nom, String description, String tag, byte[] image, DateTime date, Position position)
        {
            Nom = nom;
            Description = description;
            Tag = tag;
            Image = image;
            Date = date;
            Position = position;
        }

        public Enregistrement() { }

        public ImageSource GetImageSource()
        {
            return ImageSource.FromStream(() => new MemoryStream(Image));
        }
    }
}
