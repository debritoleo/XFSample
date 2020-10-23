using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XFSample.Infra.Data;
using XFSample.Models;
using XFSamples.ViewModels;

namespace XFSample.ViewModels
{
    public class PersonDetailViewModel : BaseViewModel
    {
        private readonly IRepository<Person> _repository;

        public PersonDetailViewModel(INavigation navigation) : base(navigation)
        {
            _repository =
                new Repository<Person>
                (Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "People.db3"));

            SaveCommand = new Command(async () => await Save());
        }

        public ICommand SaveCommand { private set; get; }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _phone;
        public string Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private DateTime _dtBirth;
        public DateTime DtBirth
        {
            get => _dtBirth;
            set => SetProperty(ref _dtBirth, value);
        }

        private async Task Save()
        {
            var person = new Person(Name, Phone, Email, Password, DtBirth);
            await _repository.SaveAsync(person);
        }
    }
}
