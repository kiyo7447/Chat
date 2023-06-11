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
using ChatApp.Services;
using Android.Gms.Tasks;
using AndroidX.AppCompat.App;
using Firebase.Iid;


namespace ChatApp.Droid.Services
{
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    [Service(Name = "com.yourcompany.yourapp.MyFirebaseMessagingService", Exported = false), IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    [assembly: Dependency(typeof(MyFirebaseMessagingService))]
    public class MyFirebaseMessagingService : FirebaseMessagingService 
    {
        //コンストラクタ
        public MyFirebaseMessagingService()
        {
            //何もしない
        }

        public override void OnNewToken(string token)
        {
            base.OnNewToken(token);

            //こいつは動かない。Subscribe to the "SetTags" message
            /*
                        MessagingCenter.Subscribe<App, List<string>>(this, "SetTags", (sender, tags) =>
                        {
                            // Register the device with Azure Notification Hub
                            using NotificationHub hub = new NotificationHub("OpenAIChat", "Endpoint=sb://chatnotificationhub.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=yNKdiFV8LXCX6bDg/hZmWdjZdO9RVKSIkZf8jES1ajQ=", this);
                            Registration registration = hub.Register(p0, tags.ToArray());
                        });
            */
            SendRegistrationToServer(token);

        }

        private void SendRegistrationToServer(string token)
        {
           

            //
            if (string.IsNullOrEmpty(token))
                return;
            //            using NotificationHub hub = new NotificationHub("<hub-name>", "<connection-string>", this);

            //Azure Notification Hubの名前
            //DefaultFullSharedAccessSignatureの接続文字列
            using NotificationHub hub = new NotificationHub("OpenAIChat", "Endpoint=sb://chatnotificationhub.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=yNKdiFV8LXCX6bDg/hZmWdjZdO9RVKSIkZf8jES1ajQ=", this);

            // 複数のタグをリストに設定
            // これは通知時にタグ式を使ってPush通知を行う。
            //
            List<string> tags = new List<string>() { "uid-none", "did-123", "lcd-niigata", "fov-deai", "age-30" };

            // register device with Azure Notification Hub using the token from FCM
            //Registration registration = hub.Register(token, "<tag>");
           
            //実機でこれがエラーになる

            Registration registration = hub.Register(token, tags.ToArray());

        }

        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);

            // プッシュ通知の受信処理
            var messageBody = message.GetNotification()?.Body;
            MessagingCenter.Send<App, string>(App.Current as App, "ReceivedNotification", messageBody);
        }

        //[Obsolete]
        //void INotificationService.Initialize()
        //{
            //using NotificationHub hub = new NotificationHub("OpenAIChat", "Endpoint=sb://chatnotificationhub.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=yNKdiFV8LXCX6bDg/hZmWdjZdO9RVKSIkZf8jES1ajQ=", this);

            //// 1. 現在の登録情報を取得します。
            //Registration oldRegistration =  hub.GetRegistrationAsync<Registration>(token);

            //if (oldRegistration != null)
            //{
            //    // 2. 新しいタグのリストを設定します。
            //    oldRegistration.Tags = new HashSet<string>(newTags);

            //    // 3. 更新された登録をAzure Notification Hubに送信します。
            //    await hub.UpdateRegistrationAsync(oldRegistration);
            //}



            //throw new NotImplementedException();
            //トークンを取得する

            //FirebaseMessagingからTokenを取得する。
            //var token = FirebaseInstanceId.Instance.Token;

            //Firebase.FirebaseApp.InitializeApp(Application.Context);

            //SendRegistrationToServer(token);

            //Firebase.Iid.FirebaseInstanceId.Instance.GetInstanceId().AddOnCompleteListener(new IOnCompleteListener());
            
        //}


    }
    //class a : AppCompatActivity,  IOnCompleteListener
    //{
    //    public void OnSuccess(Java.Lang.Object result)
    //    {
    //        var instanceIdResult = result.Class.Cast<InstanceIdResult>(result);
    //        string token = instanceIdResult.Token;
    //        // use the token as needed
    //    }
    //}
}

