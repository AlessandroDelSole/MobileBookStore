using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using MobileBookStore.DataAccess;
using Xamarin.Forms;

namespace MobileBookStore
{
    public partial class App : Application
    {

        internal static DataManager DataManager;

        public App()
        {
            InitializeComponent();
            MobileCenter.Start(typeof(Analytics), typeof(Crashes));

            Xamarin.Forms.Device.OnPlatform(Android: () => Constants.BaseUrl = "https://your-android-service-url.azurewebsites.net/",
                                            iOS: ()=> Constants.BaseUrl= "https://your-ios-service-url.azurewebsites.net/");

            DataManager = new DataManager(Constants.BaseUrl);

            MainPage = new MobileBookStore.MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
