using System.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;
using System;

namespace Meetum.Views
{
    public class RootPage : MasterDetailPage
    {
        Page displayPage;
        OptionItem previousItem;

        public RootPage ()
        {

            var optionsPage = new MenuPage { Icon = "settings.png" };
            optionsPage.Menu.ItemSelected += (sender, e) => NavigateTo(e.Data as OptionItem);

            Master = optionsPage;

            NavigateTo(optionsPage.Menu.ItemSource.Cast<OptionItem>().First());
        }

//        public void LoadData()
//        {
//            if (displayPage is MainPage)
//                ((MainPage)displayPage).LoadData();
//        }

        void NavigateTo (OptionItem option)
        {
            if (previousItem != null)
                previousItem.Selected = false;

            option.Selected = true;
            previousItem = option;

            if (!option.Title.Equals("Dashboard")) {
                displayPage = new ContentPage { Title = option.Title, Content = new Label { Text = "FOOOOOOOOO" + new Random().Next() } };
            } else {
                displayPage = new MainPage { Title = option.Title };
            }

            var navPage = new NavigationPage(displayPage) {
                Tint = Color.FromHex("5AA09B")
            };

            Detail = navPage;

            IsPresented = false;
        }
    }
}

