﻿using ChatApp.Models;
using ChatApp.Services;
using DataModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChatApp.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        ObservableCollection<User> _users;
        ObservableCollection<Message> _recentChat;

        public HomeViewModel()
        {

            LoadData();


        }

        public ObservableCollection<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Message> RecentChat
        {
            get { return _recentChat; }
            set
            {
                _recentChat = value;
                OnPropertyChanged();
            }
        }

        public ICommand DetailCommand => new Command<object>(OnNavigate);

        void LoadData()
        {
            Users = new ObservableCollection<User>(MessageService.Instance.GetUsers());
            RecentChat = new ObservableCollection<Message>(MessageService.Instance.GetChats());

            //RecentChat.Add(new Message { Sender = sender, Text = "かずま", Time = "12:00" });
        }

         void OnNavigate(object parameter)
        {
            //List<string> tags = new List<string>() { "uid-999", "did-123", "lcd-niigata", "fov-deai", "age-30" };

            //((App)App.Current).SetTags(tags); ;

            //List<string> tags = new List<string>() { "uid-123", "did-123", "lcd-niigata", "fov-deai", "age-30" };

            // Send the tags to the Android project via MessagingCenter
            //MessagingCenter.Send<App, List<string>>(this, "SetTags", tags);

            // 通知サービスの初期化を実行する
            //DependencyService.Get<INotificationService>().Initialize();


            //Androidでは成功した
            var service = DependencyService.Get<INotificationService>();
            List<string> tags = new List<string>() { "uid-222", "did-123", "lcd-niigata", "fov-deai", "age-30" };
            service.SetTags(tags.ToArray());




            NavigationService.Instance.NavigateToAsync<DetailViewModel>(parameter);
        }


    }
}