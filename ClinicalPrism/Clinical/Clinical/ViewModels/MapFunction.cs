using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Clinical.ViewModels
{
    class MapFunction
    {
        public async Task GetLocation(string address)
        {
            var placemark = new Placemark();
            placemark.Thoroughfare = address;
            await Map.OpenAsync(placemark);
        }
    }
}
