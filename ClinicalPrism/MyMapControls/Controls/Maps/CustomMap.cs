using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace ControlExamples.Controls.Maps
{
    public class CustomMap : Map
    {
        public static readonly BindableProperty CenterRegionProperty = BindableProperty.Create("CenterRegion", typeof(MapSpan), typeof(CustomMap), null,propertyChanged: CenterRegionChanged);

        private static void CenterRegionChanged(BindableObject sender, object oldValue, object newValue)
        {
            CustomMap map = (CustomMap)sender;

            if (newValue is MapSpan location)
            {
                map.MoveToRegion(location);
            }
        }

        public MapSpan CenterRegion
        {
            get { return (MapSpan)GetValue(CenterRegionProperty); }
            set { SetValue(CenterRegionProperty, value); }
        }


    }
}
