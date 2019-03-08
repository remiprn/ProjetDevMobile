using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ProjetDevMobile.Services
{
    class CalculeDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan diff = DateTime.Today.Subtract((DateTime)value);
            double nbJour = diff.TotalDays;
            int nbJourInt = System.Convert.ToInt32(nbJour);
            if(nbJourInt == 0)
            {
                return "Today";
            }
            else if(nbJourInt < 7)
            {
                return nbJourInt + " jours";
            }
            else
            {
                int nbSemaine = nbJourInt / 7;
                return nbSemaine + " semaines";
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
