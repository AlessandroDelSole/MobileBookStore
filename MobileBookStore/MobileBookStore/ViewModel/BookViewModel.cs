using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MobileBookStore.Model;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using Microsoft.WindowsAzure.MobileServices;
using MobileBookStore.Authentication;

namespace MobileBookStore.ViewModel
{
    public class BookViewModel: INotifyPropertyChanged
    {
        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; OnPropertyChanged(); }
        }

        private bool isUserAuthenticated;

        public bool IsUserAuthenticated
        {
            get
            {
                return isUserAuthenticated;
            }

            set
            {
                isUserAuthenticated = value; OnPropertyChanged();
            }
        }

        private string userId;

        public string UserId
        {
            get
            {
                return userId;
            }

            set
            {
                userId = value; OnPropertyChanged();
            }
        }

        private ObservableCollection<Book> books;

        public ObservableCollection<Book> Books
        {
            get
            {
                return books;
            }

            set
            {
                books = value; OnPropertyChanged();
            }
        }

        private Book selectedBook;

        public Book SelectedBook
        {
            get
            {
                return selectedBook;
            }

            set
            {
                selectedBook = value; OnPropertyChanged();
            }
        }

        public BookViewModel()
        {
            this.Books = new ObservableCollection<Book>();
            this.IsBusy = false;
        }


        public Command SaveBooks
        {
            get
            {
                return new RelayCommand(async() => 
                {
                    if(IsUserAuthenticated)
                    {
                        this.IsBusy = true;
                        await App.DataManager.SaveAllAsync(this.Books);
                        this.IsBusy = false;
                    }
                });
            }

        }

        public Command LoadBooks
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    if(IsUserAuthenticated)
                    {
                        this.IsBusy = true;
                        this.Books = new ObservableCollection<Book>(await App.DataManager.LoadAsync<Book>(UserId));
                        this.IsBusy = false;
                    }
                });
            }
        }

        public Command AddNewBook
        {
            get
            {
                return new RelayCommand(() => this.Books.Add(new Book { UserId = this.UserId } ));
            }
        }

        public Command DeleteBook
        {
            get
            {
                return new RelayCommand(() => this.Books.Remove(this.SelectedBook));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task LoginAsync()
        {
            var authenticator = DependencyService.Get<IAuthentication>();
            var user = await authenticator.LoginAsync(App.DataManager.Client, MobileServiceAuthenticationProvider.MicrosoftAccount);
            if (user == null)
            {
                IsUserAuthenticated = false;
                return;
            }
            else
            {
                UserId = user.UserId;
                IsUserAuthenticated = true;
                return;
            }
        }
    }
}
