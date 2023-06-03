using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using ChatApp.Models;
using System.Collections.ObjectModel;
using ChatApp.Services;

namespace ChatApp.ViewModels
{
    public class EntryViewModel : ViewModelBase
    {
        ObservableCollection<User> _users;
        ObservableCollection<Color> _colors;


        //コンストラクタ
        public EntryViewModel()
        {
            //RegisterCommand = new Command(Register, CanRegister);

            //モデルをロードするLoadData();を呼び出す
            LoadData();
        }

        private void LoadData()
        {
            Users = new ObservableCollection<User>(EntryDataServcice.Instance.GetUsers());
            Colors = new ObservableCollection<Color>(EntryDataServcice.Instance.GetColors());
        }

        /// <summary>
        /// ユーザアイコンの選択の初期設定データ
        /// </summary>
        public ObservableCollection<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// カラー選択の初期設定データ
        /// </summary>
        public ObservableCollection<Color> Colors
        {
            get { return _colors; }
            set
            {
                _colors = value;
                OnPropertyChanged();
            }
        }

        //ユーザが選択したデータを保持するプロパティ
        private string _name;
        private string _image;
        private Color _color;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }

        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                OnPropertyChanged();
            }
        }


        public ICommand RegisterCommand => new Command(Register);


        private void Register()
        {
            if (string.IsNullOrEmpty(Name) ||
                   string.IsNullOrEmpty(Image) ||
                   Color == new Color())
            {
                return;
            }


            var user = new User
            {
                Name = Name,
                Image = Image,
                Color = Color
            };

            //userをJsonに変換する。
            var userJson = JsonConvert.SerializeObject(user);

            Preferences.Set("User", userJson);

            //Navigate to HomeView
            NavigationService.Instance.NavigateToAsync<HomeViewModel>();

        }


        //BoxViewをタップしたときに呼び出されるコマンド
        public ICommand UserSelectedCommand => new Command<object>(OnUserSelectedCommand);
        void OnUserSelectedCommand(object sender)
        {
            var boxView = sender as BoxView;
            Color = boxView.Color;
        }

        //SelectUserCommandを呼び出すコマンド
        public ICommand SelectUserCommand => new Command<object>(OnSelectUserCommand);
        void OnSelectUserCommand(object sender)
        {
            var user = sender as User;
            Image = user.Image;
        }

        //SelectColorCommandを呼び出すコマンド
        public ICommand SelectColorCommand => new Command<object>(OnSelectColorCommand);
        void OnSelectColorCommand(object sender)
        {
            var color = sender as Color?;
            Color = color.Value;
        }
    }

}
