using Microsoft.Azure.NotificationHubs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatFunctionApp.Services
{
    public class NotificationService
    {
        private static readonly string NotificationHubName = Environment.GetEnvironmentVariable("NotificationHubName");
        private static readonly string NotificationHubConnectionString = Environment.GetEnvironmentVariable("NotificationHubConnectionString");

        private NotificationHubClient hub;

        //ログを実装します。
        private readonly ILogger _logger;

        public NotificationService(ILogger<NotificationService> logger)
        {
            _logger = logger;

            //NotificationHubNameがからの場合は、例外を発生させる。
            if (string.IsNullOrEmpty(NotificationHubName))
            {
                throw new Exception("NotificationHubName is null or empty.");
            }
            //NotificationHubConnectionStringがからの場合は、例外を発生させる。
            if (string.IsNullOrEmpty(NotificationHubConnectionString))
            {
                throw new Exception("NotificationHubConnectionString is null or empty.");
            }


            //
            hub = NotificationHubClient.CreateClientFromConnectionString(NotificationHubConnectionString, NotificationHubName, true);



        }
        public async Task SendNotificationAsync(string message, string tag = null)
        {
            //var androidPayload = "{ \"data\" : {\"message\":\"" + message + "\"}}";

            message = "ok";
            var androidPayload = "{ \"notification\" : {\"title\" : \"Notification Hub Test Notification\", \"body\":\"テストメッセージです。\"},     \"data\" : {\"message\":\"" + message + "\"}}";

            var applePayload = "{ \"aps\" : {\"alert\":\"" + message + "\"}}";

            // Windows (UWP) notification payload
            var windowsPayload = @"<toast><visual><binding template=""ToastText01""><text id=""1"">" +
                                    message + "</text></binding></visual></toast>";

            _logger.LogInformation($"SendNotificationAsync 実施前: {message}");

            try
            {
                if (string.IsNullOrEmpty(tag))
                {
                    // ブロードキャスト通知
                    await hub.SendFcmNativeNotificationAsync(androidPayload);
                    //TOOD:iOS,UWPの実装
                    //await hub.SendAppleNativeNotificationAsync(applePayload);
                    //await hub.SendWindowsNativeNotificationAsync(windowsPayload);
                }
                else
                {
                    // タグ付き通知
                    await hub.SendFcmNativeNotificationAsync(androidPayload, tag);
                    //TODO: iOS, UWPの実装
                    //await hub.SendAppleNativeNotificationAsync(applePayload, tag);
                    //await hub.SendWindowsNativeNotificationAsync(windowsPayload, tag);
                }

            }
            catch (Exception ex)
            {
                //エラーログを出力する。
                _logger.LogError($"Error Message:{ex.ToString()}");
                throw;
            }
            _logger.LogInformation($"SendNotificationAsync 実施後: {message}");
        }
    }
}


