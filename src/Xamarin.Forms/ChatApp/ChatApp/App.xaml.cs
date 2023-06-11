using ChatApp.Services;
using ChatApp.ViewModels;
using ChatApp.Views;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;

namespace ChatApp
{
    public partial class App : Xamarin.Forms.Application
    {
        // 通知のカウンター（インスタンスの
        int counter = 0;
        public App()
        {
            Current.On<Windows>().SetImageDirectory("Assets");

            InitializeComponent();

            //Xamarin.Essentials.Preferences.Clear();


            //各OSごとのインスタンスを取得する。
            //var notificationService = DependencyService.Get<NotificationService>();


            // DependencyService.Get<IAnimatable>();

            // Set the tags
            //List<string> tags = new List<string>() { "uid-444", "did-123", "lcd-niigata", "fov-deai", "age-30" };

            // Send the tags to the Android project via MessagingCenter
            //MessagingCenter.Send<App, List<string>>(this, "SetTags", tags);


            Debug.WriteLine($"RuntimePlatform:{Device.RuntimePlatform}");



            //Push通知を受け取る
            // Android:○
            // iOS:未検証
            // Windows:検証予定なし
            MessagingCenter.Subscribe<App, string>(this,  "ReceivedNotification", (sender, arg) =>
            {
                counter++;
                //メッセージを受け取ったら、通知を表示する
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await MainPage.DisplayAlert($"通知:{counter}", arg, "OK");
                });

                // ここで通知メッセージを受け取ります。
                Debug.WriteLine($"Received Notification: {arg}");
            });


            // 後でMainPageを切り替えるときは、NavigationSerivceを使う
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
        /*
        public void SetTags(List<string> tags)
        {
            // Set the tags
            //List<string> tags = new List<string>() { "uid-123", "did-123", "lcd-niigata", "fov-deai", "age-30" };

            // Send the tags to the Android project via MessagingCenter
            MessagingCenter.Send<App, List<string>>(this, "SetTags", tags);
        }
        */
    }
}
