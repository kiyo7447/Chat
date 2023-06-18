using ChatApp.Models;
using ChatApp.Services;
using DataModel;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
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

        //検索コマンド
        public ICommand SearchCommand => new Command(OnSearch);



        //メッセージを送信するコマンド
        public ICommand SendMessageCommand => new Command(OnSendMessageCommand);

        //TELをかけるコマンド
        public ICommand CallTelCommand => new Command(OnCallTelCommand);

        public ICommand CallVideoCommand => new Command(OnCallVideoCommand);

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

        async void OnSendMessageCommand()
        {
            //入力したメッセージが空の場合は何もしない
            if (string.IsNullOrEmpty(_entiryMessage))
                return;

            //データ送信
            var retMessage = await PostDataToAzureFunctionAsync(new MyChat { MyGuid = "ok", TalkGuid ="aaa", Property1 = _entiryMessage, Property2 = 30 });





            _messages.Add(new Message { Sender = _user, Text = retMessage, SendDateTime = DateTime.Now });

            _messages.Add(new Message { Sender = null, Text = _entiryMessage, SendDateTime = DateTime.Now });
            //NavigationService.Instance.NavigateBackAsync();




            //入力したメッセージをクリアする
            EntryMessage = string.Empty;

            //表示後に画面をスクロースする
            MessageAdded?.Invoke();

        }

        private void OnSearch(object obj)
        {
            Application.Current.MainPage.DisplayAlert("確認", "この機能は有料会員、またはポイントを使用します。", "OK");
        }
        private void OnCallTelCommand(object obj)
        {
            //「この機能は有料会員、またはポイントを使用します。」とポップアップのメッセージを表示する。
            Application.Current.MainPage.DisplayAlert("確認", "この機能は有料会員、またはポイントを使用します。", "OK");
        }

        private void OnCallVideoCommand(object obj)
        {
            //ポップアップメッセージを表示する。
            Application.Current.MainPage.DisplayAlert("確認", "この機能は有料会員、またはポイントを使用します。", "OK");
        }

        public async Task<string> PostDataToAzureFunctionAsync(MyChat data)
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync("https://chatfunctionapp20230617.azurewebsites.net/api/ReceiveMessageFunction?code=oLKFCfXysKcpotCklMeZO47X8U9RL0yu6hndEr5nrnvdAzFun2wNLg==", content);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    // レスポンスボディを読み込む
                    Debug.WriteLine($"Response from Azure Function: {responseString}");
                    return responseString;
                }
                else
                {
                    Debug.WriteLine($"Failed to post data: {response.StatusCode}");
                    return $"Failed to post data: {response.StatusCode}";
                    //throw new SystemException($"Failed to post data: {response.StatusCode}");
                }
            }
        }
    }
}