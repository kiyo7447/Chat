using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsAzure.Messaging;
using Xamarin.Forms;

namespace ChatApp.Droid.Services
{
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    [Service(Name = "com.yourcompany.yourapp.MyFirebaseMessagingService", Exported = false), IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]

    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        public override void OnNewToken(string p0)
        {
            base.OnNewToken(p0);

            //こいつは動かない。Subscribe to the "SetTags" message
/*
            MessagingCenter.Subscribe<App, List<string>>(this, "SetTags", (sender, tags) =>
            {
                // Register the device with Azure Notification Hub
                using NotificationHub hub = new NotificationHub("OpenAIChat", "Endpoint=sb://chatnotificationhub.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=yNKdiFV8LXCX6bDg/hZmWdjZdO9RVKSIkZf8jES1ajQ=", this);
                Registration registration = hub.Register(p0, tags.ToArray());
            });
*/

            SendRegistrationToServer(p0);

            


        }
        private void SendRegistrationToServer(string token)
        {
            //            using NotificationHub hub = new NotificationHub("<hub-name>", "<connection-string>", this);

            //Azure Notification Hubの名前
            //DefaultFullSharedAccessSignatureの接続文字列
            using NotificationHub hub = new NotificationHub("OpenAIChat", "Endpoint=sb://chatnotificationhub.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=yNKdiFV8LXCX6bDg/hZmWdjZdO9RVKSIkZf8jES1ajQ=", this);

            // 複数のタグをリストに設定
            // これは通知時にタグ式を使ってPush通知を行う。
            //
            List<string> tags = new List<string>() { "uid-333", "did-123", "lcd-niigata", "fov-deai", "age-30" };

            // register device with Azure Notification Hub using the token from FCM
            //Registration registration = hub.Register(token, "<tag>");
            Registration registration = hub.Register(token, tags.ToArray());

        }

        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);

            // プッシュ通知の受信処理
            var messageBody = message.GetNotification()?.Body;
            MessagingCenter.Send<App, string>(App.Current as App, "ReceivedNotification", messageBody);
        }
    }

}

