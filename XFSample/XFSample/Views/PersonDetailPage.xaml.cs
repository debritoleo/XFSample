using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFSample.ViewModels;

namespace XFSample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonDetailPage : ContentPage
    {
        public PersonDetailPage(int id = 0)
        {
            InitializeComponent();
            BindingContext = new PersonDetailViewModel(Navigation, id);
        }
    }
}