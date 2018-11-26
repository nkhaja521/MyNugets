using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Xamarin.Forms.Maps;

namespace GI.Common.XF.Helpers
{
    public static class AddressHelper
    {
        public static double latti, longi;
        public static async Task<string> GetAddress(string CurrAddress, Position position)
        {
            try
            {
                Geocoder geo = new Geocoder();

                var addresses = await geo.GetAddressesForPositionAsync(new Position(position.Latitude, position.Longitude));
                CurrAddress = addresses.FirstOrDefault();

                return CurrAddress;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public static async Task<Position> GetCurrentLocation()
        {
            Position pos = new Position();
            var locator = CrossGeolocator.Current;
            try
            {
                if (IsLocationAvailable())
                {
                    locator.DesiredAccuracy = 50;
                    //locator.PositionChanged += Locator_PositionChanged;
                    //await locator.StartListeningAsync(TimeSpan.FromMinutes(0.06), 1, true);
                    var myPosition = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
                    pos = new Position(myPosition.Latitude, myPosition.Longitude);
                    return pos;
                }

                return pos;
            }
            catch (Exception ex)
            {
                var myPos = await locator.GetLastKnownLocationAsync();
                if (myPos != null)
                    pos = new Position(myPos.Latitude, myPos.Longitude);
                else
                    pos = new Position(0, 0);
                return pos;
            }

        }

        private static void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            latti = e.Position.Latitude;
            longi = e.Position.Longitude;
        }

        public static bool IsLocationAvailable()
        {
            if (!CrossGeolocator.IsSupported)
                return false;

            return CrossGeolocator.Current.IsGeolocationAvailable;
        }
    }
}
