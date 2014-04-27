using System.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Meetum.Views
{
    public class SearchPage : MasterDetailPage
    {
        MapDisplayPage displayPage;
        OptionItem previousItem;

        public SearchPage ()
        {

            var optionsPage = new MapOptionsPage { Icon = "settings.png" };
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

            displayPage = new MapDisplayPage { Title = option.Title };

            Detail = new NavigationPage(displayPage) {
                Tint = Color.FromHex("5AA09B"),               
            };

            IsPresented = false;
        }
    }
}

