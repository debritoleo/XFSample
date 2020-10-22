using Refit;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace XFSample.Services
{
    public class LoginService
    {
        public LoginService()
        {
            _loginApi = RestService.For<ILoginApi>(EndPoints.BaseUrl);
        }
        private readonly ILoginApi _loginApi;
        private const string _keyToken = "KEY_TOKEN";

        public async Task<bool> EffectLogin(string email, string password)
        {
            var credential = await _loginApi.Login(email, password);

            if (string.IsNullOrWhiteSpace(credential?.Token))
                return false;

            await SecureStorage.SetAsync(_keyToken, credential.Token);

            return true;
        }
    }
}
