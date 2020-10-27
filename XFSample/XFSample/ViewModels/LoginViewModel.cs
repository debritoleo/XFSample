using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XFSample.Helpers;
using XFSample.Services;
using XFSample.Services.Validations.Rules;
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

        public string ErrorMessage { get; set; }

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
                if (!Validate()) return;

                await DialogsHelper.ShowLoading("Aguarde");

                var isValid = await _loginService.EffectLogin(Email, Password);

                if (!isValid)
                    await DialogsHelper.ShowAlert("Atenção", "Não foi possivel efetuar o login");

                App.Current.MainPage = new NavigationPage(new MainPage());
            }
            catch (Exception ex)
            {
                await DialogsHelper.ShowAlert("Atenção", "Não foi possivel efetuar o login");
            }
            finally
            {
                await DialogsHelper.HideLoading();
            }
        }

        private bool IsEnabledLogin()
        {
            return !string.IsNullOrWhiteSpace(Email) &&
                   !string.IsNullOrWhiteSpace(Password);
        }

        private bool Validate()
        {
            var ruleMail = new IsValidEmailRule<string>() { ValidationMessage = "O Email é inválido" };
            if (ruleMail.Check(Email))
                return true;

            ErrorMessage = ruleMail.ValidationMessage;
            return false;
        }
    }
}
