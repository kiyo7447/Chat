using ChatApp.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ChatApp.Services
{
    /// <summary>
    /// 初期セットアップ用のでデータを提供するサービス
    /// </summary>
    public class EntryDataServcice
    {
        static EntryDataServcice _instance;

        public static EntryDataServcice Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new EntryDataServcice();

                return _instance;
            }
        }

        readonly User user1 = new User
        {
            Name = "Alaya Cordova",
            Image = "emoji1.png",
            //Color = Color.FromHex("#FFE0EC")
        };
        readonly User user2 = new User()
        {
            Name = "Cecily Trujillo",
            Image = "emoji2.png",
            //Color = Color.FromHex("#BFE9F2")
        };
        readonly User user3 = new User()
        {
            Name = "Eathan Sheridan",
            Image = "emoji3.png",
            //Color = Color.FromHex("#FFD6C4")
        };
        readonly User user4 = new User()
        {
            Name = "Komal Orr",
            Image = "emoji4.png",
            Color = Color.FromHex("#C3C1E6")
        };
        readonly User user5 = new User()
        {
            Name = "Sariba Abood",
            Image = "emoji5.png",
            Color = Color.FromHex("#FFE0EC")
        };
        readonly User user6 = new User()
        {
            Name = "Justin O'Moore",
            Image = "emoji6.png",
            Color = Color.FromHex("#FFE5A6")
        };
        readonly User user7 = new User()
        {
            Name = "Alissia Shah",
            Image = "emoji7.png",
            Color = Color.FromHex("#FFE0EC")
        };
        readonly User user8 = new User()
        {
            Name = "Antoni Whitney",
            Image = "emoji8.png",
            Color = Color.FromHex("#FFE0EC")
        };
        readonly User user9 = new User()
        {
            Name = "Jaime Zuniga",
            Image = "emoji9.png",
            Color = Color.FromHex("#C3C1E6")
        };
        readonly User user10 = new User()
        {
            Name = "Barbara Cherry",
            Image = "emoji10.png",
            Color = Color.FromHex("#FF95A2")
        };

        public List<Color> GetColors()
        {
            return new List<Color>
            {
                Color.FromHex("#FFE0EC"),
                Color.FromHex("#BFE9F2"),
                Color.FromHex("#FFD6C4"),
                Color.FromHex("#C3C1E6"),
                Color.FromHex("#FFE5A6"),
                Color.FromHex("#FF95A2"),
            };
        }

        public List<User> GetUsers()
        {
            return new List<User>
            {
                user1, user2, user3, user4, user5, user6, user7, user8, user9, user10
            };
        }
        /*
        public List<Message> GetChats()
        {
            return new List<Message>
            {
                new Message
                {
                  Sender = user6,
                  //SendDateTimeに今日の日付で時間を18:32でデータを設定する。
                  SendDateTime = DateTime.Now,
                  Text = "Hey there! What\'s up? Is everything ok?",
                },
              new Message
              {
                Sender = user1,
                  SendDateTime = DateTime.Now,
                Text = "Can I call you back later?, I\'m in a meeting.",
              },
              new Message
              {
                Sender = user3,
                  SendDateTime = DateTime.Now,
                Text = "Yeah. Do you have any good song to recommend?",
              },
              new Message
              {
                Sender = user2,
                  SendDateTime = DateTime.Now,
                Text = "Hi! I went shopping today and found a nice t-shirt.",
              },
              new Message
              {
                Sender = user4,
                  SendDateTime = DateTime.Now,
                Text= "I passed you on the ride to work today, see you later.",
              },
            };
        }

        public List<Message> GetMessages(User sender)
        {
            return new List<Message> {
              new Message
              {
                Sender = sender,
                  SendDateTime = DateTime.Now.AddDays(-2),
                Text= "Hey there! What\'s up?",
              },
              new Message
              {
                Sender = null,
                  SendDateTime = DateTime.Now.AddDays(-2),
                Text = "Nothing. Just chilling and watching YouTube. What about you?",
              },
              new Message
              {
                Sender = sender,
                  SendDateTime = DateTime.Now.AddDays(-1),
                Text =
                    "Same here! Been watching YouTube for the past 5 hours despite of having so much to do! 😅",
              },              
              new Message
              {
                Sender = sender,
                  SendDateTime = DateTime.Now.AddDays(-1),
                Text = "It\'s hard to be productive, man 😞",
              },
              new Message
              {
                Sender = null,
                  SendDateTime = DateTime.Now,
                Text = "Yeah I know. I\'m in the same position 😂",
              },
            };
        }
        */
    }
}