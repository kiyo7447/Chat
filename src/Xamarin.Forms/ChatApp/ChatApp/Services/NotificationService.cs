using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ChatApp.Services
{
    /// <summary>
    /// Push通知用のサービス
    /// </summary>
    class NotificationService
    {
        static NotificationService _instance;


        public static NotificationService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new NotificationService();

                return _instance;
            }
        }


    }
}
