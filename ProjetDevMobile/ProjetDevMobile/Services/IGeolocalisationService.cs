using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjetDevMobile.Services
{
    public interface IGeolocalisationService
    {
        bool IsLocationAvailable();
        Task<Position> GetCurrentLocation();
        Task<Address> GetAddressForPositionAsync(Position position);
    }
}
