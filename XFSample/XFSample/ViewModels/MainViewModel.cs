using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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
            _repository = new Repository<Person>();

            People = new ObservableCollection<Person>();

            RefreshCommand = new Command(async () => await LoadPeople());
            NewPersonCommand = new Command(async () => await OpenPageNewPerson());
            SelectCommand = new Command(async () => await OpenPageEditPerson());
        }

        public ICommand RefreshCommand { private set; get; }
        public ICommand NewPersonCommand { private set; get; }
        public ICommand SelectCommand { private set; get; }
        public ObservableCollection<Person> People { get; set; }
        private Person _selected;
        public Person SelectedItem
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        public async Task LoadPeople()
        {
            try
            {
                IsBusy = true;

                var peopleBd = await _repository.GetAllAsync();

                People?.Clear();

                peopleBd = peopleBd.OrderByDescending(x => x.RegistrationDate)
                                   .ToList();

                foreach (var person in peopleBd)
                {
                    People.Add(person);
                }
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

        private async Task OpenPageEditPerson()
        {
            await Navigation.PushAsync(new PersonDetailPage(SelectedItem.Id));
        }
    }
}
