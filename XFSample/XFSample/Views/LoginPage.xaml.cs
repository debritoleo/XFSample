using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFSample.ViewModels;

namespace XFSamples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(Navigation);
        }
    }
}