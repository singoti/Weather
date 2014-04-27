using Android.App;
using Android.OS;
using Xamarin.Forms;
using Xamarin;
using Meetum.Views;

namespace Meetum.Android
{
    [Activity (Label = "Meetum", MainLauncher = true)]
    public class MainActivity : Xamarin.Forms.Platform.Android.AndroidActivity
    {
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            Forms.Init(this, bundle);
            FormsMaps.Init(this, bundle);
            Meetum.Init(typeof(Meetum).Assembly);

            // Set our view from the "main" layout resource
            SetPage (BuildView());
        }

        static Page BuildView()
        {
            return new SearchPage();
        }
    }
}


