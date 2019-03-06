using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetDevMobile.Models
{
    public class Enregistrement
    {
        public enum ETag { Drink,Food,ToSee}

        public String Nom { get; set; }
        public String Description { get; set; }
        public String Tag { get; set; }
        public byte[] Image { get; set; }
        public DateTime Date { get; set; }

        public Enregistrement(String nom, String description, String tag, byte[] image, DateTime date)
        {
            Nom = nom;
            Description = description;
            Tag = tag;
            Image = image;
            Date = date;
        }

        public Enregistrement() { }
    }
}
