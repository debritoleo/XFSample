using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XFSample;
using XFSample.Infra.Data;
using XFSample.Models;
using XFSample.Views;

namespace XFSamples.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IRepository<Person> _repository;
        public MainViewModel(INavigation navigation) : base(navigation)
        {
            _repository =
                new Repository<Person>
                (Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "People.db3"));

            RefreshCommand = new Command(async () => await LoadPeople());
            NewPersonCommand = new Command(async () => await OpenPageNewPerson());
            Task.Run(async () => await LoadPeople());
        }

        public ICommand RefreshCommand { private set; get; }
        public ICommand NewPersonCommand { private set; get; }
        public ObservableCollection<Person> People { get; set; }

        private async Task LoadPeople()
        {
            try
            {
                IsBusy = true;
                People?.Clear();
                People = new ObservableCollection<Person>(await _repository.GetAllAsync());
            }
            catch (Exception ex)
            {
                await App.Current.MainPage
                    .DisplayAlert("Atenção", "Não foi possivel obter os registros.", "Cancelar");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task OpenPageNewPerson()
        {
            await Navigation.PushAsync(new PersonDetailPage());
        }
    }
}
