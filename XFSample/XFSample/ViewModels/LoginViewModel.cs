using System.Windows.Input;
using Xamarin.Forms;
using XFSamples;
using XFSamples.ViewModels;

namespace XFSample.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel(INavigation navigation) : base(navigation)
        {
            LoginCommand = new Command(Login, IsEnabledLogin);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                SetProperty(ref _email, value);
                ((Command)LoginCommand).ChangeCanExecute();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
                ((Command)LoginCommand).ChangeCanExecute();
            }
        }

        public ICommand LoginCommand { private set; get; }

        private void Login()
        {
            Navigation.PushAsync(new NavigationPage(new MainPage()));
        }

        private bool IsEnabledLogin()
        {
            return !string.IsNullOrWhiteSpace(Email) &&
                   !string.IsNullOrWhiteSpace(Password);
        }
    }
}
