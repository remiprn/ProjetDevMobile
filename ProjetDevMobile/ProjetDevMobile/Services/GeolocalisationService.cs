using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace ProjetDevMobile.Services
{
    public class GeolocalisationService: IGeolocalisationService
    {
        private IGeolocator _locator;
        private Geocoder _geocoder;

        public GeolocalisationService()
        {
            _locator = CrossGeolocator.Current;
            _locator.DesiredAccuracy = 100;

            _geocoder = new Geocoder();
        }

        public bool IsLocationAvailable()
        {
            if (!CrossGeolocator.IsSupported)
                return false;

            return _locator.IsGeolocationAvailable;
        }

        public async Task<Plugin.Geolocator.Abstractions.Position> GetCurrentLocation()
        {
            Plugin.Geolocator.Abstractions.Position position = null;
            try
            {
                position = await _locator.GetLastKnownLocationAsync();

                if (position != null)
                {
                    //got a cahched position, so let's use it.
                    return position;
                }

                if (!IsLocationAvailable() || !_locator.IsGeolocationEnabled)
                {
                    //not available or enabled
                    return null;
                }

                position = await _locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to get location: " + ex);
            }

            if (position == null)
                return null;

            var output = string.Format("Time: {0} \nLat: {1} \nLong: {2} \nAltitude: {3} \nAltitude Accuracy: {4} \nAccuracy: {5} \nHeading: {6} \nSpeed: {7}",
                    position.Timestamp, position.Latitude, position.Longitude,
                    position.Altitude, position.AltitudeAccuracy, position.Accuracy, position.Heading, position.Speed);

            Debug.WriteLine(output);

            return position;
        }

        public async Task<string> GetAddressForPositionAsync(Xamarin.Forms.Maps.Position position)
        {
            string address = null;
            try
            {
                //IEnumerable<string> addresses = await _geocoder.GetAddressesForPositionAsync(position);
                //address = addresses.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to get address: " + ex);
            }

            if (address == null)
                Console.WriteLine("No address found for position.");
            else
                Console.WriteLine("Address: {0}", address);

            address = "Adresse mockée !";

            return address;
        }
    }
}
