using ChatApp.ViewModels;
using Xamarin.Forms;

namespace ChatApp.Views
{
    public partial class DetailView : ContentPage
    {
        public DetailView()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);


            // Assuming your ViewModel is bound as the BindingContext
            (BindingContext as DetailViewModel).MessageAdded += OnMessageAdded;
        }

        private void OnMessageAdded()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var count = (BindingContext as DetailViewModel).Messages.Count;
                _messagesCollectionView.ScrollTo(count - 1, position: ScrollToPosition.End, animate: true);
            });
        }
    }
}