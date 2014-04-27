using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Linq;
using System.Diagnostics;
using Meetum.Controls;
using System.Collections.Generic;
using System.Resources;
using Meetum.Models;
using System.IO;
using System.Threading.Tasks;

namespace Meetum.Views
{
    public static class CustomerMapFactory
    {
        public static StackLayout InitalizeList (ContentPage parent)
        {
            var data = LoadData ();
            var list = new ListView();
            list.ItemSource = data;
            var cell = new DataTemplate(typeof(TextCell));
            cell.SetBinding(TextCell.TextProperty, "Labels[0].Value");
            cell.SetBinding(TextCell.DetailProperty, "Categories[0].Value");

            list.ItemTemplate = cell;

            var stack = new StackLayout();
            stack.Children.Add(list);

            return stack;
        }

        public static StackLayout InitializeMap (ContentPage parent)
        {
            var map = MakeMap ();
            map.HasScrollEnabled = true;
            map.HasZoomEnabled = false;

            var searchAddress = new SearchBar { Placeholder = "Search Address" };

            searchAddress.SearchButtonPressed += async (e, a) => {
                var addressQuery = searchAddress.Text;
                searchAddress.Text = "";
                searchAddress.Unfocus ();

                var positions = (await (new Geocoder ()).GetPositionsForAddressAsync (addressQuery)).ToList ();
                if (!positions.Any ())
                    return;

                var position = positions [0];
                map.MoveToRegion (MapSpan.FromCenterAndRadius (position,
                    Distance.FromMeters (4000)));
                map.Pins.Add (new Pin {
                    Label = addressQuery,
                    Position = position,
                    Address = addressQuery
                });
            };

            var buttonAddressFromPosition = new Button { Text = "Address From Position", TextColor = Color.White };
            buttonAddressFromPosition.Clicked += async (e, a) => {
                var addresses = (await (new Geocoder ()).GetAddressesForPositionAsync (new Position (41.8902, 12.4923))).ToList ();
                foreach (var ad in addresses)
                    Debug.WriteLine (ad);
            };

            //Device.BeginInvokeOnMainThread(() => this.Title = "Thread Safe");

            parent.ToolbarItems.Add(new ToolbarItem("Filter", "filter.png", () => {}));

            var buttonZoomIn = new Button { Text = "Zoom In", TextColor = Color.White };
            buttonZoomIn.Clicked += (e, a) => map.MoveToRegion (map.VisibleRegion.WithZoom (5f));

            var buttonZoomOut = new Button { Text = "Zoom Out", TextColor = Color.White };
            buttonZoomOut.Clicked += (e, a) => map.MoveToRegion (map.VisibleRegion.WithZoom (1 / 3f));

            var mapTypeButton = new Button { Text = "Map Type", TextColor = Color.White };
            mapTypeButton.Clicked += async (e, a) => {
                var result = await parent.DisplayActionSheet ("Select Map Type", null, null, "Street", "Satellite", "Hybrid");
                switch (result) {
                case "Street":
                    map.MapType = MapType.Street;
                    break;
                case "Satellite":
                    map.MapType = MapType.Satellite;
                    break;
                case "Hybrid":
                    map.MapType = MapType.Hybrid;
                    break;
                }
            };

            var stack = new StackLayout { Spacing = 0, BackgroundColor = Color.FromHex("A19887")};

            map.VerticalOptions = LayoutOptions.FillAndExpand;
            map.HeightRequest = 100;
            map.WidthRequest = 960;
            stack.Children.Add (searchAddress);
            stack.Children.Add (map);

            var buttonStack = new StackLayout { 
                Orientation = StackOrientation.Horizontal,
                Spacing = 30,
                Padding = new Thickness(20, 0, 20, 0)
            };

            buttonStack.Children.Add (mapTypeButton);
            buttonStack.Children.Add (buttonZoomIn);
            buttonStack.Children.Add (buttonZoomOut);
            buttonStack.Children.Add (buttonAddressFromPosition);

            // Wrap in a horizonal scroll view to handle small screens.
            stack.Children.Add(new ScrollView { Content = buttonStack, HeightRequest = 44, Orientation = ScrollView.ScrollOrientation.Horizontal });

            return stack;
        }

        static List<POI> LoadData ()
        {
            if (Meetum.PointsOfInterest != null) return Meetum.PointsOfInterest;

            var jsonStream = Meetum.LoadResource ("Meetum.Models.Poi.json");
            TestData data = null;
            using (var jsonReader = new StreamReader (jsonStream)) {
                var json = jsonReader.ReadToEnd ();
                data = global::Newtonsoft.Json.JsonConvert.DeserializeObject<TestData> (json);
            }
            Meetum.PointsOfInterest = data.PointsOfInterest;

            return data.PointsOfInterest;
        }

        public static Map MakeMap ()
        {
            var data = LoadData ();

            var pins = data.Select(p => {
                var pos = p.Location.Points[0];
                var poslist = pos.Poslist.Split(' ');
                var pin = new Pin {
                    Type = PinType.Place,
                    Position = new Position (Convert.ToDouble(poslist[0]), Convert.ToDouble(poslist[1])),
                    Label = p.Labels[0].Value,
                    Address = (String)p.Location.Address ?? (String)p.Location.Value ?? (String)p.Location.Points[0].Value
                };
                return pin;
            }).ToList();

            var xamarin = new Position(37.797536, -122.401933);;
            var m = new MyMap(MapSpan.FromCenterAndRadius(xamarin, Distance.FromMiles(0.1)));

            foreach(var p in pins) 
            {
                m.Pins.Add(p);
            }

            return m;
        }
    }
}

