using System.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Forms.Maps;

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

            ShowLoginDialog();

            NavigateTo(optionsPage.Menu.ItemSource.Cast<OptionItem>().First());
        }

        async void ShowLoginDialog ()
        {
            var layout = new StackLayout();
            var label = new Label 
            {
                Text = "Connect with Your Data",
                Font = Font.BoldSystemFontOfSize(NamedSize.Large),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                XAlign = TextAlignment.Center, // Center the text in the blue box.
                YAlign = TextAlignment.Center, // Center the text in the blue box.
            };

            layout.Children.Add(label);

            var username = new Entry() { Placeholder = "Username" };
            layout.Children.Add(username);

            var password = new Entry() { Placeholder = "Password" };
            layout.Children.Add(password);

            var page = new ContentPage 
            {
                Content = layout
            };

            var button = new Button() { Text = "Sign In"};
            button.Clicked += async (sender, e) =>
            {
                await Navigation.PopModal();
                Debug.WriteLine(username.Text);
                Debug.WriteLine(password.Text);
            };
            layout.Children.Add(button);

            await Navigation.PushModal(page);
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

