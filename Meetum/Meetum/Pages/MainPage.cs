using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace Meetum.Views
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            BackgroundColor = Color.Black;

            BindingContext = new {
                MapTab = new TabItem { Title = "Map", Icon = "map.png" },
                ListTab = new TabItem { Title = "List", Icon = "list.png" }
            };

            Children.Add(CreateMapTab());
            Children.Add(CreateListTab());
        }

        static Page CreateMapTab ()
        {
            var page = new ContentPage();
            page.Content = MapFactory.InitializeMap(page);

            page.SetBinding(BindableObject.BindingContextProperty, "MapTab");
            page.SetBinding(Page.TitleProperty, "Title");
            page.SetBinding(Page.IconProperty, "Icon");

            return page;
        }

        static Page CreateListTab ()
        {
            var page = new ContentPage();
            page.Content = MapFactory.InitalizeList(page);

            page.SetBinding(BindableObject.BindingContextProperty, "ListTab");
            page.SetBinding(Page.TitleProperty, "Title");
            page.SetBinding(Page.IconProperty, "Icon");

            return page;
        }
    }
}
