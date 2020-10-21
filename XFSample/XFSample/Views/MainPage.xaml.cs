using Xamarin.Forms;
using XFSamples.ViewModels;

namespace XFSamples
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel(Navigation);
        }
    }
}
