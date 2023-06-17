using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using System.Collections.Generic;
using Xamarin.Forms;
using ChatApp.Services;
using ChatApp.Droid.Services;
using Firebase.Iid;
using WindowsAzure.Messaging;
using System.Threading.Tasks;

namespace ChatApp.Droid
{
    [Activity(Label = "ChatApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Push通知のセットアップ（Andorid版）
            var ap = Firebase.FirebaseApp.InitializeApp(Application);

            //通知関連のサービス
            DependencyService.Register<INotificationService, FirebaseNotificationService>();

            DependencyService.Register<IDeviceService, AndroidDeviceService>(); 


            /*

            //Push通知のサービスをセットアップ（Andorid版）
            //Xamarin.Forms.DependencyService.Register<INotificationService, MyFirebaseMessagingService>();
            //[assembly: Dependency(typeof(MyFirebaseMessagingService))]
            //で対応
            string token = FirebaseInstanceId.Instance.Token;

            using NotificationHub hub = new NotificationHub("OpenAIChat", "Endpoint=sb://chatnotificationhub.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=yNKdiFV8LXCX6bDg/hZmWdjZdO9RVKSIkZf8jES1ajQ=", this);

            // 新しいタグのリストを作成します
            List<string> newTags = new List<string>() { "newTag1", "newTag2", "newTag3" };

            if (token != null)
            {
                // 更新された Registration を Azure Notification Hub に送信します
                await Task.Run(() => hub.Register(token, newTags.ToArray()));
            }

            */


            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}