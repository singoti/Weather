using System.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Meetum.Views
{
    public class RootPage : MasterDetailPage
    {
        MainPage displayPage;
        OptionItem previousItem;

        public RootPage ()
        {

            var optionsPage = new MenuPage { Icon = "settings.png" };
            optionsPage.Menu.ItemSelected += (sender, e) => NavigateTo(e.Data as OptionItem);

            Master = optionsPage;

            NavigateTo(optionsPage.Menu.ItemSource.Cast<OptionItem>().First());
        }

        public void LoadData()
        {
            displayPage.LoadData();
        }

        void NavigateTo (OptionItem option)
        {
            if (previousItem != null)
                previousItem.Selected = false;

            option.Selected = true;
            previousItem = option;

            displayPage = new MainPage { Title = option.Title };

            Detail = new NavigationPage(displayPage) {
                Tint = Color.FromHex("5AA09B"),               
            };

            IsPresented = false;
        }
    }
}

