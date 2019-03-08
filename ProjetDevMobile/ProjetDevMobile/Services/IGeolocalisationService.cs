using Plugin.Geolocator.Abstractions;
using Xamarin.Forms.Maps;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjetDevMobile.Services
{
    public interface IGeolocalisationService
    {
        bool IsLocationAvailable();
        Task<Plugin.Geolocator.Abstractions.Position> GetCurrentLocation();
        Task<string> GetAddressForPositionAsync(Xamarin.Forms.Maps.Position position);
    }
}
