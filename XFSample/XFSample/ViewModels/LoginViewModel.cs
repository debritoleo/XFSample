using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XFSample.Services;
using XFSamples;
using XFSamples.ViewModels;

namespace XFSample.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel(INavigation navigation) : base(navigation)
        {
            LoginCommand = new Command(async () => await Login(), IsEnabledLogin);
            _loginService = new LoginService();
        }

        private readonly LoginService _loginService;
        public ICommand LoginCommand { private set; get; }

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

        private async Task Login()
        {
            try
            {
                var isValid = await _loginService.EffectLogin(Email, Password);

                if (!isValid)
                    await App.Current.MainPage.DisplayAlert("Atenção", "Não foi possivel efetuar o login", "Cancelar");

                App.Current.MainPage = new NavigationPage(new MainPage());
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Atenção", "Não foi possivel efetuar o login", "Cancelar");
            }
        }

        private bool IsEnabledLogin()
        {
            return !string.IsNullOrWhiteSpace(Email) &&
                   !string.IsNullOrWhiteSpace(Password);
        }
    }
}
