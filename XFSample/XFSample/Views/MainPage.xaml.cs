using System.Threading.Tasks;
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

        protected override async void OnAppearing()
        {
            await LoadPeopleAsync();
        }

        async Task LoadPeopleAsync()
        {
            await ((MainViewModel)BindingContext).LoadPeople();
        }
    }
}
