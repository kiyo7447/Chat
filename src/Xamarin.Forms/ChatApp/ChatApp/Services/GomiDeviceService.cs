using ChatApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;
//jsonを使うために追加 System.Text.Jsonは.NET6からでした。。。
using Newtonsoft.Json;


namespace ChatApp.Services
{


    public class GomiDeviceService
    {
        private const string DeviceIdKey = "DeviceId";

        public string GetDeviceId()
        {
            if (!Preferences.ContainsKey(DeviceIdKey))
            {
                var deviceId = System.Guid.NewGuid().ToString();
                Preferences.Set(DeviceIdKey, deviceId);
            }

            return Preferences.Get(DeviceIdKey, string.Empty);
        }

        // デバイスにChatDataというキーでデータ（オブジェクト）を保存する。
        public void SetChatData(ObservableCollection<Message> chatData)
        {
            //cahtDataをstringにシリアライズする。
            var json = JsonConvert.SerializeObject(chatData);

            Preferences.Set("ChatData", json);
        }





    }
}