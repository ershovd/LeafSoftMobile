using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace ExLeafSoftApplication.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapView : ContentView
	{
        Map map;
       public static readonly BindableProperty GpsPositionProperty = BindableProperty.Create("GpsPosition", typeof(string), 
           typeof(MapView),string.Empty);
       


        public string GpsPosition
        {
            get { return (string)GetValue(GpsPositionProperty); }
            set { SetValue(GpsPositionProperty, value); }
        }

        public MapView ()
		{
			InitializeComponent ();

           


            map = new Map
            {
                IsShowingUser = true,
                HeightRequest = 250,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            var current = new Button { Text = "Currrent Location" };
            current.Clicked += (sender, e) => {
                Device.BeginInvokeOnMainThread(() => A());
            };


            var buttons = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                
                Children = {
                   current
                }
            };

            // put the page together
            Content = new StackLayout
            {
                Spacing = 0,
                Children = {
                    map,
                    buttons
                }
            };
        }


        async void A()
        {
            var locator = CrossGeolocator.Current;
            var position1 = await locator.GetPositionAsync(TimeSpan.FromSeconds(1));
            var P = new Position(position1.Latitude, position1.Longitude);

            map.MoveToRegion(MapSpan.FromCenterAndRadius(P,
                                         Distance.FromMiles(1)));

            GpsPosition = position1.Latitude.ToString() + ";" + position1.Longitude.ToString();
            //var position = new Position(36.9628066, -122.0194722); // Latitude, Longitude
            var ap = new Pin
            {
                Type = PinType.Place,
                Position = P,
                Label = "Current Location",
                Address = "custom detail info"
            };
            map.Pins.Add(ap);


        }
    }
}