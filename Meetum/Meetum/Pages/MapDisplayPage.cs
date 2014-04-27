using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace Meetum.Views
{
    public class MapDisplayPage : TabbedPage
    {
        public MapDisplayPage()
        {
            BackgroundColor = Color.Black;

            BindingContext = new {
                Tab1 = new TabItem { Title = "Store Map", Icon = "map.png" },
                Tab2 = new TabItem { Title = "Store List", Icon = "list.png" }
            };
        }

        public void LoadData()
        {
            Children.Add(CreateMapTab());
            Children.Add(CreateListTab());
        }

        Page CreateMapTab ()
        {
            var page = new ContentPage();
            page.Content = CustomerMapFactory.InitializeMap(page);

            page.SetBinding(BindableObject.BindingContextProperty, "Tab1");
            page.SetBinding(Page.TitleProperty, "Title");
            page.SetBinding(Page.IconProperty, "Icon");

            return page;
        }

        Page CreateListTab ()
        {
            var page = new ContentPage();
            page.Content = CustomerMapFactory.InitalizeList(page);

            page.SetBinding(BindableObject.BindingContextProperty, "Tab2");
            page.SetBinding(Page.TitleProperty, "Title");
            page.SetBinding(Page.IconProperty, "Icon");

            return page;
        }
    }
}
