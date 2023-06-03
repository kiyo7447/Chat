using System;


namespace ChatApp.Models
{
    public class Message
    {
        public User Sender { get; set; }

        //Role
        public Role Role { get; set; }

        public string Text { get; set; }
        public DateTime SendDateTime{ get; set; }

        //SendDateTimeDisplayの返す値は、当日は、"HH:mm"で返し、一日前は、"昨日 HH:mm"で返す、それ以外は、"MM/dd HH:mm"で返す。
        public string DisplaySendDateTime
        {
            get
            {
                if (SendDateTime.Date == DateTime.Now.Date)
                {
                    return SendDateTime.ToString("HH:mm");
                }
                else if (SendDateTime.Date == DateTime.Now.Date.AddDays(-1))
                {
                    return SendDateTime.ToString("昨日 HH:mm");
                }
                else
                {
                    return SendDateTime.ToString("MM/dd HH:mm");
                }
            }
        }

        

    }
}