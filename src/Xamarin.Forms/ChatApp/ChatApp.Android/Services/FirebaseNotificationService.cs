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
    internal class FirebaseNotificationService : INotificationService
    {
        public void Initialize()
        {
            //何もしない
        }

        [Obsolete]
        public async void SetTags(string[] tags)
        {
            //Push通知のサービスをセットアップ（Andorid版）
            //Xamarin.Forms.DependencyService.Register<INotificationService, MyFirebaseMessagingService>();
            //[assembly: Dependency(typeof(MyFirebaseMessagingService))]
            //で対応
            string token = FirebaseInstanceId.Instance.Token;

            using NotificationHub hub = new NotificationHub("OpenAIChat", "Endpoint=sb://chatnotificationhub.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=yNKdiFV8LXCX6bDg/hZmWdjZdO9RVKSIkZf8jES1ajQ=", Android.App.Application.Context);

            if (token != null)
            {
                // 更新された Registration を Azure Notification Hub に送信します
                //hub.Register(token, newTags.ToArray());
                await System.Threading.Tasks.Task.Run(() => hub.Register(token, tags));
            }


        }
    }
}