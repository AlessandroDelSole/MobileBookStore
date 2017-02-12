using MobileBookStore.ViewModel;
using Xamarin.Forms;

namespace MobileBookStore
{
    public partial class MainPage : ContentPage
    {

        private BookViewModel ViewModel { get; set; }

        public MainPage()
        {
            InitializeComponent();

            this.ViewModel = new BookViewModel();
            this.BindingContext = this.ViewModel;

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if(this.ViewModel.IsUserAuthenticated==false)
            {
                await this.ViewModel.LoginAsync();
            }
        }
    }
}
