using ChatApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//Modelsを追加
using ChatApp.Models;

namespace ChatApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryView : ContentPage
    {
        public EntryView()
        {
            InitializeComponent();

            //string[] emojiFilenames = { "emoji1.png", "emoji2.png", "emoji3.png", "emoji4.png", "emoji5.png", };
            //foreach (var filename in emojiFilenames)
            //{
            //    var image = new Image { Source = filename };
            //    var tapGestureRecognizer = new TapGestureRecognizer();
            //    tapGestureRecognizer.Tapped += (s, e) => { (BindingContext as EntryViewModel).Image = filename; };
            //    image.GestureRecognizers.Add(tapGestureRecognizer);

            //    EmojiGrid.Children.Add(image);
            //}

            //string[] colors = { "#FFE0EC", "#BFE9F2", /* ... */ };
            //foreach (var hexColor in colors)
            //{
            //    var boxView = new BoxView { Color = Color.FromHex(hexColor) };
            //    var tapGestureRecognizer = new TapGestureRecognizer();
            //    tapGestureRecognizer.Tapped += (s, e) => { (BindingContext as EntryViewModel).Color = Color.FromHex(hexColor); };
            //    boxView.GestureRecognizers.Add(tapGestureRecognizer);

            //    ColorGrid.Children.Add(boxView);
            //}
        }

    }
}