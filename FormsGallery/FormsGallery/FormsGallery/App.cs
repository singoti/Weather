using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace FormsGallery
{
    public class App
    {
        public static Page GetMainPage()
        {
			var profilePage = new ContentPage {
				Title = "Profile",
				Icon = "Profile.png",
				Content = new StackLayout {
					Spacing = 20, Padding = 50,
					VerticalOptions = LayoutOptions.Center,
					Children = {
						new Entry { Placeholder = "Username", HorizontalOptions = LayoutOptions.FillAndExpand },
						new Entry { Placeholder = "Password", HorizontalOptions = LayoutOptions.FillAndExpand },
						new Button {
							Text = "Login",
							TextColor = Color.White, BackgroundColor = Color.FromHex("77D065"),
						},
					}
				}
			};
			var settingsPage = new ContentPage { Title = "Settings", Icon = "Settings.png" };
			var mainPage = new TabbedPage { Children = { profilePage, settingsPage } };

			return mainPage;
        }
    }
}
