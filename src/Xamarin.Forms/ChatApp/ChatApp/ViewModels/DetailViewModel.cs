using ChatApp.Models;
using ChatApp.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChatApp.ViewModels
{
    public class DetailViewModel : ViewModelBase
    {
        User _user;
        string _entiryMessage;
        ObservableCollection<Message> _messages;

        public event Action MessageAdded;

        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }

        public string EntryMessage
        {
            get { return _entiryMessage; }
            set
            {
                if (_entiryMessage != value)
                {
                    _entiryMessage = value;
                    OnPropertyChanged(nameof(EntryMessage));
                }
            }
        }

        // INotifyPropertyChangedの実装
        //public event PropertyChangedEventHandler PropertyChanged;
        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        public ObservableCollection<Message> Messages
        {
            get { return _messages; }
            set
            {
                _messages = value;
                OnPropertyChanged();
            }
        }

        public ICommand BackCommand => new Command(OnBack);

        public ICommand SendMessageCommand => new Command(OnSendMessageCommand);

        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData is Message message)
            {
                User = message.Sender;
                Messages = new ObservableCollection<Message>(MessageService.Instance.GetMessages(User));
            }

            return base.InitializeAsync(navigationData);
        }

        void OnBack()
        {
            _ = NavigationService.Instance.NavigateBackAsync();
        }

        void OnSendMessageCommand()
        {
            //入力したメッセージが空の場合は何もしない
            if (string.IsNullOrEmpty(_entiryMessage))
                return;

            _messages.Add(new Message { Sender = _user, Text = "かずま", SendDateTime = DateTime.Now });

            _messages.Add(new Message { Sender = null, Text = _entiryMessage, SendDateTime = DateTime.Now });
            //NavigationService.Instance.NavigateBackAsync();

            //入力したメッセージをクリアする
            EntryMessage = string.Empty;

            //表示後に画面をスクロースする
            MessageAdded?.Invoke();

         }
    }
}