using ChatApp.Services;
using ChatApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;

namespace ChatApp
{
    public partial class App : Xamarin.Forms.Application
    {
        public App()
        {
            Current.On<Windows>().SetImageDirectory("Assets");

            InitializeComponent();

            //DeviceServcieをDIコンテナに登録する
            DependencyService.Register<DeviceService>();

            if (Xamarin.Essentials.Preferences.ContainsKey("User"))
            {
                MainPage = new NavigationPage(new HomeView());
            }
            else
            {
                MainPage = new NavigationPage(new EntryView());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
