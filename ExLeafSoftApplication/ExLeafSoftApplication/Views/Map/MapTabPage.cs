using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ExLeafSoftApplication.Views
{
    public class MapTabPage :TabbedPage
    {
        public MapTabPage()
        {
            Children.Add(new MapPage { Title = "Map/Zoom" });

            Title = "ShowMap";

            // opens the platform's native Map app
            Children.Add(new MapAppPage { Title = "Map App"});
            Children.Add(new PinPage { Title = "Pin App" });
            Children.Add(new GeoCoderPage { Title = "GeoCoder App" });
        }
      

    }
}
